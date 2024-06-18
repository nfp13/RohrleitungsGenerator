using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.SqlServer.Server;
using System.Xml;
using System.Linq;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net.NetworkInformation;
//using Microsoft.Office.Interop.Excel;
//using Inventor;

namespace ROhr2
{
    public partial class Form1 : Form
    {

        string FilePath = null;



        List<Panel> listPanel = new List<Panel>();
        int index;

        public Form1()
        {


            InitializeComponent();
            InitializeUI("UIMode");
        }

        private void InitializeUI(string key)
        {
            try
            {
                var uiMode = ConfigurationManager.AppSettings[key];
                if (uiMode == "light")
                {
                    btn_changemode.Text = "Dark Mode";
                    this.ForeColor = Color.FromArgb(47, 54, 64);
                    this.BackColor = Color.FromArgb(245, 246, 250);
                    pnlleiste.BackColor = Color.FromArgb(231, 231, 231);
                    ConfigurationManager.AppSettings[key] = "dark";
                }
                else
                {
                    btn_changemode.Text = "Light Mode";
                    this.ForeColor = Color.FromArgb(245, 246, 250);
                    this.BackColor = Color.FromArgb(29, 29, 29);
                    pnlleiste.BackColor = Color.FromArgb(49, 49, 49);
                    ConfigurationManager.AppSettings[key] = "light";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void btn_changemode_Click(object sender, EventArgs e)
        {
            InitializeUI("UIMode");
        }

        private void btn_zip_Click(object sender, EventArgs e)
        {
            if (btn_zip.Text == "")
            {
                btn_zip.Text = "X";
            }
            else
            {
                btn_zip.Text = "";
            }
        }

        private void btn_datei_Click(object sender, EventArgs e)
        {
            /*
            //kommt alles in unterprogramm

            public void Analyze()
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

            public void AnalyzeBoard(string boardOccurrenceName)
            {
                _status.Name = "Analyzing Board";
                _status.OnProgess();

                foreach (ComponentOccurrence componentOccurrence in _assemblyComponentDefinition.Occurrences)
                {
                    //Finding Board Assembly and calculating size, coordinates of centerpoint, holediameter and radius of corner

                    if (componentOccurrence.Name == boardOccurrenceName)
                    {
                        Box box = componentOccurrence.Definition.RangeBox;

                        _MinPointBoard = box.MinPoint;
                        _MaxPointBoard = box.MaxPoint;

                        BoardW = (box.MaxPoint.X - box.MinPoint.X);
                        BoardL = (box.MaxPoint.Y - box.MinPoint.Y);
                        BoardH = (box.MaxPoint.Z - box.MinPoint.Z);

                        _XOM = box.MinPoint.X + (BoardW / 2);
                        _YOM = box.MinPoint.Y + (BoardL / 2);
                        _ZOM = box.MinPoint.Z + (BoardH / 2);

                        AssemblyDocument boardAssemblyDocument;
                        boardAssemblyDocument = componentOccurrence.Definition.Document;

                        AssemblyComponentDefinition boardAssemblyComponentDefinition;
                        boardAssemblyComponentDefinition = boardAssemblyDocument.ComponentDefinition;

                        foreach (ComponentOccurrence componentOccurrence1 in boardAssemblyComponentDefinition.Occurrences)
                        {
                            if (componentOccurrence1.Name.Contains("STEP"))
                            {
                                PartDocument boardPartDocument;
                                boardPartDocument = componentOccurrence1.Definition.Document;

                                _status.Progress = 60;
                                _status.OnProgess();

                                foreach (HoleFeature holeFeature in boardPartDocument.ComponentDefinition.Features.HoleFeatures)
                                {
                                    HoleDia = holeFeature.HoleDiameter.Value;

                                    Inventor.Point minPointHole = holeFeature.RangeBox.MinPoint;

                                    if (_MinPointBoard.VectorTo(minPointHole).X + HoleDia / 2 < CornerRadius || CornerRadius == 0)
                                    {
                                        CornerRadius = _MinPointBoard.VectorTo(minPointHole).X + HoleDia / 2;
                                    }
                                }
                            }
                        }
                    }
                    _status.Progress = 100;
                    _status.Name = "Done";
                    _status.OnProgess();
                }

                //Calculating component height for bottom and top of PCB

                _MaxPointGes = _assemblyComponentDefinition.RangeBox.MaxPoint;
                _MinPointGes = _assemblyComponentDefinition.RangeBox.MinPoint;

                CompHeightBottom = _MinPointGes.VectorTo(_MinPointBoard).Z;
                CompHeightTop = _MaxPointBoard.VectorTo(_MaxPointGes).Z;

                _status.Name = "Done";
                _status.OnProgess();

            }

            */

        }
    }
}
