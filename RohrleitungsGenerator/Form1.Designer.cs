namespace ROhr2
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panel1 = new Panel();
            txtb_groesse = new TextBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            pctb_z = new PictureBox();
            pctb_y = new PictureBox();
            txtb_datei = new TextBox();
            label18 = new Label();
            btn_datei = new Button();
            pctb_x = new PictureBox();
            label15 = new Label();
            lblueberschrift1 = new Label();
            label1 = new Label();
            pnlleiste = new Panel();
            label16 = new Label();
            progb_inventor = new ProgressBar();
            btn_changemode = new Button();
            btn_weiter = new Button();
            btn_zurueck = new Button();
            panel3 = new Panel();
            label19 = new Label();
            pctb_rohrleitungx = new PictureBox();
            btn_zip = new Button();
            btn_exportieren = new Button();
            btn_speicherort = new Button();
            pctb_rohrleitungy = new PictureBox();
            label17 = new Label();
            label22 = new Label();
            label23 = new Label();
            label33 = new Label();
            panel2 = new Panel();
            btn_verbindungentf = new Button();
            btn_verbindunghinzu = new Button();
            btn_anwendung = new Button();
            btn_supports = new Button();
            label24 = new Label();
            txtb_druck = new TextBox();
            label14 = new Label();
            combb_fluid = new ComboBox();
            label13 = new Label();
            txtb_temperatur = new TextBox();
            combb_material = new ComboBox();
            label12 = new Label();
            label8 = new Label();
            combb_gefaelle = new ComboBox();
            label7 = new Label();
            txtb_biegeradius = new TextBox();
            txtb_wandstaerke = new TextBox();
            label28 = new Label();
            label27 = new Label();
            txtb_rohrdurchmesser = new TextBox();
            label6 = new Label();
            combb_flansch2 = new ComboBox();
            combb_flansch1 = new ComboBox();
            combb_verbindung = new ComboBox();
            combb_normrohr = new ComboBox();
            combb_eigenschaften = new ComboBox();
            label2 = new Label();
            label9 = new Label();
            label10 = new Label();
            label31 = new Label();
            label32 = new Label();
            label11 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pctb_z).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pctb_y).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pctb_x).BeginInit();
            pnlleiste.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pctb_rohrleitungx).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pctb_rohrleitungy).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(txtb_groesse);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(pctb_z);
            panel1.Controls.Add(pctb_y);
            panel1.Controls.Add(txtb_datei);
            panel1.Controls.Add(label18);
            panel1.Controls.Add(btn_datei);
            panel1.Controls.Add(pctb_x);
            panel1.Controls.Add(label15);
            panel1.Controls.Add(lblueberschrift1);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(14, 2);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(817, 415);
            panel1.TabIndex = 12;
            // 
            // txtb_groesse
            // 
            txtb_groesse.BackColor = SystemColors.Control;
            txtb_groesse.Location = new Point(689, 81);
            txtb_groesse.Margin = new Padding(4, 3, 4, 3);
            txtb_groesse.Name = "txtb_groesse";
            txtb_groesse.ReadOnly = true;
            txtb_groesse.Size = new Size(93, 23);
            txtb_groesse.TabIndex = 36;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(595, 329);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(87, 15);
            label5.TabIndex = 35;
            label5.Text = "Halle Ansicht Z";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(321, 329);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(87, 15);
            label4.TabIndex = 34;
            label4.Text = "Halle Ansicht Y";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(35, 329);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(87, 15);
            label3.TabIndex = 33;
            label3.Text = "Halle Ansicht X";
            // 
            // pctb_z
            // 
            pctb_z.ImageLocation = "";
            pctb_z.Location = new Point(595, 138);
            pctb_z.Margin = new Padding(4, 3, 4, 3);
            pctb_z.Name = "pctb_z";
            pctb_z.Size = new Size(187, 185);
            pctb_z.SizeMode = PictureBoxSizeMode.Zoom;
            pctb_z.TabIndex = 32;
            pctb_z.TabStop = false;
            // 
            // pctb_y
            // 
            pctb_y.ImageLocation = "";
            pctb_y.Location = new Point(321, 138);
            pctb_y.Margin = new Padding(4, 3, 4, 3);
            pctb_y.Name = "pctb_y";
            pctb_y.Size = new Size(187, 185);
            pctb_y.SizeMode = PictureBoxSizeMode.Zoom;
            pctb_y.TabIndex = 31;
            pctb_y.TabStop = false;
            // 
            // txtb_datei
            // 
            txtb_datei.BackColor = SystemColors.Control;
            txtb_datei.Location = new Point(391, 81);
            txtb_datei.Margin = new Padding(4, 3, 4, 3);
            txtb_datei.Name = "txtb_datei";
            txtb_datei.ReadOnly = true;
            txtb_datei.Size = new Size(93, 23);
            txtb_datei.TabIndex = 28;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(321, 87);
            label18.Margin = new Padding(4, 0, 4, 0);
            label18.Name = "label18";
            label18.Size = new Size(37, 15);
            label18.TabIndex = 27;
            label18.Text = "Datei:";
            // 
            // btn_datei
            // 
            btn_datei.AutoSize = true;
            btn_datei.FlatStyle = FlatStyle.Flat;
            btn_datei.Location = new Point(187, 81);
            btn_datei.Margin = new Padding(4, 3, 4, 3);
            btn_datei.Name = "btn_datei";
            btn_datei.Size = new Size(31, 31);
            btn_datei.TabIndex = 16;
            btn_datei.Text = "+";
            btn_datei.UseVisualStyleBackColor = true;
            btn_datei.Click += btn_datei_Click;
            // 
            // pctb_x
            // 
            pctb_x.ImageLocation = "";
            pctb_x.Location = new Point(35, 138);
            pctb_x.Margin = new Padding(4, 3, 4, 3);
            pctb_x.Name = "pctb_x";
            pctb_x.Size = new Size(187, 185);
            pctb_x.SizeMode = PictureBoxSizeMode.Zoom;
            pctb_x.TabIndex = 15;
            pctb_x.TabStop = false;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(595, 87);
            label15.Margin = new Padding(4, 0, 4, 0);
            label15.Name = "label15";
            label15.Size = new Size(72, 15);
            label15.TabIndex = 13;
            label15.Text = "Größe Halle:";
            // 
            // lblueberschrift1
            // 
            lblueberschrift1.AutoSize = true;
            lblueberschrift1.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblueberschrift1.Location = new Point(286, 23);
            lblueberschrift1.Margin = new Padding(4, 0, 4, 0);
            lblueberschrift1.Name = "lblueberschrift1";
            lblueberschrift1.Size = new Size(227, 24);
            lblueberschrift1.TabIndex = 10;
            lblueberschrift1.Text = "Rohrleitungs Generator";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(35, 87);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(96, 15);
            label1.TabIndex = 1;
            label1.Text = "Datei hochladen:";
            // 
            // pnlleiste
            // 
            pnlleiste.BackColor = SystemColors.Control;
            pnlleiste.Controls.Add(label16);
            pnlleiste.Controls.Add(progb_inventor);
            pnlleiste.Controls.Add(btn_changemode);
            pnlleiste.Controls.Add(btn_weiter);
            pnlleiste.Controls.Add(btn_zurueck);
            pnlleiste.Location = new Point(14, 418);
            pnlleiste.Margin = new Padding(4, 3, 4, 3);
            pnlleiste.Name = "pnlleiste";
            pnlleiste.Size = new Size(817, 46);
            pnlleiste.TabIndex = 13;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(158, 16);
            label16.Margin = new Padding(4, 0, 4, 0);
            label16.Name = "label16";
            label16.Size = new Size(51, 15);
            label16.TabIndex = 25;
            label16.Text = "Inventor";
            // 
            // progb_inventor
            // 
            progb_inventor.Location = new Point(35, 12);
            progb_inventor.Margin = new Padding(4, 3, 4, 3);
            progb_inventor.Name = "progb_inventor";
            progb_inventor.Size = new Size(117, 23);
            progb_inventor.TabIndex = 16;
            // 
            // btn_changemode
            // 
            btn_changemode.AutoSize = true;
            btn_changemode.FlatStyle = FlatStyle.Flat;
            btn_changemode.Location = new Point(365, 9);
            btn_changemode.Margin = new Padding(4, 3, 4, 3);
            btn_changemode.Name = "btn_changemode";
            btn_changemode.Size = new Size(88, 31);
            btn_changemode.TabIndex = 14;
            btn_changemode.Text = "Mode";
            btn_changemode.UseVisualStyleBackColor = true;
            btn_changemode.Click += btn_changemode_Click;
            // 
            // btn_weiter
            // 
            btn_weiter.AutoSize = true;
            btn_weiter.FlatStyle = FlatStyle.Flat;
            btn_weiter.Location = new Point(694, 9);
            btn_weiter.Margin = new Padding(4, 3, 4, 3);
            btn_weiter.Name = "btn_weiter";
            btn_weiter.Size = new Size(88, 31);
            btn_weiter.TabIndex = 15;
            btn_weiter.Text = "weiter";
            btn_weiter.UseVisualStyleBackColor = true;
            btn_weiter.Click += btn_weiter_Click;
            // 
            // btn_zurueck
            // 
            btn_zurueck.AutoSize = true;
            btn_zurueck.FlatStyle = FlatStyle.Flat;
            btn_zurueck.Location = new Point(595, 9);
            btn_zurueck.Margin = new Padding(4, 3, 4, 3);
            btn_zurueck.Name = "btn_zurueck";
            btn_zurueck.Size = new Size(88, 31);
            btn_zurueck.TabIndex = 15;
            btn_zurueck.Text = "zurück";
            btn_zurueck.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            panel3.Controls.Add(label19);
            panel3.Controls.Add(pctb_rohrleitungx);
            panel3.Controls.Add(btn_zip);
            panel3.Controls.Add(btn_exportieren);
            panel3.Controls.Add(btn_speicherort);
            panel3.Controls.Add(pctb_rohrleitungy);
            panel3.Controls.Add(label17);
            panel3.Controls.Add(label22);
            panel3.Controls.Add(label23);
            panel3.Controls.Add(label33);
            panel3.Location = new Point(838, 425);
            panel3.Margin = new Padding(4, 3, 4, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(817, 415);
            panel3.TabIndex = 26;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(397, 277);
            label19.Margin = new Padding(4, 0, 4, 0);
            label19.Name = "label19";
            label19.Size = new Size(79, 15);
            label19.TabIndex = 29;
            label19.Text = "Rohrleitung X";
            // 
            // pctb_rohrleitungx
            // 
            pctb_rohrleitungx.Location = new Point(397, 87);
            pctb_rohrleitungx.Margin = new Padding(4, 3, 4, 3);
            pctb_rohrleitungx.Name = "pctb_rohrleitungx";
            pctb_rohrleitungx.Size = new Size(187, 185);
            pctb_rohrleitungx.SizeMode = PictureBoxSizeMode.Zoom;
            pctb_rohrleitungx.TabIndex = 28;
            pctb_rohrleitungx.TabStop = false;
            // 
            // btn_zip
            // 
            btn_zip.AutoSize = true;
            btn_zip.FlatStyle = FlatStyle.Flat;
            btn_zip.Location = new Point(187, 138);
            btn_zip.Margin = new Padding(4, 3, 4, 3);
            btn_zip.Name = "btn_zip";
            btn_zip.Size = new Size(29, 29);
            btn_zip.TabIndex = 27;
            btn_zip.UseVisualStyleBackColor = true;
            btn_zip.Click += btn_zip_Click;
            // 
            // btn_exportieren
            // 
            btn_exportieren.AutoSize = true;
            btn_exportieren.FlatStyle = FlatStyle.Flat;
            btn_exportieren.Location = new Point(70, 242);
            btn_exportieren.Margin = new Padding(4, 3, 4, 3);
            btn_exportieren.Name = "btn_exportieren";
            btn_exportieren.Size = new Size(146, 31);
            btn_exportieren.TabIndex = 24;
            btn_exportieren.Text = "Exportieren";
            btn_exportieren.UseVisualStyleBackColor = true;
            btn_exportieren.Click += btn_exportieren_Click;
            // 
            // btn_speicherort
            // 
            btn_speicherort.AutoSize = true;
            btn_speicherort.FlatStyle = FlatStyle.Flat;
            btn_speicherort.Image = (Image)resources.GetObject("btn_speicherort.Image");
            btn_speicherort.ImageAlign = ContentAlignment.TopLeft;
            btn_speicherort.Location = new Point(187, 81);
            btn_speicherort.Margin = new Padding(4, 3, 4, 3);
            btn_speicherort.Name = "btn_speicherort";
            btn_speicherort.Size = new Size(31, 31);
            btn_speicherort.TabIndex = 16;
            btn_speicherort.Text = "+";
            btn_speicherort.UseVisualStyleBackColor = true;
            // 
            // pctb_rohrleitungy
            // 
            pctb_rohrleitungy.Location = new Point(595, 87);
            pctb_rohrleitungy.Margin = new Padding(4, 3, 4, 3);
            pctb_rohrleitungy.Name = "pctb_rohrleitungy";
            pctb_rohrleitungy.Size = new Size(187, 185);
            pctb_rohrleitungy.SizeMode = PictureBoxSizeMode.Zoom;
            pctb_rohrleitungy.TabIndex = 15;
            pctb_rohrleitungy.TabStop = false;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(595, 277);
            label17.Margin = new Padding(4, 0, 4, 0);
            label17.Name = "label17";
            label17.Size = new Size(79, 15);
            label17.TabIndex = 13;
            label17.Text = "Rohrleitung Y";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label22.Location = new Point(286, 23);
            label22.Margin = new Padding(4, 0, 4, 0);
            label22.Name = "label22";
            label22.Size = new Size(119, 24);
            label22.TabIndex = 10;
            label22.Text = "Exportieren";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(35, 144);
            label23.Margin = new Padding(4, 0, 4, 0);
            label23.Name = "label23";
            label23.Size = new Size(109, 15);
            label23.TabIndex = 9;
            label23.Text = "Als ZIP exportieren:";
            // 
            // label33
            // 
            label33.AutoSize = true;
            label33.Location = new Point(35, 87);
            label33.Margin = new Padding(4, 0, 4, 0);
            label33.Name = "label33";
            label33.Size = new Size(111, 15);
            label33.TabIndex = 1;
            label33.Text = "Speicherort wählen:";
            // 
            // panel2
            // 
            panel2.Controls.Add(btn_verbindungentf);
            panel2.Controls.Add(btn_verbindunghinzu);
            panel2.Controls.Add(btn_anwendung);
            panel2.Controls.Add(btn_supports);
            panel2.Controls.Add(label24);
            panel2.Controls.Add(txtb_druck);
            panel2.Controls.Add(label14);
            panel2.Controls.Add(combb_fluid);
            panel2.Controls.Add(label13);
            panel2.Controls.Add(txtb_temperatur);
            panel2.Controls.Add(combb_material);
            panel2.Controls.Add(label12);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(combb_gefaelle);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(txtb_biegeradius);
            panel2.Controls.Add(txtb_wandstaerke);
            panel2.Controls.Add(label28);
            panel2.Controls.Add(label27);
            panel2.Controls.Add(txtb_rohrdurchmesser);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(combb_flansch2);
            panel2.Controls.Add(combb_flansch1);
            panel2.Controls.Add(combb_verbindung);
            panel2.Controls.Add(combb_normrohr);
            panel2.Controls.Add(combb_eigenschaften);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(label10);
            panel2.Controls.Add(label31);
            panel2.Controls.Add(label32);
            panel2.Controls.Add(label11);
            panel2.Location = new Point(838, 2);
            panel2.Margin = new Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(817, 415);
            panel2.TabIndex = 27;
            // 
            // btn_verbindungentf
            // 
            btn_verbindungentf.AutoSize = true;
            btn_verbindungentf.FlatStyle = FlatStyle.Flat;
            btn_verbindungentf.Location = new Point(217, 71);
            btn_verbindungentf.Margin = new Padding(4, 3, 4, 3);
            btn_verbindungentf.Name = "btn_verbindungentf";
            btn_verbindungentf.Size = new Size(156, 31);
            btn_verbindungentf.TabIndex = 55;
            btn_verbindungentf.Text = "Verbindung entfernen";
            btn_verbindungentf.UseVisualStyleBackColor = true;
            // 
            // btn_verbindunghinzu
            // 
            btn_verbindunghinzu.AutoSize = true;
            btn_verbindunghinzu.FlatStyle = FlatStyle.Flat;
            btn_verbindunghinzu.Location = new Point(38, 73);
            btn_verbindunghinzu.Margin = new Padding(4, 3, 4, 3);
            btn_verbindunghinzu.Name = "btn_verbindunghinzu";
            btn_verbindunghinzu.Size = new Size(167, 31);
            btn_verbindunghinzu.TabIndex = 54;
            btn_verbindunghinzu.Text = "Verbindung hinzufügen";
            btn_verbindunghinzu.UseVisualStyleBackColor = true;
            // 
            // btn_anwendung
            // 
            btn_anwendung.AutoSize = true;
            btn_anwendung.FlatStyle = FlatStyle.Flat;
            btn_anwendung.Location = new Point(76, 293);
            btn_anwendung.Margin = new Padding(4, 3, 4, 3);
            btn_anwendung.Name = "btn_anwendung";
            btn_anwendung.Size = new Size(146, 31);
            btn_anwendung.TabIndex = 53;
            btn_anwendung.Text = "Anwendung";
            btn_anwendung.UseVisualStyleBackColor = true;
            btn_anwendung.Click += btn_anwendung_Click;
            // 
            // btn_supports
            // 
            btn_supports.AutoSize = true;
            btn_supports.FlatStyle = FlatStyle.Flat;
            btn_supports.Location = new Point(192, 257);
            btn_supports.Margin = new Padding(4, 3, 4, 3);
            btn_supports.Name = "btn_supports";
            btn_supports.Size = new Size(29, 29);
            btn_supports.TabIndex = 52;
            btn_supports.UseVisualStyleBackColor = true;
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new Point(41, 263);
            label24.Margin = new Padding(4, 0, 4, 0);
            label24.Name = "label24";
            label24.Size = new Size(107, 15);
            label24.TabIndex = 51;
            label24.Text = "Supports einfügen:";
            // 
            // txtb_druck
            // 
            txtb_druck.BackColor = SystemColors.Control;
            txtb_druck.Location = new Point(679, 269);
            txtb_druck.Margin = new Padding(4, 3, 4, 3);
            txtb_druck.Name = "txtb_druck";
            txtb_druck.Size = new Size(116, 23);
            txtb_druck.TabIndex = 50;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(582, 272);
            label14.Margin = new Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new Size(41, 15);
            label14.TabIndex = 49;
            label14.Text = "Druck:";
            // 
            // combb_fluid
            // 
            combb_fluid.BackColor = SystemColors.Control;
            combb_fluid.DropDownStyle = ComboBoxStyle.DropDownList;
            combb_fluid.FormattingEnabled = true;
            combb_fluid.Items.AddRange(new object[] { "Gas", "Wasser", "Öl" });
            combb_fluid.Location = new Point(679, 197);
            combb_fluid.Margin = new Padding(4, 3, 4, 3);
            combb_fluid.Name = "combb_fluid";
            combb_fluid.Size = new Size(116, 23);
            combb_fluid.TabIndex = 48;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(590, 203);
            label13.Margin = new Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new Size(36, 15);
            label13.TabIndex = 47;
            label13.Text = "Fluid:";
            // 
            // txtb_temperatur
            // 
            txtb_temperatur.BackColor = SystemColors.Control;
            txtb_temperatur.Location = new Point(679, 299);
            txtb_temperatur.Margin = new Padding(4, 3, 4, 3);
            txtb_temperatur.Name = "txtb_temperatur";
            txtb_temperatur.Size = new Size(116, 23);
            txtb_temperatur.TabIndex = 46;
            // 
            // combb_material
            // 
            combb_material.BackColor = SystemColors.Control;
            combb_material.DropDownStyle = ComboBoxStyle.DropDownList;
            combb_material.FormattingEnabled = true;
            combb_material.Items.AddRange(new object[] { "Stahllegierung", "Polycarbonat" });
            combb_material.Location = new Point(679, 159);
            combb_material.Margin = new Padding(4, 3, 4, 3);
            combb_material.Name = "combb_material";
            combb_material.Size = new Size(116, 23);
            combb_material.TabIndex = 45;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(590, 171);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(53, 15);
            label12.TabIndex = 44;
            label12.Text = "Material:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(554, 302);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(119, 15);
            label8.TabIndex = 42;
            label8.Text = "Temperatur differenz:";
            // 
            // combb_gefaelle
            // 
            combb_gefaelle.BackColor = SystemColors.Control;
            combb_gefaelle.DropDownStyle = ComboBoxStyle.DropDownList;
            combb_gefaelle.FormattingEnabled = true;
            combb_gefaelle.Items.AddRange(new object[] { "Egal", "1%", "3%", "5%" });
            combb_gefaelle.Location = new Point(679, 233);
            combb_gefaelle.Margin = new Padding(4, 3, 4, 3);
            combb_gefaelle.Name = "combb_gefaelle";
            combb_gefaelle.Size = new Size(116, 23);
            combb_gefaelle.TabIndex = 41;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(586, 242);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(46, 15);
            label7.TabIndex = 40;
            label7.Text = "Gefälle:";
            // 
            // txtb_biegeradius
            // 
            txtb_biegeradius.BackColor = SystemColors.Control;
            txtb_biegeradius.Location = new Point(419, 300);
            txtb_biegeradius.Margin = new Padding(4, 3, 4, 3);
            txtb_biegeradius.Name = "txtb_biegeradius";
            txtb_biegeradius.Size = new Size(116, 23);
            txtb_biegeradius.TabIndex = 39;
            // 
            // txtb_wandstaerke
            // 
            txtb_wandstaerke.BackColor = SystemColors.Control;
            txtb_wandstaerke.Location = new Point(419, 265);
            txtb_wandstaerke.Margin = new Padding(4, 3, 4, 3);
            txtb_wandstaerke.Name = "txtb_wandstaerke";
            txtb_wandstaerke.Size = new Size(116, 23);
            txtb_wandstaerke.TabIndex = 38;
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Location = new Point(289, 303);
            label28.Margin = new Padding(4, 0, 4, 0);
            label28.Name = "label28";
            label28.Size = new Size(71, 15);
            label28.TabIndex = 37;
            label28.Text = "Biegeradius:";
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Location = new Point(289, 269);
            label27.Margin = new Padding(4, 0, 4, 0);
            label27.Name = "label27";
            label27.Size = new Size(72, 15);
            label27.TabIndex = 36;
            label27.Text = "Wandstärke:";
            // 
            // txtb_rohrdurchmesser
            // 
            txtb_rohrdurchmesser.BackColor = SystemColors.Control;
            txtb_rohrdurchmesser.Location = new Point(419, 235);
            txtb_rohrdurchmesser.Margin = new Padding(4, 3, 4, 3);
            txtb_rohrdurchmesser.Name = "txtb_rohrdurchmesser";
            txtb_rohrdurchmesser.Size = new Size(116, 23);
            txtb_rohrdurchmesser.TabIndex = 35;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(289, 235);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(103, 15);
            label6.TabIndex = 34;
            label6.Text = "Rohrdurchmesser:";
            // 
            // combb_flansch2
            // 
            combb_flansch2.BackColor = SystemColors.Control;
            combb_flansch2.DropDownStyle = ComboBoxStyle.DropDownList;
            combb_flansch2.FormattingEnabled = true;
            combb_flansch2.Items.AddRange(new object[] { "Bauteil 1", "Bauteil 2", "Bauteil 3" });
            combb_flansch2.Location = new Point(105, 203);
            combb_flansch2.Margin = new Padding(4, 3, 4, 3);
            combb_flansch2.Name = "combb_flansch2";
            combb_flansch2.Size = new Size(116, 23);
            combb_flansch2.TabIndex = 33;
            // 
            // combb_flansch1
            // 
            combb_flansch1.BackColor = SystemColors.Control;
            combb_flansch1.DropDownStyle = ComboBoxStyle.DropDownList;
            combb_flansch1.FormattingEnabled = true;
            combb_flansch1.Items.AddRange(new object[] { "Bauteil 1", "Bauteil 2", "Bauteil 3" });
            combb_flansch1.Location = new Point(105, 156);
            combb_flansch1.Margin = new Padding(4, 3, 4, 3);
            combb_flansch1.Name = "combb_flansch1";
            combb_flansch1.Size = new Size(116, 23);
            combb_flansch1.TabIndex = 32;
            // 
            // combb_verbindung
            // 
            combb_verbindung.BackColor = SystemColors.Control;
            combb_verbindung.DropDownStyle = ComboBoxStyle.DropDownList;
            combb_verbindung.FormattingEnabled = true;
            combb_verbindung.Items.AddRange(new object[] { "Verbindung 1", "Verbindung 2", "Verbindung 3" });
            combb_verbindung.Location = new Point(105, 114);
            combb_verbindung.Margin = new Padding(4, 3, 4, 3);
            combb_verbindung.Name = "combb_verbindung";
            combb_verbindung.Size = new Size(116, 23);
            combb_verbindung.TabIndex = 31;
            // 
            // combb_normrohr
            // 
            combb_normrohr.BackColor = SystemColors.Control;
            combb_normrohr.DropDownStyle = ComboBoxStyle.DropDownList;
            combb_normrohr.FormattingEnabled = true;
            combb_normrohr.Items.AddRange(new object[] { "Normrohr 1", "Normrohr 2" });
            combb_normrohr.Location = new Point(419, 197);
            combb_normrohr.Margin = new Padding(4, 3, 4, 3);
            combb_normrohr.Name = "combb_normrohr";
            combb_normrohr.Size = new Size(116, 23);
            combb_normrohr.TabIndex = 30;
            combb_normrohr.SelectedIndexChanged += combb_normrohr_SelectedIndexChanged;
            // 
            // combb_eigenschaften
            // 
            combb_eigenschaften.AutoCompleteCustomSource.AddRange(new string[] { "Normbögen", "Biegeradius" });
            combb_eigenschaften.BackColor = SystemColors.Control;
            combb_eigenschaften.DropDownStyle = ComboBoxStyle.DropDownList;
            combb_eigenschaften.FormattingEnabled = true;
            combb_eigenschaften.Items.AddRange(new object[] { "Normbögen", "selbst definiert" });
            combb_eigenschaften.Location = new Point(419, 159);
            combb_eigenschaften.Margin = new Padding(4, 3, 4, 3);
            combb_eigenschaften.Name = "combb_eigenschaften";
            combb_eigenschaften.Size = new Size(116, 23);
            combb_eigenschaften.TabIndex = 29;
            combb_eigenschaften.SelectedIndexChanged += combb_eigenschaften_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(35, 209);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(59, 15);
            label2.TabIndex = 28;
            label2.Text = "Flansch 2:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(35, 162);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(59, 15);
            label9.TabIndex = 27;
            label9.Text = "Flansch 1:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(35, 120);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(71, 15);
            label10.TabIndex = 26;
            label10.Text = "Verbindung:";
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Location = new Point(289, 201);
            label31.Margin = new Padding(4, 0, 4, 0);
            label31.Name = "label31";
            label31.Size = new Size(122, 15);
            label31.TabIndex = 25;
            label31.Text = "Normrohr auswählen:";
            // 
            // label32
            // 
            label32.AutoSize = true;
            label32.Location = new Point(289, 163);
            label32.Margin = new Padding(4, 0, 4, 0);
            label32.Name = "label32";
            label32.Size = new Size(84, 15);
            label32.TabIndex = 24;
            label32.Text = "Eigenschaften:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label11.Location = new Point(286, 23);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(138, 24);
            label11.TabIndex = 10;
            label11.Text = "Einstellungen";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1695, 861);
            Controls.Add(panel2);
            Controls.Add(panel3);
            Controls.Add(panel1);
            Controls.Add(pnlleiste);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            Shown += Form1_Shown;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pctb_z).EndInit();
            ((System.ComponentModel.ISupportInitialize)pctb_y).EndInit();
            ((System.ComponentModel.ISupportInitialize)pctb_x).EndInit();
            pnlleiste.ResumeLayout(false);
            pnlleiste.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pctb_rohrleitungx).EndInit();
            ((System.ComponentModel.ISupportInitialize)pctb_rohrleitungy).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtb_datei;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btn_datei;
        private System.Windows.Forms.PictureBox pctb_x;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblueberschrift1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlleiste;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ProgressBar progb_inventor;
        private System.Windows.Forms.Button btn_changemode;
        private System.Windows.Forms.Button btn_weiter;
        private System.Windows.Forms.Button btn_zurueck;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.PictureBox pctb_rohrleitungx;
        private System.Windows.Forms.Button btn_zip;
        private System.Windows.Forms.Button btn_exportieren;
        private System.Windows.Forms.Button btn_speicherort;
        private System.Windows.Forms.PictureBox pctb_rohrleitungy;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox combb_flansch2;
        private System.Windows.Forms.ComboBox combb_flansch1;
        private System.Windows.Forms.ComboBox combb_verbindung;
        private System.Windows.Forms.ComboBox combb_normrohr;
        private System.Windows.Forms.ComboBox combb_eigenschaften;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox pctb_z;
        private System.Windows.Forms.PictureBox pctb_y;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtb_biegeradius;
        private System.Windows.Forms.TextBox txtb_wandstaerke;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtb_rohrdurchmesser;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox combb_gefaelle;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtb_druck;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox combb_fluid;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtb_temperatur;
        private System.Windows.Forms.ComboBox combb_material;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_anwendung;
        private System.Windows.Forms.Button btn_supports;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button btn_verbindunghinzu;
        private System.Windows.Forms.Button btn_verbindungentf;
        private TextBox txtb_groesse;
    }
}

