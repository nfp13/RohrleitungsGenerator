using ROhr2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace ROhr2
{
    public class GeneratePipeSystem
    {
        public GeneratePipeSystem(Data data)
        {
            _data = data;
            var ts = new ThreadStart(_ManageThreads);
            _managementThread = new Thread(ts);
            _managementThread.Start();
        }

        public void Stop()
        {
            _managementThread.Abort();

            foreach (Thread t in _threads) t.Abort();
        }

        private void _ManageThreads()
        {
            foreach (Connection con in _data.Connections)
            {
                _CreatePipeAgentThread(con);
            }
            int pipeCount = _data.Connections.Count;

            while (_threads.Count > 0)
            {
                foreach (Thread t in _threads.ToList())
                {
                    if (t.ThreadState == ThreadState.Stopped)
                    {
                        int index = _threads.IndexOf(t);
                        _threads.RemoveAt(index);

                        int coll = _CheckCollisionsWithPipe(_pipeAgents[index].Con());
                        int Collisions = pipeCount - coll;
                        _conBuffer.Enqueue(_pipeAgents[index].Con(), Collisions);

                        _pipeAgents.RemoveAt(index);
                    }
                }
            }

            while (_conBuffer.Count > 0)
            {
                Connection con = _conBuffer.Dequeue();

                _ReGeneratePipe(con);
                _conBuffer.Clear();
                Done.Clear();

                foreach (Connection c in _data.Connections)
                {
                    int coll = _CheckCollisionsWithPipe(c);
                    if (coll != 0)
                    {
                        int Collisions = pipeCount - coll;
                        _conBuffer.Enqueue(c, Collisions);
                    }
                    else
                    {
                        Done.Add(c);
                    }
                }
            }

            foreach (Connection c in _data.Connections)
            {
                _PathMessageBox(c.Path);
            }

        }

        private void _CreatePipeAgentThread(Connection con)
        {
            _pipeAgents.Add(new PipeAgent(con, _data));
            _threads.Add(new Thread(new ThreadStart(_pipeAgents.Last().Solve)));
            _threads.Last().Start();
            System.Diagnostics.Debug.WriteLine("New ThreadID: " + _threads.Last().ManagedThreadId.ToString());
        }

        private int _CheckCollisionsWithPipe(Connection con)
        {
            int count = 0;
            foreach (Connection c in _data.Connections)
            {
                if (c == con)
                {
                    continue;
                }
                float minDist = (float)(con.pipe.R + c.pipe.R);
                int i = 1;
                bool k = true;
                while (i < con.Path.Count && k)
                {
                    int n = 1;
                    Vector3 p1 = con.Path[i - 1];
                    Vector3 q1 = con.Path[i];

                    while (n < c.Path.Count)
                    {
                        Vector3 p2 = c.Path[n - 1];
                        Vector3 q2 = c.Path[n];

                        float dist = _ClosestDistanceBetweenLineSegments(p1, q1, p2, q2);

                        if (dist < minDist)
                        {
                            count++;
                            k = false;
                            break;
                        }

                        n++;
                    }
                    i++;
                }
            }
            //MessageBox.Show(count.ToString());
            return count;
        }

        private void _ReGeneratePipe(Connection con)
        {
            con.Path.Clear();
            foreach (Connection c in _data.Connections)
            {
                if (c == con)
                {
                }
                else
                {
                    int i = 1;
                    while (i < c.Path.Count)
                    {
                        Vector3 start = c.Path[i - 1];
                        Vector3 end = c.Path[i];
                        _data.Zylinders.Add(new Zylinder(start, end, c.pipe.R));
                        i++;
                    }

                }
            }
            PipeAgent Agent = new PipeAgent(con, _data);
            Agent.Solve();
            _data.Zylinders.Clear();
        }

        private void _PathMessageBox(List<Vector3> Path)
        {
            string PathString = "";
            foreach (Vector3 v in Path)
            {
                PathString += v.ToString() + "\n";
            }
            MessageBox.Show(PathString, "CurrentPath");
        }

        private float _ClosestDistanceBetweenLineSegments(Vector3 p1, Vector3 q1, Vector3 p2, Vector3 q2)       //p1/q1 Start1/End1
        {
            Vector3 u = q1 - p1;
            Vector3 v = q2 - p2;
            Vector3 w = p1 - p2;
            float a = Vector3.Dot(u, u); // square of length of u
            float b = Vector3.Dot(u, v);
            float c = Vector3.Dot(v, v); // square of length of v
            float d = Vector3.Dot(u, w);
            float e = Vector3.Dot(v, w);
            float D = a * c - b * b; // denominator of the solution

            float sc, sN, sD = D; // sc = sN / sD, default sD = D >= 0
            float tc, tN, tD = D; // tc = tN / tD, default tD = D >= 0

            // Compute the line parameters of the two closest points
            if (D < float.Epsilon)
            {
                // The lines are almost parallel
                sN = 0.0f; // force using point P1 on segment S1
                sD = 1.0f; // to prevent possible division by 0.0 later
                tN = e;
                tD = c;
            }
            else
            {
                // Get the closest points on the infinite lines
                sN = (b * e - c * d);
                tN = (a * e - b * d);
                if (sN < 0.0f)
                {
                    // sc < 0 => the s=0 edge is visible
                    sN = 0.0f;
                    tN = e;
                    tD = c;
                }
                else if (sN > sD)
                {
                    // sc > 1 => the s=1 edge is visible
                    sN = sD;
                    tN = e + b;
                    tD = c;
                }
            }

            if (tN < 0.0f)
            {
                // tc < 0 => the t=0 edge is visible
                tN = 0.0f;
                // recompute sc for this edge
                if (-d < 0.0f)
                    sN = 0.0f;
                else if (-d > a)
                    sN = sD;
                else
                {
                    sN = -d;
                    sD = a;
                }
            }
            else if (tN > tD)
            {
                // tc > 1 => the t=1 edge is visible
                tN = tD;
                // recompute sc for this edge
                if ((-d + b) < 0.0f)
                    sN = 0.0f;
                else if ((-d + b) > a)
                    sN = sD;
                else
                {
                    sN = (-d + b);
                    sD = a;
                }
            }

            // Finally do the division to get sc and tc
            sc = (Math.Abs(sN) < float.Epsilon ? 0.0f : sN / sD);
            tc = (Math.Abs(tN) < float.Epsilon ? 0.0f : tN / tD);

            // Get the difference of the two closest points
            Vector3 dP = w + (sc * u) - (tc * v); // = S1(sc) - S2(tc)

            return dP.Length(); // return the closest distance
        }



        private Thread _managementThread;
        private List<Thread> _threads = new List<Thread>();
        private List<PipeAgent> _pipeAgents = new List<PipeAgent>();
        private PriorityQueue<Connection, int> _conBuffer = new PriorityQueue<Connection, int>();
        public List<Connection> Done = new List<Connection>();
        private Data _data;
    }

}
