using System;
using System.IO;
using System.Windows.Forms;
using System.IO.Compression;
using PackAndGoLib;
using Inventor;


namespace ROhr2
{
    public class Speichern
    {

        public Speichern(Status status)
        {
            _status = status;
            _status.Name = "Getting Temp Path";
            _status.OnProgess();

            //Gets temporary folder of the user 
            string result = System.IO.Path.GetTempPath();
            _tempPath = result;

            _status.Name = "Done";
            _status.OnProgess();
        }

        //Gets temporary paths for the Inventor parts and the assebley
        public string getPathRohr()
        {
            string[] paths = { _tempPath, "Rohr.ipt" };
            _pathRohr = System.IO.Path.Combine(paths);
            return _pathRohr;
        }
        public string getPathBaugruppe()
        {
            string[] paths = { _tempPath, "Gesamt.iam" };
            _pathBaugruppen = System.IO.Path.Combine(paths);
            return _pathBaugruppen;
        }

        //Gets temporary paths for the screenshots
        public string getPathScreenX()
        {
            string[] paths = { _tempPath, "ScreenX.jpg" };
            _pathScreenX = System.IO.Path.Combine(paths);
            return _pathScreenX;
        }
        public string getPathScreenY()
        {
            string[] paths = { _tempPath, "ScreenY.jpg" };
            _pathScreenY = System.IO.Path.Combine(paths);
            return _pathScreenY;
        }
        public string getPathScreenZ()
        {
            string[] paths = { _tempPath, "ScreenZ.jpg" };
            _pathScreenZ = System.IO.Path.Combine(paths);
            return _pathScreenZ;
        }
        public string getPathScreenA()
        {
            string[] paths = { _tempPath, "ScreenA.jpg" };
            _pathScreenZ = System.IO.Path.Combine(paths);
            return _pathScreenZ;
        }
        public string getPathScreenB()
        {
            string[] paths = { _tempPath, "ScreenB.jpg" };
            _pathScreenZ = System.IO.Path.Combine(paths);
            return _pathScreenZ;
        }

        //Deletes created files in the temp folder
        public void deleteFiles()
        {
            System.IO.File.Delete(getPathRohr());
            System.IO.File.Delete(getPathBaugruppe());
            _tempPath = "";
            _pathRohr = "";
            _pathUnten = "";
        }

        //Exports final model in a specific folder structure
        public void exportFiles()
        {
            _status.Name = "Exporting Files";
            _status.Progress = 0;
            _status.OnProgess();

            //Main folder
            string[] paths = { selectedPath, "Rohrleitung" };
            folderPath = System.IO.Path.Combine(paths);
            var dir1 = folderPath;
            if (!Directory.Exists(dir1))
            {
                Directory.CreateDirectory(dir1);
            }

            _status.Progress = 30;
            _status.OnProgess();

            //CAD folder
            //string[] pathsCAD = { folderPath, "CAD" };
            //folderPathCAD = System.IO.Path.Combine(pathsCAD);
            //var dir3 = folderPathCAD;
            //if (!Directory.Exists(dir3))
            //{
            //    Directory.CreateDirectory(dir3);
            //}

            _status.Progress = 100;
            _status.Name = "Done";
            _status.OnProgess();

        }

        //Compresses the Main folder into a .zip
        public void makeZip()
        {
            _status.Name = "Creating Zip";
            _status.Progress = 25;
            _status.OnProgess();

            string[] paths = { selectedPath, "Rohrleitung.zip" };
            string zipPath = System.IO.Path.Combine(paths);
            System.IO.Compression.ZipFile.CreateFromDirectory(folderPath, zipPath);

            _status.Progress = 100;
            _status.Name = "Done";
            _status.OnProgess();
        }

 


        //Initialization of the required variables
        private Status _status;
        private string _tempPath;
        private string _pathRohr;
        private string _pathUnten;
        private string _pathBaugruppen;
        private string _pathScreenX;
        private string _pathScreenY;
        private string _pathScreenZ;
        private string _pathObenStl;
        private string _pathUntenStl;
        private string _pathObenStp;
        private string _pathUntenStp;
        public string folderPathCAD, folderPathDruck, selectedPath, folderPath;
    }

}
