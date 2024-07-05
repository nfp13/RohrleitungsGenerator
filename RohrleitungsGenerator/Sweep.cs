//using Microsoft.Office.Interop.Excel;
using Inventor;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ROhr2
{
    public class Sweep
    {
        Speichern speichern;

        public Sweep(Inventor.Application inventorApp, string filePath, Status status, Connection con)
        {
            //Setting private variables
            _status = status;
            _inventorApp = inventorApp;
            _con = con;
            _filePath = filePath;
            speichern = new Speichern(status);
            _status.Name = "Opening Assembly";
            //_status.OnProgess();

            //Opening the assembly and part Document
            _partDocument = _inventorApp.Documents.Add(DocumentTypeEnum.kPartDocumentObject, _inventorApp.GetTemplateFile(DocumentTypeEnum.kPartDocumentObject)) as PartDocument;
            _partComponentDefinition = _partDocument.ComponentDefinition;
            _transientGeometry = _inventorApp.TransientGeometry;
            _assemblyDocument = _inventorApp.Documents.Open(_filePath, false) as AssemblyDocument;
            _assemblyComponentDefinition = _assemblyDocument.ComponentDefinition;

            _status.Name = "Done";
            //_status.OnProgess();
        }

        public void sketch3d()
        {
            //Draw 3D sketch by creating points
            Vector3 p = Vector3.Multiply(_con.Path[0], 100);
            _wp = _partComponentDefinition.WorkPoints.AddFixed(_transientGeometry.CreatePoint(p.X, p.Z, p.Y));

            Vector3 p1 = Vector3.Multiply(_con.Path[1], 100);
            WorkPoint wp1 = _partComponentDefinition.WorkPoints.AddFixed(_transientGeometry.CreatePoint(p1.X, p1.Z, p1.Y));

            _sketch3D = _partComponentDefinition.Sketches3D.Add();

            //connect points to 3D line
            _lines.Add(_sketch3D.SketchLines3D.AddByTwoPoints(_wp, wp1, true, _con.pipe.B *100));
            for (int i = 2; i < _con.Path.Count; i++)
            {
                Vector3 p2 = Vector3.Multiply(_con.Path[i], 100); ;
                WorkPoint wp2 = _partComponentDefinition.WorkPoints.AddFixed(_transientGeometry.CreatePoint(p2.X, p2.Z, p2.Y));
                _lines.Add(_sketch3D.SketchLines3D.AddByTwoPoints(_lines[i-2].EndPoint, wp2, true, _con.pipe.B * 100));
            }
        }

        public void sketch2d()
        {
            //drawing 2D sketch of the pipe cross-section
            _workPlane = _partComponentDefinition.WorkPlanes.AddByNormalToCurve(_lines[0], _wp);
            _sketch2D = _partComponentDefinition.Sketches.Add(_workPlane);
            _origin = _sketch2D.ModelToSketchSpace(_transientGeometry.CreatePoint(0, 0, 0));
            _sketch2D.OriginPoint = _wp;

            //drawing two circles to get a donat
            SketchCircle circ1 = _sketch2D.SketchCircles.AddByCenterRadius(_transientGeometry.CreatePoint2d(0,0), _con.pipe.R * 100);
            SketchCircle circ2 = _sketch2D.SketchCircles.AddByCenterRadius(_transientGeometry.CreatePoint2d(0, 0), _con.pipe.R * 100 - _con.pipe.W * 100);
            _profile = _sketch2D.Profiles.AddForSolid();
            foreach (ProfilePath pp in _profile)
            {
                if (pp[1].SketchEntity == circ2)
                {
                    pp.AddsMaterial = false;
                }
            }

        }

        public void feature()
        {
            //using the 3D and 2D sketch to sweep
            _path = _partComponentDefinition.Features.CreatePath(_lines[0]);
            _sweep = _partComponentDefinition.Features.SweepFeatures.AddUsingPath(_profile, _path, Inventor.PartFeatureOperationEnum.kJoinOperation);

        }

        public void addPart()
        {
            // save  part
            speichern = new Speichern(_status);
            _partDocument.SaveAs(speichern.getPathRohr(), true);

            // add part to assembley
            _status.Name = "Adding Parts";
            _status.Progress = 0;
            //_status.OnProgess();

            //Generating Matrix
            Matrix positionMatrix = _inventorApp.TransientGeometry.CreateMatrix();

            //Move 
            Inventor.Vector trans = _inventorApp.TransientGeometry.CreateVector(0, 0, 0);
            positionMatrix.SetTranslation(trans);

            //Place
            ComponentOccurrence rohr = _assemblyDocument.ComponentDefinition.Occurrences.Add(speichern.getPathRohr(), positionMatrix);
            //MessageBox.Show(speichern.getPathRohr());

            _status.Progress = 100;
            _status.Name = "Done";
            //_status.OnProgess();

        }

        private Inventor.Application _inventorApp;
        private string _filePath;
        private AssemblyDocument _assemblyDocument;
        private AssemblyComponentDefinition _assemblyComponentDefinition;
        private PartDocument _partDocument;
        private PartComponentDefinition _partComponentDefinition;
        private TransientGeometry _transientGeometry;
        private WorkPoint[] _workPoint = new WorkPoint[5];
        private Sketch3D _sketch3D;
        private SketchLine3D _3DLine;
        private List<SketchLine3D> _lines = new List <SketchLine3D>();
        private PlanarSketch _sketch2D;
        private WorkPlane _workPlane;
        private Point2d _origin;
        private Inventor.Point _modelPoint;
        private Profile _profile;
        private SweepFeature _sweep;
        private Inventor.Path _path;
        private WorkPoint _wp;
        private Status _status;
        private Connection _con;

    }

}
