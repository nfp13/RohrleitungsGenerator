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
using Microsoft.Office.Interop.Excel;
using Inventor;
using System.Numerics;

namespace ROhr2
{

    public partial class Form1 : Form
    {

        string FilePath = null;


        Analyze analyze;
        Status status;
        Speichern speichern;
        Data data;
        GeneratePipeSystem solver;
        Sweep sweep;
        Database database;

        Inventor.Application inventorApp;


        //Für Seiten wechseln
        List<Panel> listPanel = new List<Panel>();
        int index;

        public Form1()
        {
            status = new Status();
            data = new Data();
            solver = new GeneratePipeSystem(data);
            solver.Done += new EventHandler(PipesGenerated);
            //status.Progressed += new EventHandler(UpdateStatus);

            InitializeComponent();
            InitializeUI("UIMode");

            database = new Database();


            //Rund um die Comboboxen
            combb_fluid.DataSource = database.Fluids;
            combb_fluid.DisplayMember = "Name";
            combb_fluid.ValueMember = "Dichte";

            combb_material.DataSource = database.Werkstoffe;
            combb_material.DisplayMember = "Name";
            combb_material.ValueMember = "Dichte";

            combb_normrohr.DataSource = database.Normrohre;
            combb_normrohr.DisplayMember = "Außenradius";
            combb_normrohr.ValueMember = "Außenradius";

            combb_eigenschaften.SelectedIndex = 0;

            TestPipeGen();
        }

        private void TestPipeGen()
        {
            /*
            Cuboid Cube = new Cuboid(new Vector3(2, -2, -1), new Vector3(4, 2, 1));
            data.Cuboids.Add(Cube);
            data.SetMinSize();

            Pipe pipe = new Pipe(200000000000, 0.000012, 0.00001943, 0.00001943, 0.0001, 10, 200000000000, 0.5, 235000000, 0.1, 0.05, 0.5, 0);

            Connection.Flange flange1 = new Connection.Flange(new Vector3(0, 0, 0), new Vector3(1, 0, 0));
            Connection.Flange flange2 = new Connection.Flange(new Vector3(6, 0, 0), new Vector3(-1, 0, 0));

            Connection.Flange flange3 = new Connection.Flange(new Vector3(3, 0, 3), new Vector3(0, 0, -1));
            Connection.Flange flange4 = new Connection.Flange(new Vector3(3, 0, -3), new Vector3(0, 0, 1));


            Connection connection1 = new Connection(flange1, flange2, pipe);
            //Connection connection2 = new Connection(flange3, flange4, pipe);
            data.Connections.Add(connection1);
            //data.Connections.Add(connection2);

            solver.Solve();
            */
        }

        private void PipesGenerated(object sender, EventArgs e)     //will be triggert when the PipeSystem is generated
        {
            foreach (Connection con in solver.Finished)
            {
                sweep = new Sweep(inventorApp, FilePath, status, con);
                sweep.sketch3d();
                sweep.sketch2d();
                sweep.feature();
                sweep.addPart();
            }

            MessageBox.Show("Event Working!");

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            status.Name = "Inventor starting";
            status.OnProgess();

            //Start Inventor

            Type inventorAppType = System.Type.GetTypeFromProgID("Inventor.Application");
            inventorApp = System.Activator.CreateInstance(inventorAppType) as Inventor.Application;
            inventorApp.Visible = true;

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
                    solver.Stop();
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
                combb_flansch2.BindingContext = new BindingContext();

                txtb_datei.Text = FileName;

                analyze.SavePictureAs(speichern.getPathScreenX(), "X");
                pctb_x.Image = Image.FromFile(speichern.getPathScreenX());

                analyze.SavePictureAs(speichern.getPathScreenY(), "Y");
                pctb_y.Image = Image.FromFile(speichern.getPathScreenY());

                analyze.SavePictureAs(speichern.getPathScreenZ(), "Z");
                pctb_z.Image = Image.FromFile(speichern.getPathScreenZ());

                analyze.Hall(analyze.Parts.ElementAt(0));
                txtb_groesse.Text = (analyze.HallW * 10).ToString("0.0") + "/" + (analyze.HallL * 10).ToString("0.0") + "/" + (analyze.HallH * 10).ToString("0.0");
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


        private void btn_anwendung_Click(object sender, EventArgs e)
        {
            //selfmade hier alle So nur Normbögen aus textbox
            double R = Convert.ToDouble(txtb_rohrdurchmesser.Text);     //Außenradius
            double W = Convert.ToDouble(txtb_wandstaerke.Text);         //Wandstärke
            //mit Nikolas       //Selfmade
            double Q = database.Querschnittsfläche(R, W);
            double Bwm = database.GetBwm(R, W);                         //kein plan was das bei Pipe pipe ist
            double Twm = database.GetTwm(R, W);                         //kein plan was das bei Pipe pipe ist
            double B = Convert.ToDouble(txtb_biegeradius.Text);
            double E = database.Werkstoffe[combb_material.SelectedIndex].Emodul;
            //Selfmade
            double KP1 = database.Werkstoffe[combb_material.SelectedIndex].Wärmeausdehnung;
            double KP2 = database.Werkstoffe[combb_material.SelectedIndex].Mindestzugfestigkeit;
            double KP3 = database.Werkstoffe[combb_material.SelectedIndex].Dichte;
            double KP4 = database.Werkstoffe[combb_material.SelectedIndex].Schubmodul;
            double KP5 = database.Fluids[combb_fluid.SelectedIndex].Dichte;

            Pipe pipe = new Pipe(E, KP1, KP2, 0.00001943, 0.0001, 10, 200000000000, 0.5, 235000000, 0.1, 0.05, B, 0);

            Object selectedItem = combb_flansch1.SelectedItem;
            string selected = selectedItem.ToString();
            Connection.Flange flange1 = analyze.getFlangeFromPartName(selected);

            Object selectedItem1 = combb_flansch2.SelectedItem;
            string selected1 = selectedItem1.ToString();
            Connection.Flange flange2 = analyze.getFlangeFromPartName(selected1);

            Connection connection1 = new Connection(flange1, flange2, pipe);
            data.Connections.Add(connection1);

            analyze.GenerateCuboids();

            data.SetMinSize();

            solver.Solve();

        }

        private void btn_exportieren_Click(object sender, EventArgs e)
        {

        }

        private void combb_eigenschaften_SelectedIndexChanged(object sender, EventArgs e)
        {
            Object selectedItem1 = combb_eigenschaften.SelectedItem;
            string selected1 = selectedItem1.ToString();

            if (selected1 == "Normbögen")
            {
                combb_normrohr.Enabled = true;
                txtb_rohrdurchmesser.ReadOnly = true;
                txtb_wandstaerke.ReadOnly = true;
                txtb_biegeradius.ReadOnly = true;
            }
            else
            {
                combb_normrohr.Enabled = false;
                txtb_rohrdurchmesser.ReadOnly = false;
                txtb_wandstaerke.ReadOnly = false;
                txtb_biegeradius.ReadOnly = false;
            }

        }

        private void combb_normrohr_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtb_biegeradius.Text = database.Normrohre[combb_normrohr.SelectedIndex].Biegeradius.ToString();
            txtb_wandstaerke.Text = database.Normrohre[combb_normrohr.SelectedIndex].Wandstärke.ToString();
            txtb_rohrdurchmesser.Text = database.Normrohre[combb_normrohr.SelectedIndex].Außenradius.ToString();
        }

        private void txtb_rohrdurchmesser_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 46 && txtb_rohrdurchmesser.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }

            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }
    }

}
