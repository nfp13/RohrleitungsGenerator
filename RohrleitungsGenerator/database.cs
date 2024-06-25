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

        public void GetNormrohreData(double holeDia)
        {
            int MaxRowN = _wsNormrohre.Rows.Count;
            int i = 2;
            double readExcelDia = 0.0;

            while (i < MaxRowN && readExcelDia < holeDia)
            {
                i++;
                double Außenradius = (double)(_wsNormrohre.Cells[i, 1] as Microsoft.Office.Interop.Excel.Range).Value;
                double Wandstärke = (double)(_wsNormrohre.Cells[i, 2] as Microsoft.Office.Interop.Excel.Range).Value;
                double Biegeradius = (double)(_wsNormrohre.Cells[i, 3] as Microsoft.Office.Interop.Excel.Range).Value;
                Normrohre.Add(new Normrohr(Außenradius, Wandstärke, Biegeradius));
            }
                      
        }

        public void GetWerkstoffData(double holeDia)
        {
            int MaxRowN = _wsWerkstoff.Rows.Count;
            int i = 2;
            double readExcelDia = 0.0;

            while (i < MaxRowN && readExcelDia < holeDia)
            {
                i++;
                double Emodul = (double)(_wsWerkstoff.Cells[i, 1] as Microsoft.Office.Interop.Excel.Range).Value;
                double Wärmeausdehnung = (double)(_wsWerkstoff.Cells[i, 2] as Microsoft.Office.Interop.Excel.Range).Value;
                double Mindestzugfestigkeit = (double)(_wsWerkstoff.Cells[i, 3] as Microsoft.Office.Interop.Excel.Range).Value;
                double Dichte = (double)(_wsWerkstoff.Cells[i, 4] as Microsoft.Office.Interop.Excel.Range).Value;
                double Schubmodul = (double)(_wsWerkstoff.Cells[i, 5] as Microsoft.Office.Interop.Excel.Range).Value;
                Werkstoff.Add(new Werkstoff(Emodul, Wärmeausdehnung, Mindestzugfestigkeit,Dichte,Schubmodul));
                //ggf. i bei 2 anfagen?
            }

        }

        public void GetFluidData(double holeDia)
        {
            int MaxRowN = _wsFluid.Rows.Count;
            int i = 2;
            double readExcelDia = 0.0;

            while (i < MaxRowN && readExcelDia < holeDia)
            {
                i++;
                double Dichte = (double)(_wsFluid.Cells[i, 1] as Microsoft.Office.Interop.Excel.Range).Value;
                Fluid.Add(new Fluid(Dichte));
                //Name noch mit rein?
            }

        }



        public List<Fluid> Fluids = new List<Fluid>();
        public List<Werkstoff> Werkstoffe = new List<Werkstoff>();
        public List<Normrohr> Normrohre = new List<Normrohr>();

        private Microsoft.Office.Interop.Excel.Application _excelApp;
        private Workbook _wb;
        private Worksheet _wsNormrohre, _wsWerkstoff, _wsFluid;

    }

    public class Normrohr
    {
        public Normrohr(double außenradius, double wandstärke, double biegeradius)
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
        public Werkstoff(double emodul, double wärmeausdehnung, double mindestzugfestigkeit, double dichte, double schubmodul, string name)
        {
            Emodul = emodul;
            Wärmeausdehnung = wärmeausdehnung;
            Mindestzugfestigkeit = mindestzugfestigkeit;
            Dichte = dichte;
            Schubmodul = schubmodul;
            Name = name;
        }
        public double Emodul;
        public double Wärmeausdehnung;
        public double Mindestzugfestigkeit;
        public double Dichte;
        public double Schubmodul;
        public string Name;
    }

    public class Fluid
    {
        public Fluid(string name, double dichte)
        {
            Name = name;
            Dichte = dichte;
        }
        public string Name;
        public double Dichte;
    }
}