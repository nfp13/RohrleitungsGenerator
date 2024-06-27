using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Inventor;
using System;
using System.Numerics;
using System.Security.Policy;

namespace ROhr2
{
    public class Analyze
    {

        public Analyze(Inventor.Application inventorApp, string filePath, Status status, Data data)
        {
            //Setting private variables

            _status = status;
            _filePath = filePath;
            _inventorApp = inventorApp;

            _status.Name = "Opening Assembly";
            _status.OnProgess();

            //Opening the PCB assembly Document

            _assemblyDocument = _inventorApp.Documents.Open(_filePath, true) as AssemblyDocument;
            _assemblyComponentDefinition = _assemblyDocument.ComponentDefinition;

            _data = data;


            _status.Name = "Done";
            _status.OnProgess();

        }

        public void List()
        {
            //Creating list containing all component occurence names

            Parts.Clear();
            int NumberOfOccurrences = _assemblyComponentDefinition.Occurrences.Count;
            int CurrentOccurrence = 1;
            _status.Name = "Finding Parts";
            _status.Progress = 0;
            _status.OnProgess();
            foreach (ComponentOccurrence componentOccurrence in _assemblyComponentDefinition.Occurrences)
            {
                Parts.Add(componentOccurrence.Name);
                _status.Progress = Convert.ToInt16((CurrentOccurrence * 1.0) / (NumberOfOccurrences * 1.0) * 100);
                CurrentOccurrence++;
                _status.OnProgess();
            }

            _status.Name = "Done";
            _status.OnProgess();

            Hindernisse.AddRange(Parts);

        }



        public void Hall(string hallOccurrenceName)
        {
            _status.Name = "Analyzing";
            _status.OnProgess();

            Box box = _assemblyComponentDefinition.RangeBox;

            HallW = (box.MaxPoint.X - box.MinPoint.X);
            HallL = (box.MaxPoint.Y - box.MinPoint.Y);
            HallH = (box.MaxPoint.Z - box.MinPoint.Z);


            _status.Name = "Done";
            _status.OnProgess();

        }


        public void SavePictureAs(string Path, string ViewXYZ)
        {
            //Taking picture of PCB and saving in Path

            _status.Name = "Taking Screenshot";
            _status.Progress = 0;
            _status.OnProgess();

            //Creating the camera

            Inventor.View view = _assemblyDocument.Views[1];
            Inventor.Camera camera = view.Camera;
            camera.Perspective = false;

            _status.Progress = 75;
            _status.OnProgess();

            //Setting camera perspective, fiting the camera to the PCB and exporting picture

            switch (ViewXYZ)
            {

                case "X":
                    camera.ViewOrientationType = ViewOrientationTypeEnum.kIsoTopLeftViewOrientation;
                    break;

                case "Y":
                    camera.ViewOrientationType = ViewOrientationTypeEnum.kIsoTopRightViewOrientation;
                    break;

                case "Z":
                    camera.ViewOrientationType = ViewOrientationTypeEnum.kTopViewOrientation;
                    break;

                default:
                    break;
            }

            camera.Fit();
            camera.Apply();
            view.Update();
            camera.SaveAsBitmap(Path, 1080, 1080);

            _status.Progress = 100;
            _status.Name = "Done";
            _status.OnProgess();
        }

        public void GenerateCuboids()
        {
            foreach (ComponentOccurrence occ in _assemblyComponentDefinition.Occurrences)
            {
                if (Hindernisse.Contains(occ.Name))
                {
                    Inventor.Point minI = occ.RangeBox.MinPoint;
                    Inventor.Point maxI = occ.RangeBox.MaxPoint;
                    Vector3 min = new Vector3((float)minI.X / 100, (float)minI.Z / 100, (float)minI.Y / 100);
                    Vector3 max = new Vector3((float)maxI.X / 100, (float)maxI.Z / 100, (float)maxI.Y / 100);
                    Cuboid Cube = new Cuboid(min, max);
                    _data.Cuboids.Add(Cube);
                }
            }
        }

        public Connection.Flange getFlangeFromPartName(string FlangeName)
        {

            Hindernisse.Remove(FlangeName);
            PartDocument part;
            Vector3 originV3 = new Vector3(0, 0, 0);
            Vector3 dirV3 = new Vector3(0, 0, 0);

            foreach (ComponentOccurrence occ in _assemblyComponentDefinition.Occurrences)
            {
                if (FlangeName == occ.Name)
                {
                    //MessageBox.Show(occ.Name);

                    part = (PartDocument)occ.Definition.Document;
                    WorkPoint wp1 = part.ComponentDefinition.WorkPoints["Arbeitspunkt1"];
                    WorkPoint wp2 = part.ComponentDefinition.WorkPoints["Arbeitspunkt2"];

                    Inventor.Matrix matrix = occ.Transformation;
                    //matrix.Invert();
                    Inventor.Point origin = wp1.Point;
                    origin.TransformBy(matrix);
                    Inventor.Vector dir = wp1.Point.VectorTo(wp2.Point);
                    dir.TransformBy(matrix);

                    originV3 = Vector3.Multiply(new Vector3((float)origin.X, (float)origin.Z, (float)origin.Y), (float)0.01);
                    dirV3 = Vector3.Multiply(new Vector3((float)dir.X, (float)dir.Z, (float)dir.Y), (float)0.01);

                    //y und z vertauscht
                    //MessageBox.Show(origin.X.ToString() + "    " + origin.Z.ToString() + "    " + origin.Y.ToString() + "\n" + dir.X.ToString() + "    " + dir.Z.ToString() + "    " + dir.Y.ToString());
                }
            }

            Connection.Flange flange = new Connection.Flange(originV3, dirV3);

            //MessageBox.Show(FlangeName);
            Hindernisse.Remove(FlangeName);
            Flange.Add(FlangeName);
            return flange;
        }




        private Inventor.Application _inventorApp;
        private string _filePath;
        private AssemblyDocument _assemblyDocument;
        private AssemblyComponentDefinition _assemblyComponentDefinition;
        private AssemblyDocument _boardAssemblyDocument;
        private Faces _faces;

        public List<string> Parts = new List<string>();
        public List<string> Hindernisse = new List<string>();
        public List<string> Flange = new List<string>();

        public double HallW, HallL, HallH;                       //Grundmaße der Platine
        public double CompHeightTop, CompHeightBottom;              //Höhe der Komponenten auf der Platine
        public double HoleDia, CornerRadius = 0;                    //Durchmesser der Bohrungslöcher und Rundungsradius der Platine
        private double _XOM, _YOM, _ZOM;                            //Coordinates of centerpoint of Board

        private Inventor.Point _MinPointHall;
        private Inventor.Point _MaxPointHall;
        private Inventor.Point _MinPointGes;
        private Inventor.Point _MaxPointGes;

        //public List<CutOut> CutOuts = new List<CutOut>();

        private Status _status;
        private Data _data;
    }
}
