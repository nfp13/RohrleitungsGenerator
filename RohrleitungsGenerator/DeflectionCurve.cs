//using Microsoft.Office.Interop.Excel;
using System.Numerics;

namespace ROhr2
{
    public class DeflectionCurve
    {
        public DeflectionCurve()
        {
            Vector3 start = new Vector3(0, 0, 0);
            Vector3 point1 = new Vector3(1, 0, 0);
            Vector3 point2 = new Vector3(4, 0, 0);
            Vector3 point3 = new Vector3(7, 0, 0);
            Vector3 end = new Vector3(8, 0, 0);
            Vector3 Force = new Vector3(0, 4, 0);
            Vector3 Moment = new Vector3(0, (float)-5.33, 0);

            Pipe testPipe = new Pipe(200000000000, 0.000012, 0.00001943, 0.00001943, 0.0001, 10, 200000000000, 1, 235000000, 0.1, 0.05, 0.5, 0);


            _beams.Add(new StartBeam(testPipe, start, point1, Force, Moment));
            _beams.Add(new Beam(testPipe, point1, point2, _beams[0]));
            _beams.Add(new Beam(testPipe, point2, point3, _beams[1]));
            _beams.Add(new Beam(testPipe, point3, end, _beams[2]));
            MessageBox.Show("Force: " + _beams[0].GetForceAtEnd().ToString() + "\nMoment: " + _beams[0].GetMomentAtEnd().ToString() + "\nDeflection: " + _beams[0].GetDeflectionAtEnd().ToString() + "\nAngle: " + _beams[0].GetDeflectionAngleAtEnd().ToString());
            MessageBox.Show("Force: " + _beams[1].GetForceAtEnd().ToString() + "\nMoment: " + _beams[1].GetMomentAtEnd().ToString() + "\nDeflection: " + _beams[1].GetDeflectionAtEnd().ToString() + "\nAngle: " + _beams[1].GetDeflectionAngleAtEnd().ToString());
            MessageBox.Show("Force: " + _beams[2].GetForceAtEnd().ToString() + "\nMoment: " + _beams[2].GetMomentAtEnd().ToString() + "\nDeflection: " + _beams[2].GetDeflectionAtEnd().ToString() + "\nAngle: " + _beams[2].GetDeflectionAngleAtEnd().ToString());
            MessageBox.Show("Force: " + _beams[3].GetForceAtEnd().ToString() + "\nMoment: " + _beams[3].GetMomentAtEnd().ToString() + "\nDeflection: " + _beams[3].GetDeflectionAtEnd().ToString() + "\nAngle: " + _beams[3].GetDeflectionAngleAtEnd().ToString());
            //MessageBox.Show(_beams[0].GetDeflectionAtEnd().Y.ToString() + " " + _beams[1].GetDeflectionAtEnd().Y.ToString() + " " + _beams[2].GetDeflectionAtEnd().Y.ToString() + " ");
        }

