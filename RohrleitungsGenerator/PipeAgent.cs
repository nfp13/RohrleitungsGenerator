using ROhr2;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;

namespace ROhr2
{
    public class PipeAgent
    {
        public PipeAgent(Connection con, Data data)
        {
            _con = con;
            _data = data;
        }

        public void Solve()
        {
            Pathfinder _path = new Pathfinder(_con, _data);
            _con.Path.Add(_con._Flange2.Point);
            _con.Path.AddRange(_GetCornerPoints(_path.Solve()));
            _con.Path.Add(_con._Flange1.Point);
            _con.Path.Reverse();

            //_PathMessageBox();

            if (_con.pipe.S != 0)
            {
                SolverSlope slope = new SolverSlope(_con);              //No Pathchecking; only works on paths that are structured right... Addon in the Pathfinder is needed to ensure compliance (only nodes on the same level or below are valid nodes in a Path for a sloped pipe)
            }

            //_PathMessageBox();

            _curve = new DeflectionCurve(_con.Path, _con.pipe);
            _con.Details.AddRange(_curve.Details());

            //_DetailsMessageBox();
        }

        private List<Vector3> _GetCornerPoints(List<Vector3> points)
        {
            List<Vector3> _corners = new List<Vector3>();
            _corners.Add(points[0]);
            int i = 1;
            while (i < points.Count - 1)
            {
                Vector3 Dir1 = Vector3.Normalize(Vector3.Subtract(points[i], points[i - 1]));
                Vector3 Dir2 = Vector3.Normalize(Vector3.Subtract(points[i + 1], points[i]));
                if (Vector3.Distance(Dir1,Dir2) >= _data.MinSize)
                {
                    _corners.Add(points[i]);
                }
                i++;
            }
            _corners.Add(points.Last());
            return _corners;
        }

        public Connection Con()
        {
            return _con;
        }


        private void _PathMessageBox()
        {
            string PathString = "";
            foreach (Vector3 v in _con.Path)
            {
                PathString += v.ToString() + "\n";
            }
            MessageBox.Show(PathString, "CurrentPath");
        }

        private void _DetailsMessageBox()
        {
            string DetailsString = "";
            foreach (string s in _con.Details)
            {
                DetailsString += s.ToString() + "\n";
            }
            MessageBox.Show(DetailsString, "Details");
        }

        private Connection _con;
        private Data _data;
        private DeflectionCurve _curve;
    }

}
