using ROhr2;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;

namespace ROhr2
{
    public class Pathfinder
    {
        public Pathfinder(Connection con, Data data)
        {
            _Con = con;
            _Data = data;

            _OpenList.Enqueue(new Node(_Con.StartPoint), 0);
            _StepSize = (float)(_Data.MinSize * 0.5);
            //_Solve();
        }

        public List<Vector3> Solve()       //public for testing
        {
            Node currentNode;
            while (_OpenList.Count > 0)
            {
                //System.Diagnostics.Debug.WriteLine(_OpenList.Count.ToString());
                currentNode = _OpenList.Dequeue();

                if (currentNode.Position == _Con.EndPoint)
                {
                    List<Node> path = _PathToEnd(currentNode);
                    List<Vector3> result = new List<Vector3>();
                    string test = "";
                    foreach (Node node in path)
                    {
                        result.Add(node.Position);
                        test += node.Position.ToString() + "\n";
                    }

                    //MessageBox.Show("Weg Gefunden!\n" + test);

                    return result;
                }
                else if (Vector3.Distance(currentNode.Position, _Con.EndPoint) <= _StepSize)
                {
                    Node endNode = new Node(currentNode, _Con.EndPoint, _g, 1);
                    List<Node> path = _PathToEnd(endNode);
                    List<Vector3> result = new List<Vector3>();
                    string test = "";
                    foreach (Node node in path)
                    {
                        result.Add(node.Position);
                        test += node.Position.ToString() + "\n";
                    }

                    //MessageBox.Show("Weg Gefunden!\nKein direkter Treffer!\n" + test);

                    return result;
                }
                _ClosedList.Add(currentNode);
                _ExpandNode(currentNode);
            }

            MessageBox.Show("Kein Weg Gefunden");
            return null;
        }
        private void _ExpandNode(Node node)     //Generation of nodes hier Schrittgröße dynamisch ändern
        {
            Queue<Node> possibleSuccessors = new Queue<Node>();
            _GenerateAdjacentNodes(possibleSuccessors, node, 1);

            while (possibleSuccessors.Count > 0)
            {
                Node n = possibleSuccessors.Dequeue();

                /*if (n.StepFactor != 1 && n.PrevNode.PrevNode != null)
                {
                    Vector3 V1 = Vector3.Normalize(Vector3.Subtract(n.PrevNode.PrevNode.Position, n.PrevNode.Position));
                    Vector3 V2 = Vector3.Normalize(Vector3.Subtract(n.PrevNode.Position, n.Position));
                    double angle = Math.Cos((double)Vector3.Dot(V1, V2));
                    if (angle ==  0 || angle == Math.PI/2 || angle == Math.PI / 4)
                    {
                        
                    }
                    else
                    {
                        continue;
                    }
                    //Alle Halfstep nodes aussortieren welche nicht im 0°, 45° oder 90° Winkel stehen
                }*/

                bool k = true;
                foreach (Cuboid cube in _Data.Cuboids)
                {
                    if (cube.Collision(n.Position, (float)_Con.pipe.R))
                    {
                        /*if(n.StepFactor > (float)0.5)       //HalfSteps if Collision with node
                        {
                            _GenerateAdjacentNodes(possibleSuccessors, n.PrevNode, n.StepFactor * (float)0.5);
                        }*/
                        k = false; break;
                    }
                }
                if (!k) { continue; }

                foreach (Zylinder zyl in _Data.Zylinders)
                {
                    if (zyl.Collision(n.Position, (float)_Con.pipe.R))
                    {
                        //MessageBox.Show("Test");
                        k = false; break;
                    }
                }
                if (!k) { continue; }

                foreach (Node cn in _ClosedList)
                {
                    if (cn.Position == n.Position)
                    {
                        k = false; break;
                    }
                }
                if (!k) { continue; }

                int i = 0;
                List<(Node Element, double Priority)> OpenItems = _OpenList.UnorderedItems.ToList();
                while (i < _OpenList.Count)
                {
                    if (n.Position == OpenItems.ElementAt(i).Element.Position)
                    {
                        if (n.g >= OpenItems.ElementAt(i).Element.g)
                        {
                            k = false; break;
                        }
                        else
                        {
                            OpenItems.RemoveAt(i);
                            _OpenList.Clear();
                            _OpenList.EnqueueRange(OpenItems);
                        }
                    }
                    i++;
                }
                if (k)
                {
                    _OpenList.Enqueue(n, _f(n));
                }
            }
        }
        private void _GenerateAdjacentNodes(Queue<Node> possibleSuccessors, Node node, float StepFactor)
        {
            float newStepSize = StepFactor * _StepSize;

            possibleSuccessors.Enqueue(new Node(node, Vector3.Add(node.Position, new Vector3(newStepSize, 0, 0)), _g, newStepSize));
            possibleSuccessors.Enqueue(new Node(node, Vector3.Add(node.Position, new Vector3(-newStepSize, 0, 0)), _g, newStepSize));
            possibleSuccessors.Enqueue(new Node(node, Vector3.Add(node.Position, new Vector3(0, newStepSize, 0)), _g, newStepSize));
            possibleSuccessors.Enqueue(new Node(node, Vector3.Add(node.Position, new Vector3(0, -newStepSize, 0)), _g, newStepSize));

            possibleSuccessors.Enqueue(new Node(node, Vector3.Add(node.Position, new Vector3(newStepSize, newStepSize, 0)), _g, newStepSize));
            possibleSuccessors.Enqueue(new Node(node, Vector3.Add(node.Position, new Vector3(-newStepSize, newStepSize, 0)), _g, newStepSize));
            possibleSuccessors.Enqueue(new Node(node, Vector3.Add(node.Position, new Vector3(newStepSize, -newStepSize, 0)), _g, newStepSize));
            possibleSuccessors.Enqueue(new Node(node, Vector3.Add(node.Position, new Vector3(-newStepSize, -newStepSize, 0)), _g, newStepSize));

            possibleSuccessors.Enqueue(new Node(node, Vector3.Add(node.Position, new Vector3(newStepSize, 0, newStepSize)), _g, newStepSize));
            possibleSuccessors.Enqueue(new Node(node, Vector3.Add(node.Position, new Vector3(-newStepSize, 0, newStepSize)), _g, newStepSize));
            possibleSuccessors.Enqueue(new Node(node, Vector3.Add(node.Position, new Vector3(0, newStepSize, newStepSize)), _g, newStepSize));
            possibleSuccessors.Enqueue(new Node(node, Vector3.Add(node.Position, new Vector3(0, -newStepSize, newStepSize)), _g, newStepSize));

            possibleSuccessors.Enqueue(new Node(node, Vector3.Add(node.Position, new Vector3(newStepSize, newStepSize, newStepSize)), _g, newStepSize));
            possibleSuccessors.Enqueue(new Node(node, Vector3.Add(node.Position, new Vector3(-newStepSize, newStepSize, newStepSize)), _g, newStepSize));
            possibleSuccessors.Enqueue(new Node(node, Vector3.Add(node.Position, new Vector3(newStepSize, -newStepSize, newStepSize)), _g, newStepSize));
            possibleSuccessors.Enqueue(new Node(node, Vector3.Add(node.Position, new Vector3(-newStepSize, -newStepSize, newStepSize)), _g, newStepSize));

            possibleSuccessors.Enqueue(new Node(node, Vector3.Add(node.Position, new Vector3(newStepSize, 0, -newStepSize)), _g, newStepSize));
            possibleSuccessors.Enqueue(new Node(node, Vector3.Add(node.Position, new Vector3(-newStepSize, 0, -newStepSize)), _g, newStepSize));
            possibleSuccessors.Enqueue(new Node(node, Vector3.Add(node.Position, new Vector3(0, newStepSize, -newStepSize)), _g, newStepSize));
            possibleSuccessors.Enqueue(new Node(node, Vector3.Add(node.Position, new Vector3(0, -newStepSize, -newStepSize)), _g, newStepSize));

            possibleSuccessors.Enqueue(new Node(node, Vector3.Add(node.Position, new Vector3(newStepSize, newStepSize, -newStepSize)), _g, newStepSize));
            possibleSuccessors.Enqueue(new Node(node, Vector3.Add(node.Position, new Vector3(-newStepSize, newStepSize, -newStepSize)), _g, newStepSize));
            possibleSuccessors.Enqueue(new Node(node, Vector3.Add(node.Position, new Vector3(newStepSize, -newStepSize, -newStepSize)), _g, newStepSize));
            possibleSuccessors.Enqueue(new Node(node, Vector3.Add(node.Position, new Vector3(-newStepSize, -newStepSize, -newStepSize)), _g, newStepSize));

            possibleSuccessors.Enqueue(new Node(node, Vector3.Add(node.Position, new Vector3(0, 0, newStepSize)), _g, newStepSize));
            possibleSuccessors.Enqueue(new Node(node, Vector3.Add(node.Position, new Vector3(0, 0, -newStepSize)), _g, newStepSize));
        }