        public DeflectionCurve(int test)
        {
            Pipe testPipe = new Pipe(200000000000, 0.000012, 0.00001943, 0.00001943, 0.0001, 10, 200000000000, 1, 235000000, 0.1, 0.05, 0.5, 0);
            List<Vector3> points = new List<Vector3>();
            points.Add(new Vector3(0, 0, 0));
            points.Add(new Vector3(1, 0, 0));
            points.Add(new Vector3(4, 0, 0));
            points.Add(new Vector3(7, 0, 0));
            points.Add(new Vector3(8, 0, 0));

            _beams.Add(new StartBeam(testPipe, points[0], points[1], _GetStartForce(points, testPipe), new Vector3(0, 0, 0)));
            Beam prevBeam = _beams[0];
            int i = 2;
            while (i < points.Count)
            {
                Beam beam = new Beam(testPipe, points[i - 1], points[i], prevBeam);
                prevBeam = beam;
                _beams.Add(beam);
                i++;
            }

            MessageBox.Show("Force: " + _beams[0].GetForceAtEnd().ToString() + "\nMoment: " + _beams[0].GetMomentAtEnd().ToString() + "\nDeflection: " + _beams[0].GetDeflectionAtEnd().ToString() + "\nAngle: " + _beams[0].GetDeflectionAngleAtEnd().ToString());
            MessageBox.Show("Force: " + _beams[1].GetForceAtEnd().ToString() + "\nMoment: " + _beams[1].GetMomentAtEnd().ToString() + "\nDeflection: " + _beams[1].GetDeflectionAtEnd().ToString() + "\nAngle: " + _beams[1].GetDeflectionAngleAtEnd().ToString());
            MessageBox.Show("Force: " + _beams[2].GetForceAtEnd().ToString() + "\nMoment: " + _beams[2].GetMomentAtEnd().ToString() + "\nDeflection: " + _beams[2].GetDeflectionAtEnd().ToString() + "\nAngle: " + _beams[2].GetDeflectionAngleAtEnd().ToString());
            MessageBox.Show("Force: " + _beams[3].GetForceAtEnd().ToString() + "\nMoment: " + _beams[3].GetMomentAtEnd().ToString() + "\nDeflection: " + _beams[3].GetDeflectionAtEnd().ToString() + "\nAngle: " + _beams[3].GetDeflectionAngleAtEnd().ToString());
        }
        public DeflectionCurve(List<Vector3> points, Pipe pipe)
        {
            _beams.Add(new StartBeam(pipe, points[0], points[1], _GetStartForce(points, pipe), new Vector3(0, 0, 0)));
            Beam prevBeam = _beams[0];
            int i = 2;
            while (i < points.Count)
            {
                Beam beam = new Beam(pipe, points[i - 1], points[i], prevBeam);
                prevBeam = beam;
                _beams.Add(beam);
                i++;
            }
        }

        public List<string> Details()
        {
            List<string> details = new List<string>();
            foreach (Beam beam in _beams)
            {
                if (beam.CompNeeded() && beam.SupportNeeded())
                {
                    details.Add("Kompensator, Auflager");
                }
                else if (beam.CompNeeded())
                {
                    details.Add("Kompensator");
                }
                else if (beam.SupportNeeded())
                {
                    details.Add("Auflager");
                }
                else details.Add(" ");
            }
            return details;
        }

        private Vector3 _GetStartForce(List<Vector3> Points, Pipe pipe)
        {
            float moment = 0;
            int i = Points.Count - 1;
            Vector3 end = Points[i];
            while (i > 0)
            {
                Vector3 dir = Vector3.Subtract(Points[i - 1], Points[i]);
                Vector3 center = Vector3.Add(Points[i], Vector3.Multiply(dir, (float)0.5));
                Vector3 distCenterToEnd = Vector3.Subtract(end, center);
                moment += Vector3.Distance(Points[i], Points[i - 1]) * (float)pipe.q * (float)distCenterToEnd.X;
                i--;
            }
            Vector3 distStartToEnd = Vector3.Subtract(end, Points[1]);
            float _Force = moment / distStartToEnd.X;
            return new Vector3(0, _Force, 0);
        }

        private List<Beam> _beams = new List<Beam>();

