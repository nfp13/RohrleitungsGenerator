using System;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;


namespace ROhr2
{
    public class database
    {
       public database()
        {
            //intitialziren
            _Application excel = new Microsoft.Office.Interop.Excel.Application();
            _OpenRefFile();
        }

        public void _OpenRefFile()
        {
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            string databasePath = string.Format("{0}Resources\\database.xlsx", System.I0.Path.GetFullPath(System.I0.Path.Combine(RunningPath, @"..\..\")));
            _wb  = excel.Workbooks.Open(databasePath);
            _wsNormrohre = _wb.Worksheets[1];
            _wsWerkstoff = _wb.Worksheets[2];
            _wsFluid = _wb.Worksheets[3];

        }

    }
    
}
