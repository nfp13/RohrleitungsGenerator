using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Inventor;
using System;

namespace ROhr2
{
    public class Analyze
    {

        public Analyze(Inventor.Application inventorApp, string filePath, Status status)
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

        }

        

        public void Hall(string hallOccurrenceName)
        {
            _status.Name = "Analyzing";
            _status.OnProgess();

            foreach (ComponentOccurrence componentOccurrence in _assemblyComponentDefinition.Occurrences)
            {

                if (componentOccurrence.Name == hallOccurrenceName)
                {
                    Box box = componentOccurrence.Definition.RangeBox;

                    _MinPointHall = box.MinPoint;
                    _MaxPointHall = box.MaxPoint;

                    HallW = (box.MaxPoint.X - box.MinPoint.X);
                    HallL = (box.MaxPoint.Y - box.MinPoint.Y);
                    HallH = (box.MaxPoint.Z - box.MinPoint.Z);

                }
                _status.Progress = 100;
                _status.Name = "Done";
                _status.OnProgess();
            }


            _status.Name = "Done";
            _status.OnProgess();

        }

        /*

        public Matrix GetTransformationMatrix()
        {
            //Creating transformation Matrix for placing the PCB-centerpoint in the origin of an assembly document

            Vector MinMaxVector = _MinPointBoard.VectorTo(_MaxPointBoard);
            MinMaxVector.ScaleBy(0.5);
            Inventor.Point MiddlePointPlatine = _MinPointBoard;
            MiddlePointPlatine.TranslateBy(MinMaxVector);
            Vector NewVector = MiddlePointPlatine.VectorTo(_inventorApp.TransientGeometry.CreatePoint(0, 0, 0));
            Inventor.Point NewOrigin = _inventorApp.TransientGeometry.CreatePoint(0, 0, 0);
            NewOrigin.TranslateBy(NewVector);
            Matrix OriginConversion = _inventorApp.TransientGeometry.CreateMatrix();
            OriginConversion.SetCoordinateSystem(NewOrigin, _inventorApp.TransientGeometry.CreateVector(1, 0, 0), _inventorApp.TransientGeometry.CreateVector(0, 1, 0), _inventorApp.TransientGeometry.CreateVector(0, 0, 1));
            return OriginConversion;
        }

        public void AddConnectorToCutOuts(string CutOutOccurrenceName)
        {
            //Adding Connector to CutOuts list and getting all nessesary variables

            foreach (ComponentOccurrence componentOccurrence in _assemblyComponentDefinition.Occurrences)
            {
                //Finding specific occurence

                if (componentOccurrence.Name == CutOutOccurrenceName)
                {
                    Box box = componentOccurrence.Definition.RangeBox;

                    Vector Top = box.MaxPoint.VectorTo(_MaxPointBoard);
                    Vector Bottom = box.MinPoint.VectorTo(_MinPointBoard);

                    CutOut _CutOut = new CutOut();

                    _CutOut.Connector = true;

                    //Calculating size of the connector in x, y and z direction

                    _CutOut.XS = box.MaxPoint.X - box.MinPoint.X;
                    _CutOut.YS = box.MaxPoint.Y - box.MinPoint.Y;
                    _CutOut.ZS = box.MaxPoint.Z - box.MinPoint.Z;

                    //Calculating location of connector-centerpoint relative to board-centerpoint in x, y and z direction

                    _CutOut.XP = box.MinPoint.X + (_CutOut.XS / 2) - _XOM;
                    _CutOut.YP = box.MinPoint.Y + (_CutOut.YS / 2) - _YOM;
                    _CutOut.ZP = box.MinPoint.Z + (_CutOut.ZS / 2) - _ZOM;

                    //Checking if connector is located on the top or the bottom of the PCB

                    if (Top.Z <= 0.0)
                    {
                        _CutOut.Top = true;
                    }
                    else if (Bottom.Z >= 0.0)
                    {
                        _CutOut.Top = false;
                    }
                    else
                    {
                        MessageBox.Show("Fehler");
                    }

                    _CutOut.Name = componentOccurrence.Name;
                    CutOuts.Add(_CutOut);
                }
            }
        }

        public void AddLEDToCutOuts(string CutOutOccurrenceName)
        {
            //Adding LED/Display to CutOuts list and getting all nessesary variables

            foreach (ComponentOccurrence componentOccurrence in _assemblyComponentDefinition.Occurrences)
            {
                //Finding specific occurence

                if (componentOccurrence.Name == CutOutOccurrenceName)
                {
                    Box box = componentOccurrence.Definition.RangeBox;

                    CutOut _CutOut = new CutOut();

                    Vector Top = box.MaxPoint.VectorTo(_MaxPointBoard);
                    Vector Bottom = box.MinPoint.VectorTo(_MinPointBoard);

                    _CutOut.Connector = false;

                    //Calculating size of the connector in x, y and z direction

                    _CutOut.XS = box.MaxPoint.X - box.MinPoint.X;
                    _CutOut.YS = box.MaxPoint.Y - box.MinPoint.Y;
                    _CutOut.ZS = box.MaxPoint.Z - box.MinPoint.Z;

                    //Calculating location of connector-centerpoint relative to board-centerpoint in x, y and z direction

                    _CutOut.XP = box.MinPoint.X + (_CutOut.XS / 2) - _XOM;
                    _CutOut.YP = box.MinPoint.Y + (_CutOut.YS / 2) - _YOM;
                    _CutOut.ZP = box.MinPoint.Z + (_CutOut.ZS / 2) - _ZOM;

                    //Checking if connector is located on the top or the bottom of the PCB

                    if (Top.Z <= 0.0)
                    {
                        _CutOut.Top = true;
                    }
                    else if (Bottom.Z >= 0.0)
                    {
                        _CutOut.Top = false;
                    }
                    else
                    {
                        MessageBox.Show("Fehler");
                    }

                    _CutOut.Name = componentOccurrence.Name;
                    CutOuts.Add(_CutOut);
                }
            }
        }


        ggg*/

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

        private Inventor.Application _inventorApp;
        private string _filePath;
        private AssemblyDocument _assemblyDocument;
        private AssemblyComponentDefinition _assemblyComponentDefinition;
        private AssemblyDocument _boardAssemblyDocument;

        public List<string> Parts = new List<string>();

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
    }
}