        public class Beam
        {
            public Beam(Pipe pipe, Vector3 start, Vector3 end, Beam prevBeam)       //q = kraft pro cm rohrlänge  Später noch Material und Rohrkonstanten (E und I....)
            {
                _pipe = pipe;
                //Transformationsmatrix generieren

                Vector3 dir = Vector3.Normalize(Vector3.Subtract(end, start));
                Vector3 test = new Vector3(1, 0, 0);
                float angle = (float)Math.Acos(Vector3.Dot(dir, test));
                Vector3 axis = Vector3.Cross(dir, test);
                _TransformationMatrix = Matrix4x4.CreateFromAxisAngle(axis, angle);
                Matrix4x4.Invert(_TransformationMatrix, out Matrix4x4 test1);
                _ReTransformationMatrix = test1;

                _q = Vector3.Transform(new Vector3(0, (float)_pipe.q, 0), _TransformationMatrix);
                _c1 = Vector3.Transform(prevBeam.GetForceAtEnd(), _TransformationMatrix);
                _c2 = Vector3.Transform(prevBeam.GetMomentAtEnd(), _TransformationMatrix);
                _c3 = Vector3.Transform(Vector3.Negate(prevBeam.GetDeflectionAngleAtEnd()), _TransformationMatrix);
                _c4 = Vector3.Transform(Vector3.Negate(prevBeam.GetDeflectionAtEnd()), _TransformationMatrix);
                _l = Vector3.Distance(start, end);
            }

            public Beam() { }

            //Rücktransformation in ursprungskoordinaten
            public Vector3 GetForceAtEnd()
            {
                return Vector3.Transform(_Q(_l), _ReTransformationMatrix);
            }

            public Vector3 GetMomentAtEnd()
            {
                return Vector3.Transform(_M(_l), _ReTransformationMatrix);
            }

            public Vector3 GetDeflectionAngleAtEnd()
            {
                return Vector3.Transform(Vector3.Negate(_w1(_l)), _ReTransformationMatrix);
            }

            public Vector3 GetDeflectionAtEnd()
            {
                return Vector3.Transform(Vector3.Negate(_w(_l)), _ReTransformationMatrix);
            }

            protected Vector3 _Q(double x)
            {
                return Vector3.Add(Vector3.Negate(Vector3.Multiply(_q, (float)x)), _c1);
            }

            protected Vector3 _M(double x)
            {
                return Vector3.Add(Vector3.Add(Vector3.Negate(Vector3.Multiply(_q, (float)(Math.Pow(x, 2) / 2))), Vector3.Multiply(_c1, (float)x)), _c2);
            }

            protected Vector3 _w1(double x)
            {
                Vector3 w1 = Vector3.Add(Vector3.Multiply(Vector3.Add(Vector3.Add(Vector3.Multiply(_q, (float)(Math.Pow(x, 3) / 6)), Vector3.Negate(Vector3.Multiply(_c1, (float)(Math.Pow(x, 2) / 2)))), Vector3.Negate(Vector3.Multiply(_c2, (float)x))), (float)(1 / (_pipe.E * _pipe.Ib))), _c3);
                w1.X = (_M(x).X * (float)x) / (float)(_pipe.G * _pipe.It);
                return w1;
            }

            protected Vector3 _w(double x)
            {
                Vector3 w = Vector3.Add(Vector3.Add(Vector3.Multiply(Vector3.Add(Vector3.Add(Vector3.Multiply(_q, (float)(Math.Pow(x, 4) / 24)), Vector3.Negate(Vector3.Multiply(_c1, (float)(Math.Pow(x, 3) / 6)))), Vector3.Negate(Vector3.Multiply(_c2, (float)(Math.Pow(x, 2)) / 2))), (float)(1 / (_pipe.E * _pipe.Ib))), Vector3.Multiply(_c3, (float)x)), _c4);
                w.X = (_Q(x).X * (float)x) / (float)(_pipe.E * _pipe.A) + (float)(_pipe.at * _pipe.dT * x);
                return w;
            }

            protected double _Mbres(double x)
            {
                return (Math.Sqrt(Math.Pow(_M(x).Y, 2) + Math.Pow(_M(x).Z, 2)));
            }

            protected double _N(double x)
            {
                return _Q(x).X;
            }

