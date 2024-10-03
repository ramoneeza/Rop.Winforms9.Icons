namespace Rop.Winforms8._1.DoutoneIconBuilder
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            btntest = new Button();
            barra = new ProgressBar();
            btnsavebin = new Button();
            btnclose = new Button();
            splitContainer1 = new SplitContainer();
            listado = new ListBox();
            panel5 = new Panel();
            label1 = new Label();
            cbfilter = new ComboBox();
            panelmain = new Panel();
            lienzo = new Lienzo();
            panel4 = new Panel();
            cbcolor = new ComboBox();
            btnalias = new Button();
            ejemplo = new Ejemplo();
            edbaseline = new NumericUpDown();
            btncancel = new Button();
            btneditsave = new Button();
            panel2 = new Panel();
            lbbaseline = new Label();
            lbposition = new Label();
            lbiconname = new Label();
            panel3 = new Panel();
            btntop = new Button();
            labelcabecera = new Label();
            btnupgrade = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panel5.SuspendLayout();
            panelmain.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)edbaseline).BeginInit();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(btnupgrade);
            panel1.Controls.Add(btntest);
            panel1.Controls.Add(barra);
            panel1.Controls.Add(btnsavebin);
            panel1.Controls.Add(btnclose);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 738);
            panel1.Name = "panel1";
            panel1.Size = new Size(1192, 31);
            panel1.TabIndex = 0;
            // 
            // btntest
            // 
            btntest.Location = new Point(987, 4);
            btntest.Name = "btntest";
            btntest.Size = new Size(75, 23);
            btntest.TabIndex = 3;
            btntest.Text = "Test";
            btntest.UseVisualStyleBackColor = true;
            // 
            // barra
            // 
            barra.Location = new Point(205, 9);
            barra.Name = "barra";
            barra.Size = new Size(776, 13);
            barra.TabIndex = 2;
            // 
            // btnsavebin
            // 
            btnsavebin.Location = new Point(12, 4);
            btnsavebin.Name = "btnsavebin";
            btnsavebin.Size = new Size(75, 23);
            btnsavebin.TabIndex = 1;
            btnsavebin.Text = "Save binary";
            btnsavebin.UseVisualStyleBackColor = true;
            // 
            // btnclose
            // 
            btnclose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnclose.Location = new Point(1114, 4);
            btnclose.Name = "btnclose";
            btnclose.Size = new Size(75, 23);
            btnclose.TabIndex = 0;
            btnclose.Text = "Close";
            btnclose.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 37);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(listado);
            splitContainer1.Panel1.Controls.Add(panel5);
            splitContainer1.Panel1.Padding = new Padding(4);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(panelmain);
            splitContainer1.Size = new Size(1192, 701);
            splitContainer1.SplitterDistance = 455;
            splitContainer1.TabIndex = 1;
            // 
            // listado
            // 
            listado.Dock = DockStyle.Fill;
            listado.DrawMode = DrawMode.OwnerDrawFixed;
            listado.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            listado.FormattingEnabled = true;
            listado.IntegralHeight = false;
            listado.ItemHeight = 24;
            listado.Location = new Point(4, 42);
            listado.Name = "listado";
            listado.Size = new Size(447, 655);
            listado.TabIndex = 0;
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(240, 240, 255);
            panel5.Controls.Add(label1);
            panel5.Controls.Add(cbfilter);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(4, 4);
            panel5.Name = "panel5";
            panel5.Size = new Size(447, 38);
            panel5.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 6);
            label1.Name = "label1";
            label1.Size = new Size(33, 15);
            label1.TabIndex = 1;
            label1.Text = "Filter";
            // 
            // cbfilter
            // 
            cbfilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cbfilter.FormattingEnabled = true;
            cbfilter.Location = new Point(47, 3);
            cbfilter.Name = "cbfilter";
            cbfilter.Size = new Size(397, 23);
            cbfilter.TabIndex = 0;
            cbfilter.TabStop = false;
            // 
            // panelmain
            // 
            panelmain.Controls.Add(lienzo);
            panelmain.Controls.Add(panel4);
            panelmain.Controls.Add(panel2);
            panelmain.Dock = DockStyle.Fill;
            panelmain.Location = new Point(0, 0);
            panelmain.Name = "panelmain";
            panelmain.Size = new Size(733, 701);
            panelmain.TabIndex = 0;
            // 
            // lienzo
            // 
            lienzo.Baseline = 0;
            lienzo.BmpIcon = null;
            lienzo.Dock = DockStyle.Fill;
            lienzo.Editing = false;
            lienzo.Location = new Point(0, 29);
            lienzo.Name = "lienzo";
            lienzo.Size = new Size(733, 600);
            lienzo.TabIndex = 3;
            // 
            // panel4
            // 
            panel4.Controls.Add(cbcolor);
            panel4.Controls.Add(btnalias);
            panel4.Controls.Add(ejemplo);
            panel4.Controls.Add(edbaseline);
            panel4.Controls.Add(btncancel);
            panel4.Controls.Add(btneditsave);
            panel4.Dock = DockStyle.Bottom;
            panel4.Location = new Point(0, 629);
            panel4.Name = "panel4";
            panel4.Size = new Size(733, 72);
            panel4.TabIndex = 1;
            panel4.Visible = false;
            // 
            // cbcolor
            // 
            cbcolor.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cbcolor.DrawMode = DrawMode.OwnerDrawFixed;
            cbcolor.DropDownStyle = ComboBoxStyle.DropDownList;
            cbcolor.FormattingEnabled = true;
            cbcolor.ItemHeight = 20;
            cbcolor.Location = new Point(508, 42);
            cbcolor.Name = "cbcolor";
            cbcolor.Size = new Size(123, 26);
            cbcolor.TabIndex = 5;
            // 
            // btnalias
            // 
            btnalias.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnalias.Location = new Point(230, 45);
            btnalias.Name = "btnalias";
            btnalias.Size = new Size(84, 23);
            btnalias.TabIndex = 4;
            btnalias.Text = "Alias";
            btnalias.UseVisualStyleBackColor = true;
            // 
            // ejemplo
            // 
            ejemplo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            ejemplo.BaseLine = 0;
            ejemplo.Bitmap = null;
            ejemplo.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ejemplo.Location = new Point(320, 12);
            ejemplo.Name = "ejemplo";
            ejemplo.Size = new Size(182, 51);
            ejemplo.TabIndex = 3;
            ejemplo.Text = "Example";
            // 
            // edbaseline
            // 
            edbaseline.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            edbaseline.Location = new Point(165, 45);
            edbaseline.Name = "edbaseline";
            edbaseline.Size = new Size(59, 23);
            edbaseline.TabIndex = 2;
            // 
            // btncancel
            // 
            btncancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btncancel.Location = new Point(84, 45);
            btncancel.Name = "btncancel";
            btncancel.Size = new Size(75, 23);
            btncancel.TabIndex = 1;
            btncancel.Text = "Cancel";
            btncancel.UseVisualStyleBackColor = true;
            // 
            // btneditsave
            // 
            btneditsave.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btneditsave.Location = new Point(3, 45);
            btneditsave.Name = "btneditsave";
            btneditsave.Size = new Size(75, 23);
            btneditsave.TabIndex = 0;
            btneditsave.Text = "Save";
            btneditsave.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.Controls.Add(lbbaseline);
            panel2.Controls.Add(lbposition);
            panel2.Controls.Add(lbiconname);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(733, 29);
            panel2.TabIndex = 0;
            // 
            // lbbaseline
            // 
            lbbaseline.AutoSize = true;
            lbbaseline.Location = new Point(73, 6);
            lbbaseline.Name = "lbbaseline";
            lbbaseline.Size = new Size(38, 15);
            lbbaseline.TabIndex = 2;
            lbbaseline.Text = "label1";
            // 
            // lbposition
            // 
            lbposition.AutoSize = true;
            lbposition.Location = new Point(3, 6);
            lbposition.Name = "lbposition";
            lbposition.Size = new Size(38, 15);
            lbposition.TabIndex = 1;
            lbposition.Text = "label1";
            // 
            // lbiconname
            // 
            lbiconname.AutoSize = true;
            lbiconname.Location = new Point(172, 6);
            lbiconname.Name = "lbiconname";
            lbiconname.Size = new Size(38, 15);
            lbiconname.TabIndex = 0;
            lbiconname.Text = "label1";
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.ControlDark;
            panel3.Controls.Add(btntop);
            panel3.Controls.Add(labelcabecera);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(1192, 37);
            panel3.TabIndex = 2;
            // 
            // btntop
            // 
            btntop.Location = new Point(4, 8);
            btntop.Name = "btntop";
            btntop.Size = new Size(61, 23);
            btntop.TabIndex = 1;
            btntop.Text = "Edit...";
            btntop.UseVisualStyleBackColor = true;
            // 
            // labelcabecera
            // 
            labelcabecera.AutoSize = true;
            labelcabecera.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelcabecera.ForeColor = Color.FromArgb(255, 255, 192);
            labelcabecera.Location = new Point(71, 9);
            labelcabecera.Name = "labelcabecera";
            labelcabecera.Size = new Size(57, 21);
            labelcabecera.TabIndex = 0;
            labelcabecera.Text = "label1";
            // 
            // btnupgrade
            // 
            btnupgrade.Location = new Point(124, 4);
            btnupgrade.Name = "btnupgrade";
            btnupgrade.Size = new Size(75, 23);
            btnupgrade.TabIndex = 4;
            btnupgrade.Text = "Upgrade ...";
            btnupgrade.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1192, 769);
            Controls.Add(splitContainer1);
            Controls.Add(panel1);
            Controls.Add(panel3);
            Name = "Form1";
            Text = "Bank Icon Builder";
            panel1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panelmain.ResumeLayout(false);
            panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)edbaseline).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        public Button btnclose;
        private SplitContainer splitContainer1;
        public Panel panelmain;
        private Panel panel3;
        public Label labelcabecera;
        public Lienzo lienzo;
        public Panel panel4;
        private Panel panel2;
        public Button btntop;
        public ListBox listado;
        public Label lbiconname;
        public Button btneditsave;
        public Button btncancel;
        public Label lbposition;
        public Label lbbaseline;
        public NumericUpDown edbaseline;
        public Ejemplo ejemplo;
        public Button btnalias;
        public Button btnsavebin;
        public ProgressBar barra;
        public Button btntest;
        private Panel panel5;
        private Label label1;
        public ComboBox cbfilter;
        public ComboBox cbcolor;
        public Button btnupgrade;
    }
}
