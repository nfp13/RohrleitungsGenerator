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

        public string ToString()
        {
            return _Center.ToString();
        }
    }
    public class Connection
    {

        public Connection()
        {
            _Flange1 = new Flange(new Vector3(0, 0, 0), new Vector3(1, 0, 0));
            _Flange2 = new Flange(new Vector3((float)6.2, (float)0.3, 0), new Vector3(-1, 0, 0));
            Radius = (float)1;

            float _minDistFlange = (float)0.5;
            StartPoint = Vector3.Add(_Flange1.Point, Vector3.Multiply(Vector3.Normalize(_Flange1.Direction), _minDistFlange));
            EndPoint = Vector3.Add(_Flange2.Point, Vector3.Multiply(Vector3.Normalize(_Flange2.Direction), _minDistFlange));

        }
        public Connection(Flange flange1, Flange flange2, double BendingRadius)
        {
            _Flange1 = flange1;
            _Flange2 = flange2;

            Radius = (float)0.8;

            float _minDistFlange = (float)(Math.Sin(Math.PI / 4) * BendingRadius);
            StartPoint = Vector3.Add(_Flange1.Point, Vector3.Multiply(Vector3.Normalize(_Flange1.Direction), _minDistFlange));
            EndPoint = Vector3.Add(_Flange2.Point, Vector3.Multiply(Vector3.Normalize(_Flange2.Direction), _minDistFlange));

            //analyze.UpdateList(flange1);
            //analyze.UpdateList(flange2);
        }
        private Flange _Flange1, _Flange2;

        public double Wallthickness;
        public bool StandardizedParts = false;
        public float Radius;

        public List<Vector3> Path = new List<Vector3>();
        public Vector3 StartPoint;
        public Vector3 EndPoint;
        public List<Element> Elements = new List<Element>();
        public Pipe pipe;


        public class Element
        {
            public Element() { }

            public Vector3 Start, End;
            public double Radius;
            public Func<double, Vector3> Function;
            public double lMax;         //Maximum der Laufvariable der Function
        }
        public class Pipe : Element
        {
            public Pipe(Vector3 start, Vector3 end, double outerDiameter, double wallThickness)
            {
                Start = start;
                End = end;
                Radius = outerDiameter * 0.5;

                Vector3 _Dir = Vector3.Normalize(Vector3.Subtract(End, Start));
                Function = (l) => { return Vector3.Add(Start, Vector3.Multiply(_Dir, (float)l)); };
                lMax = Vector3.Distance(Start, End);

                _OuterDiameter = outerDiameter;
                _WallThickness = wallThickness;
            }
            private double _OuterDiameter, _WallThickness;
        }
        public class Bend : Element
        {
            public Bend(Vector3 start, Vector3 end, double outerDiameter, double wallThickness)
            {

            }
        }

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
        public Pipe(double _E, double _at, double _Ib, double _It, double _A, double _dT, double _G, double _q)
        {
            E = _E;         //E-Modul
            at = _at;       //Wärmeausdehnungskoeffizient
            Ib = _Ib;       //Biegewiderstandsmoment
            It = _It;       //Torsionswiderstandsmoment
            A = _A;         //Querschnittsfläche
            dT = _dT;       //Temperaturdelta
            G = _G;         //Schubmodul
            q = _q;         //Flächenlast aufgrund von Fluid und Rohrgewicht
        }

        public double E, at, Ib, It, A, dT, G, q;
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
        public List<Connection> Connections = new List<Connection>();
    }

}
