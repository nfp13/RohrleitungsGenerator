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
using Inventor;

namespace ROhr2
{
    public partial class Form1 : Form
    {

        string FilePath = null;


        Analyze analyze;
        Status status;
        Speichern speichern;
        Data data;

        Inventor.Application inventorApp;


        //Für Seiten wechseln
        List<Panel> listPanel = new List<Panel>();
        int index;

        public Form1()
        {
            status = new Status();
            data = new Data();
            //status.Progressed += new EventHandler(UpdateStatus);

            InitializeComponent();
            InitializeUI("UIMode");
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            status.Name = "Inventor starting";
            status.OnProgess();

            //Start Inventor

            Type inventorAppType = System.Type.GetTypeFromProgID("Inventor.Application");
            inventorApp = System.Activator.CreateInstance(inventorAppType) as Inventor.Application;
            inventorApp.Visible = false;

            status.Name = "Done";
            status.OnProgess();

            //normteile = new Normteile();

            speichern = new Speichern(status);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                dynamic result = MessageBox.Show("Soll das Program beendet werden?", "Test Prog", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    inventorApp.Quit();
                    //normteile.CloseExcel();
                    System.Windows.Forms.Application.Exit();

                    //löschen
                    //speichern = new Speichern(status);
                    //speichern.deleteFiles();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void InitializeUI(string key)
        {
            try
            {
                var uiMode = ConfigurationManager.AppSettings[key];
                if (uiMode == "light")
                {
                    btn_changemode.Text = "Dark Mode";
                    this.ForeColor = System.Drawing.Color.FromArgb(47, 54, 64);
                    this.BackColor = System.Drawing.Color.FromArgb(245, 246, 250);
                    pnlleiste.BackColor = System.Drawing.Color.FromArgb(231, 231, 231);
                    ConfigurationManager.AppSettings[key] = "dark";
                }
                else
                {
                    btn_changemode.Text = "Light Mode";
                    this.ForeColor = System.Drawing.Color.FromArgb(245, 246, 250);
                    this.BackColor = System.Drawing.Color.FromArgb(29, 29, 29);
                    pnlleiste.BackColor = System.Drawing.Color.FromArgb(49, 49, 49);
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
            //Get FilePath of assembly

            speichern = new Speichern(status);
            string FileName = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Bitte Halle auswählen...";
            openFileDialog.Filter = "Inventor Assembly (*.iam) | *.iam";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FilePath = openFileDialog.FileName;
                FileName = openFileDialog.SafeFileName;
            }

            if (FilePath == null)
            {
                MessageBox.Show("No File Selected!");
            }
            else
            {
                //Open assembly and analyze, set comboboxes to Parts list
                analyze = new Analyze(inventorApp, FilePath, status, data);
                analyze.List();
                combb_flansch1.DataSource = analyze.Parts;
                combb_flansch2.DataSource = analyze.Parts;

                txtb_datei.Text = FileName;

                analyze.SavePictureAs(speichern.getPathScreenX(), "X");
                pctb_x.Image = Image.FromFile(speichern.getPathScreenX());

                analyze.SavePictureAs(speichern.getPathScreenY(), "Y");
                pctb_y.Image = Image.FromFile(speichern.getPathScreenY());

                analyze.SavePictureAs(speichern.getPathScreenZ(), "Z");
                pctb_z.Image = Image.FromFile(speichern.getPathScreenZ());

                analyze.Hall(analyze.Parts.ElementAt(0));
                txtb_groesse.Text = (analyze.HallW * 10).ToString() + "/" + (analyze.HallL * 10).ToString() + "/" + (analyze.HallH * 10).ToString();
            }
        }

        private void btn_weiter_Click(object sender, EventArgs e)
        {
            analyze.GenerateCuboids();
            foreach (Cuboid cube in data.Cuboids)
            {
                MessageBox.Show(cube.ToString());
            }
        }
    }

}