            public bool SupportNeeded()
            {
                double tension1 = (_Mbres(0) * _pipe.R) / _pipe.Ib;
                double tension2 = (_Mbres(_l / 2) * _pipe.R) / _pipe.Ib;
                double tension3 = (_Mbres(_l) * _pipe.R) / _pipe.Ib;

                //MessageBox.Show(tension1.ToString() + " | " + tension2.ToString() + " | " + tension3.ToString());

                if (tension1 > _pipe.Rm || tension2 > _pipe.Rm || tension3 > _pipe.Rm)
                {
                    return true;
                }
                else return false;
            }

            public bool CompNeeded()
            {
                double tension1 = (_N(0) * _pipe.R) / _pipe.Ib;
                double tension2 = (_N(_l / 2) * _pipe.R) / _pipe.Ib;
                double tension3 = (_N(_l) * _pipe.R) / _pipe.Ib;

                //MessageBox.Show(tension1.ToString() + " | " + tension2.ToString() + " | " + tension3.ToString());

                if (tension1 > _pipe.Rm || tension2 > _pipe.Rm || tension3 > _pipe.Rm)
                {
                    return true;
                }
                else return false;
            }

            protected Matrix4x4 _TransformationMatrix, _ReTransformationMatrix;
            protected Vector3 _q;
            protected Beam _prevBeam;
            protected Pipe _pipe;
            protected double _l;
            protected Vector3 _c1, _c2, _c3, _c4;      //Integrationskonstanten
        }

        public class StartBeam : Beam
        {
            public StartBeam(Pipe pipe, Vector3 start, Vector3 end, Vector3 ForceStart, Vector3 MomentStart)
            {
                _pipe = pipe;
                Vector3 dir = Vector3.Normalize(Vector3.Subtract(end, start));
                Vector3 test = new Vector3(1, 0, 0);
                float angle = (float)Math.Acos(Vector3.Dot(dir, test));
                Vector3 axis = Vector3.Cross(dir, test);
                _TransformationMatrix = Matrix4x4.CreateFromAxisAngle(axis, angle);
                Matrix4x4.Invert(_TransformationMatrix, out Matrix4x4 test1);
                _ReTransformationMatrix = test1;

                _q = Vector3.Transform(new Vector3(0, (float)_pipe.q, 0), _TransformationMatrix);
                _c1 = Vector3.Transform(ForceStart, _TransformationMatrix);
                _c2 = Vector3.Transform(MomentStart, _TransformationMatrix);
                _c3 = Vector3.Transform(new Vector3(0, 0, 0), _TransformationMatrix);
                _c4 = Vector3.Transform(new Vector3(0, 0, 0), _TransformationMatrix);
                _l = Vector3.Distance(start, end);
            }
        }

        public class EndBeam : Beam
        {
            public EndBeam(Pipe pipe, Vector3 start, Vector3 end, Beam prevBeam)
            {
                {
                    _pipe = pipe;
                    Vector3 dir = Vector3.Normalize(Vector3.Subtract(end, start));
                    Vector3 test = new Vector3(1, 0, 0);
                    float angle = (float)Math.Acos(Vector3.Dot(dir, test));
                    Vector3 axis = Vector3.Cross(dir, test);
                    _TransformationMatrix = Matrix4x4.CreateFromAxisAngle(axis, angle);
                    Matrix4x4.Invert(_TransformationMatrix, out Matrix4x4 test1);
                    _ReTransformationMatrix = test1;

                    _q = Vector3.Transform(new Vector3(0, (float)_pipe.q, 0), _TransformationMatrix);
                    _c1 = Vector3.Transform(prevBeam.GetForceAtEnd(), _TransformationMatrix);
                    _c2 = Vector3.Transform(prevBeam.GetMomentAtEnd(), _TransformationMatrix);
                    _c3 = Vector3.Transform(Vector3.Negate(prevBeam.GetDeflectionAngleAtEnd()), _TransformationMatrix);
                    _c4 = Vector3.Transform(Vector3.Negate(prevBeam.GetDeflectionAtEnd()), _TransformationMatrix);
                    _l = Vector3.Distance(start, end);
                }
            }

        }

    }

}
