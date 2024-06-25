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
            _wb  = _excelApp.Workbooks.Open(DatabasePath);
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

        private Microsoft.Office.Interop.Excel.Application _excelApp;
        private Workbook _wb;
        private Worksheet _wsNormrohre, _wsWerkstoff, _wsFluid;


    }
    
}
