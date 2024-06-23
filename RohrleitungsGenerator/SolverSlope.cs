using ROhr2;
using System;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ROhr2
{
    public class SolverSlope
    {
        public SolverSlope(Connection con)
        {
            _con = con;

            int i = 1;
            while (i < _con.Path.Count)
            {
                Vector3 vector = Vector3.Subtract(_con.Path[i], _con.Path[i - 1]);
                double distXZ = Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Z, 2));
                double diff = -distXZ * (_con.pipe.S / 100);
                if (diff < vector.Y)
                {
                    float y = _con.Path[i].Y - (float)(vector.Y - diff);
                    _con.Path[i] = new Vector3(_con.Path[i].X, y, _con.Path[i].Z);
                }
                i++;
            }
        }

        private Connection _con;
    }

}
