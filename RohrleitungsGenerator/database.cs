using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;


namespace ROhr2
{
    public class Database
    {
        public Database()
        {
            //intitialziren
            _excelApp = new Microsoft.Office.Interop.Excel.Application();
            _OpenRefFile();
        }

        public void _OpenRefFile()
        {
            string DatabasePath = $"{System.IO.Path.GetTempPath()}database.xlsx";
            _wb = _excelApp.Workbooks.Open(DatabasePath);
            _wsNormrohre = (Worksheet)_wb.Worksheets[1];
            _wsWerkstoff = (Worksheet)_wb.Worksheets[2];
            _wsFluid = (Worksheet)_wb.Worksheets[3];
        }

        public void CloseExcel()
        {
            _wb.Close();
            _excelApp.Quit();
        }

        private void _GenerateTemplateFile()
        {
            //Creating Template from Resources and saving it to the temp folder 

            byte[] templateFile = RohrleitungsGenerator.Properties.Resources.database;
            string tempPath = $"{System.IO.Path.GetTempPath()}database.xlsx";
            using (MemoryStream ms = new MemoryStream(templateFile))
            {
                using (FileStream fs = new FileStream(tempPath, FileMode.OpenOrCreate))
                {
                    ms.WriteTo(fs);
                    fs.Close();
                }
                ms.Close();
            }
        }

        public double GetNormrohreHoleDia(double holeDia)
        {
            int MaxRowN = _wsNormrohre.Rows.Count;
            int i = 2;
            double readExcelDia = 0.0;

            while (i < MaxRowN && readExcelDia < holeDia)
            {
                i++;
                double cellValue = (double)(_wsNormrohre.Cells[i, 1] as Microsoft.Office.Interop.Excel.Range).Value;
            }
            i--;
            if (_wsNormrohre.Cells[i + 1, 1].Value >= holeDia)
            {
                return (_wsNormrohre.Cells[i, 2].Value);
            }
            else
            {
                MessageBox.Show("Löcher in Platine zu groß!");
                return 0.0;
            }

        }

        public List<Fluid> fluid = new List<Fluid>();
        public List<Werkstoff> werkstoff = new List<Werkstoff>();
        public List<Normrohre> normrohre = new List<Normrohre>();

        private Microsoft.Office.Interop.Excel.Application _excelApp;
        private Workbook _wb;
        private Worksheet _wsNormrohre, _wsWerkstoff, _wsFluid;

    }

    public class Normrohre
    {
        public Normrohre(double außenradius, double wandstärke, double biegeradius)
        {
            Außenradius = außenradius;
            Wandstärke = wandstärke;
            Biegeradius = biegeradius;
        }
        public double Außenradius;
        public double Wandstärke;
        public double Biegeradius;
    }

    public class Werkstoff
    {
        public Werkstoff(double emodul, double wärmeausdehnung, double mindestzugfestigkeit, double dichte, double schubmodul)
        {
            Emodul = emodul;
            Wärmeausdehnung = wärmeausdehnung;
            Mindestzugfestigkeit = mindestzugfestigkeit;
            Dichte = dichte;
            Schubmodul = schubmodul;
        }
        public double Emodul;
        public double Wärmeausdehnung;
        public double Mindestzugfestigkeit;
        public double Dichte;
        public double Schubmodul;
    }

    public class Fluid
    {
        public Fluid(double wasser, double öl, double gas)
        {
            Wasser = wasser;
            Öl = öl;
            Gas = gas;
        }
        public double Wasser;
        public double Öl;
        public double Gas;
    }
}