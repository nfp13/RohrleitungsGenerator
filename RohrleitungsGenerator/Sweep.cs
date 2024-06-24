//using Microsoft.Office.Interop.Excel;
using Inventor;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ROhr2
{
    public class Sweep
    {
        public Sweep(Inventor.Application inventorApp, string filePath, Status status)
        {
            _status = status;
            _inventorApp = inventorApp;

            _status.Name = "Opening Assembly";
            _status.OnProgess();

            //Opening the PCB assembly Document

            _assemblyDocument = _inventorApp.Documents.Open(_filePath, true) as AssemblyDocument;
            _assemblyComponentDefinition = _assemblyDocument.ComponentDefinition;
            _transientGeometry = inventorApp.TransientGeometry;

            _status.Name = "Done";
            _status.OnProgess();
        }

        public void sketch3d()
        {
            _workPoint[0] = _assemblyComponentDefinition.WorkPoints.AddFixed(_transientGeometry.CreatePoint(0, 0, 0));
            _workPoint[1] = _assemblyComponentDefinition.WorkPoints.AddFixed(_transientGeometry.CreatePoint(3, 0, 0));
            _workPoint[2] = _assemblyComponentDefinition.WorkPoints.AddFixed(_transientGeometry.CreatePoint(3, 4, 0));
            _workPoint[3] = _assemblyComponentDefinition.WorkPoints.AddFixed(_transientGeometry.CreatePoint(3, 4, 2));
            _workPoint[4] = _assemblyComponentDefinition.WorkPoints.AddFixed(_transientGeometry.CreatePoint(6, 4, 2));
            
            //_sketch3D = _assemblyComponentDefinition.Sketches3D.Add;

            _3DLine = _sketch3D.SketchLines3D.AddByTwoPoints(_workPoint[0], _workPoint[1], true, 1);

            for (int i = 1; i < 5; i++)
            {
                _3DLine = _sketch3D.SketchLines3D.AddByTwoPoints(_3DLine.EndPoint, _workPoint[i], true, 1);
            }
        }

        public void sketch2d()
        {
            _sketch2D = _assemblyComponentDefinition.Sketches.Add(_workPlane);
            _origin = _sketch2D.ModelToSketchSpace(_transientGeometry.CreatePoint(0, 0, 0));
            _sketch2D.SketchCircles.AddByCenterRadius(_sketch2D.ModelToSketchSpace(_modelPoint), 0.375);
            _profile = _sketch2D.Profiles.AddForSolid();

        }

        public void feature()
        {
            _path = _assemblyComponentDefinition.Features.CreatePath(_3DLine);
            _sweep = _assemblyComponentDefinition.Features.SweepFeatures.AddUsingPath(_profile, _path, Inventor.PartFeatureOperationEnum.kJoinOperation);

        }

        private Inventor.Application _inventorApp;
        private string _filePath;
        private AssemblyDocument _assemblyDocument;
        private AssemblyComponentDefinition _assemblyComponentDefinition;
        private TransientGeometry _transientGeometry;
        private WorkPoint[] _workPoint = new WorkPoint[5];
        private Sketch3D _sketch3D;
        private SketchLine3D _3DLine;
        private PlanarSketch _sketch2D;
        private WorkPlane _workPlane;
        private Point2d _origin;
        private Inventor.Point _modelPoint;
        private Profile _profile;
        private SweepFeature _sweep;
        private Inventor.Path _path;

        private Status _status;

    }

}