        private List<Node> _PathToEnd(Node last)
        {
            List<Node> Path = new List<Node>();
            Path.Add(last);

            while (Path.Last().Position != _Con.StartPoint)
            {
                Path.Add(Path.Last().PrevNode);
            }

            return Path;
        }
        private Func<Node, double> _g = (Node node) => { return node.PrevNode.g + (double)Vector3.Distance(node.Position, node.PrevNode.Position); };
        private double _f(Node node)
        {
            return node.g + (double)Vector3.Distance(_Con.EndPoint, node.Position);
        }

        private PriorityQueue<Node, double> _OpenList = new PriorityQueue<Node, double>();
        private List<Node> _ClosedList = new List<Node>();
        private Connection _Con;
        private Data _Data;
        private float _StepSize = (float)0.5;
    }

    public class Node
    {
        public Node(Vector3 Start)
        {
            Position = Start;
            PrevNode = null;
            g = 0;
            StepFactor = 1;
        }
        public Node(Node prevNode, Vector3 pos, Func<Node, double> _g, float stepFactor)
        {
            Position = pos;
            PrevNode = prevNode;
            g = _g(this);
            StepFactor = stepFactor;
        }

        public Vector3 Position;
        public double g;
        public Node PrevNode;
        public float StepFactor;
    }
}