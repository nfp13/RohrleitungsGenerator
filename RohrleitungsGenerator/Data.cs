//using Microsoft.Office.Interop.Excel;

using Inventor;
using System.Numerics;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ROhr2
{
    public class Cuboid
    {
        public Cuboid(Vector3 RangeBoxMin, Vector3 RangeBoxMax)
        {
            _Max = RangeBoxMax;
            _Min = RangeBoxMin;
            Faces = new Face[]
            {
                _GenerateFace(new Vector3(RangeBoxMin.X, RangeBoxMax.Y, RangeBoxMax.Z), RangeBoxMin),
                _GenerateFace(new Vector3(RangeBoxMax.X, RangeBoxMin.Y, RangeBoxMax.Z), RangeBoxMin),
                _GenerateFace(new Vector3(RangeBoxMax.X, RangeBoxMax.Y, RangeBoxMin.Z), RangeBoxMin),

                _GenerateFace(RangeBoxMax, new Vector3(RangeBoxMax.X, RangeBoxMin.Y, RangeBoxMin.Z)),
                _GenerateFace(RangeBoxMax, new Vector3(RangeBoxMin.X, RangeBoxMax.Y, RangeBoxMin.Z)),
                _GenerateFace(RangeBoxMax, new Vector3(RangeBoxMin.X, RangeBoxMin.Y, RangeBoxMax.Z)),
            };
            Edges = new Edge[]
            {
                new Edge(RangeBoxMin, new Vector3(RangeBoxMax.X, RangeBoxMin.Y , RangeBoxMin.Z)),
                new Edge(RangeBoxMin, new Vector3(RangeBoxMin.X, RangeBoxMax.Y , RangeBoxMin.Z)),
                new Edge(RangeBoxMin, new Vector3(RangeBoxMin.X, RangeBoxMin.Y , RangeBoxMax.Z)),

                new Edge(new Vector3(RangeBoxMin.X , RangeBoxMax.Y , RangeBoxMin.Z), new Vector3(RangeBoxMax.X, RangeBoxMax.Y, RangeBoxMin.Z)),
                new Edge(new Vector3(RangeBoxMin.X , RangeBoxMax.Y , RangeBoxMin.Z), new Vector3(RangeBoxMin.X, RangeBoxMax.Y, RangeBoxMax.Z)),

                new Edge(new Vector3(RangeBoxMax.X , RangeBoxMin.Y, RangeBoxMin.Z), new Vector3(RangeBoxMax.X , RangeBoxMax.Y, RangeBoxMin.Z)),
                new Edge(new Vector3(RangeBoxMin.X , RangeBoxMin.Y, RangeBoxMax.Z), new Vector3(RangeBoxMin.X , RangeBoxMax.Y, RangeBoxMax.Z)),

                new Edge(RangeBoxMax, new Vector3(RangeBoxMin.X, RangeBoxMax.Y , RangeBoxMax.Z)),
                new Edge(RangeBoxMax, new Vector3(RangeBoxMax.X, RangeBoxMin.Y , RangeBoxMax.Z)),
                new Edge(RangeBoxMax, new Vector3(RangeBoxMax.X, RangeBoxMax.Y , RangeBoxMin.Z)),

                new Edge(new Vector3(RangeBoxMax.X , RangeBoxMin.Y , RangeBoxMax.Z), new Vector3(RangeBoxMin.X, RangeBoxMin.Y, RangeBoxMax.Z)),
                new Edge(new Vector3(RangeBoxMax.X , RangeBoxMin.Y , RangeBoxMax.Z), new Vector3(RangeBoxMax.X, RangeBoxMin.Y, RangeBoxMin.Z))
            };
            Corners = new Vector3[]
            {
                RangeBoxMin,
                new Vector3(RangeBoxMax.X, RangeBoxMin.Y , RangeBoxMin.Z),
                new Vector3(RangeBoxMin.X, RangeBoxMax.Y , RangeBoxMin.Z),
                new Vector3(RangeBoxMin.X, RangeBoxMin.Y , RangeBoxMax.Z),

                new Vector3(RangeBoxMax.X, RangeBoxMax.Y, RangeBoxMin.Z),
                new Vector3(RangeBoxMin.X, RangeBoxMax.Y, RangeBoxMax.Z),

                new Vector3(RangeBoxMin.X, RangeBoxMax.Y , RangeBoxMax.Z),
                new Vector3(RangeBoxMax.X, RangeBoxMin.Y , RangeBoxMax.Z),
                new Vector3(RangeBoxMax.X, RangeBoxMax.Y , RangeBoxMin.Z)
            };
            _Center = Vector3.Add(_Min, Vector3.Multiply(Vector3.Subtract(_Max, _Min), (float)0.5));
            _Radius = Vector3.Distance(_Center, _Max);
        }

        public Vector3 _Min, _Max;
        private Vector3 _Center;
        private float _Radius;

        public class Face
        {
            public Face(Plane plane, Vector3 max, Vector3 min)
            {
                Max = max; Min = min;
                Plane = plane;
            }

            public Vector3 Max, Min;

            public Plane Plane;
        }
        public Face[] Faces;
        private Face _GenerateFace(Vector3 max, Vector3 min)
        {
            Vector3 normal;
            double distance;

            if (max.X == min.X)
            {
                normal = new Vector3(1, 0, 0);
                distance = min.X;
            }
            else if (max.Y == min.Y)
            {
                normal = new Vector3(0, 1, 0);
                distance = min.Y;
            }
            else
            {
                normal = new Vector3(0, 0, 1);
                distance = min.Z;
            }
            Face face = new Face(new Plane(normal, (float)distance), max, min);
            return face;
        }

        public class Edge
        {
            public Edge(Vector3 start, Vector3 end)
            {
                Position = start;
                End = end;
                Direction = Vector3.Normalize(Vector3.Subtract(end, start));
            }
            public Vector3 Position, Direction, End;
        }
        public Edge[] Edges;

        public Vector3[] Corners;

        public float GetMinDistanceToPoint(Vector3 point)
        {
            if (_PointBetweenMinMax(point, _Max, _Min))
            {
                return 0;
            }
            else
            {
                float minDistance = -1;

                foreach (Vector3 corner in Corners)     //Distance Point Point
                {
                    float d = Vector3.Distance(corner, point);
                    if (d < minDistance || minDistance == -1)
                    {
                        minDistance = d;
                    }
                }

                foreach (Edge edge in Edges)        //Distance Point Line
                {
                    float l = Vector3.Dot(point, edge.Direction) - Vector3.Dot(edge.Position, edge.Direction);
                    Vector3 s = Vector3.Add(edge.Position, Vector3.Multiply(edge.Direction, l));
                    float d = Vector3.Distance(s, point);
                    if ((d < minDistance || minDistance == -1) && _PointBetweenMinMax(s, edge.End, edge.Position))
                    {
                        minDistance = d;
                    }
                }

                foreach (Face face in Faces)        //Distance Point Plane
                {
                    float l = face.Plane.D - System.Numerics.Vector3.Dot(point, face.Plane.Normal);
                    Vector3 s = System.Numerics.Vector3.Add(point, System.Numerics.Vector3.Multiply(face.Plane.Normal, l));
                    float d = System.Numerics.Vector3.Distance(s, point);
                    if ((d < minDistance || minDistance == -1) && _PointBetweenMinMax(s, face.Max, face.Min))
                    {
                        minDistance = d;
                    }
                }
                return minDistance;
            }
        }

        public bool Collision(Vector3 point, float radius)
        {
            if (Vector3.Distance(_Center, point) > _Radius + radius)
            {
                return false;
            }
            else if (_PointBetweenMinMax(point, _Max, _Min))
            {
                return true;
            }
            else
            {
                foreach (Vector3 corner in Corners)     //Distance Point Point
                {
                    float d = Vector3.Distance(corner, point);
                    if (d < radius)
                    {
                        return true;
                    }
                }

                foreach (Edge edge in Edges)        //Distance Point Line
                {
                    float l = Vector3.Dot(point, edge.Direction) - Vector3.Dot(edge.Position, edge.Direction);
                    Vector3 s = Vector3.Add(edge.Position, Vector3.Multiply(edge.Direction, l));
                    float d = Vector3.Distance(s, point);
                    if (d < radius && _PointBetweenMinMax(s, edge.End, edge.Position))
                    {
                        return true;
                    }
                }

                foreach (Face face in Faces)        //Distance Point Plane
                {
                    float l = face.Plane.D - System.Numerics.Vector3.Dot(point, face.Plane.Normal);
                    Vector3 s = System.Numerics.Vector3.Add(point, System.Numerics.Vector3.Multiply(face.Plane.Normal, l));
                    float d = System.Numerics.Vector3.Distance(s, point);
                    if (d < radius && _PointBetweenMinMax(s, face.Max, face.Min))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        private bool _PointBetweenMinMax(Vector3 point, Vector3 Max, Vector3 Min)
        {
            return (point.X <= Max.X && point.X >= Min.X) && (point.Y <= Max.Y && point.Y >= Min.Y) && (point.Z <= Max.Z && point.Z >= Min.Z);
        }
    }

    public class Zylinder
    {
        public Zylinder(Vector3 start, Vector3 end, double r)
        {
            _Position = start;
            _End = end;
            _Direction = Vector3.Normalize(Vector3.Subtract(end, start));
            _Radius = (float)r;
        }

        public bool Collision(Vector3 point, float radius)
        {
            float l = Vector3.Dot(point, _Direction) - Vector3.Dot(_Position, _Direction);
            Vector3 s = Vector3.Add(_Position, Vector3.Multiply(_Direction, l));
            float d = Vector3.Distance(s, point);
            if (d < 1 * (radius + _Radius))
            {
                return true;
            }
            return false;
        }

        private bool _PointBetweenMinMax(Vector3 point, Vector3 Max, Vector3 Min)
        {
            return (point.X <= Max.X && point.X >= Min.X) && (point.Y <= Max.Y && point.Y >= Min.Y) && (point.Z <= Max.Z && point.Z >= Min.Z);
        }

        private Vector3 _Position, _End, _Direction;
        float _Radius;
    }
    public class Connection
    {
        public Connection()
        {
            _Flange1 = new Flange(new Vector3(0, 0, 0), new Vector3(1, 0, 0));
            _Flange2 = new Flange(new Vector3((float)6.2, (float)0.3, 0), new Vector3(-1, 0, 0));

            pipe = new Pipe(200000000000, 0.000012, 0.00001943, 0.00001943, 0.0001, 10, 200000000000, 1, 235000000, 0.1, 0.05, 0.5, 0);

            float _minDistFlange = (float)0.5;
            StartPoint = Vector3.Add(_Flange1.Point, Vector3.Multiply(Vector3.Normalize(_Flange1.Direction), _minDistFlange));
            EndPoint = Vector3.Add(_Flange2.Point, Vector3.Multiply(Vector3.Normalize(_Flange2.Direction), _minDistFlange));
        }
        public Connection(Flange flange1, Flange flange2, Pipe _pipe)
        {
            _Flange1 = flange1;
            _Flange2 = flange2;
            pipe = _pipe;

            float _minDistFlange = (float)(Math.Sin(Math.PI / 4) * pipe.B);
            StartPoint = Vector3.Add(_Flange1.Point, Vector3.Multiply(Vector3.Normalize(_Flange1.Direction), _minDistFlange));
            EndPoint = Vector3.Add(_Flange2.Point, Vector3.Multiply(Vector3.Normalize(_Flange2.Direction), _minDistFlange));
        }
        public Flange _Flange1, _Flange2;

        public List<Vector3> Path = new List<Vector3>();
        public List<string> Details = new List<string>();

        public Vector3 StartPoint;
        public Vector3 EndPoint;
        public Pipe pipe;

        public class Flange
        {
            public Flange(Vector3 point, Vector3 direction)
            {
                Point = point;
                Direction = direction;
            }
            public Vector3 Point, Direction;
        }
    }

    public class Pipe
    {
        public Pipe(double _E, double _at, double _Ib, double _It, double _A, double _dT, double _G, double _q, double _Rm, double _R, double _W, double _B, double _S)
        {
            E = _E;         //E-Modul
            at = _at;       //Wärmeausdehnungskoeffizient
            G = _G;         //Schubmodul
            Rm = _Rm;       //Zugfestigkeit

            R = _R;         //Rohraußenradius
            W = _W;         //Wandstärke
            B = _B;         //Biegeradius

            Ib = _Ib;       //Biegewiderstandsmoment
            It = _It;       //Torsionswiderstandsmoment
            A = _A;         //Querschnittsfläche

            dT = _dT;       //Temperaturdelta
            q = _q;         //Flächenlast aufgrund von Fluid und Rohrgewicht

            S = _S;         //Steigung
        }

        public double E, at, Ib, It, A, dT, G, q, Rm, R, W, B, S;
    }

    public class Data
    {
        public void SetMinSize()
        {
            foreach (Cuboid c in Cuboids)
            {
                if (MinSize != 0)
                {
                    if (c._Max.X - c._Min.X < MinSize)
                    {
                        MinSize = c._Max.X - c._Min.X;
                    }
                    if (c._Max.Y - c._Min.Y < MinSize)
                    {
                        MinSize = c._Max.Y - c._Min.Y;
                    }
                    if (c._Max.Z - c._Min.Z < MinSize)
                    {
                        MinSize = c._Max.Z - c._Min.Z;
                    }
                }
                else
                {
                    if (true)
                    {
                        MinSize = c._Max.X - c._Min.X;
                    }
                    if (c._Max.Y - c._Min.Y < MinSize)
                    {
                        MinSize = c._Max.Y - c._Min.Y;
                    }
                    if (c._Max.Z - c._Min.Z < MinSize)
                    {
                        MinSize = c._Max.Z - c._Min.Z;
                    }
                }

            }
        }

        public double MinSize = 0;
        public Data() { }
        public List<Cuboid> Cuboids = new List<Cuboid>();
        public List<Zylinder> Zylinders = new List<Zylinder>();
        public List<Connection> Connections = new List<Connection>();
    }

}
