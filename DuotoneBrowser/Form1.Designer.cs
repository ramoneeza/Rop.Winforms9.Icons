﻿namespace DuotoneBrowser
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
            components = new System.ComponentModel.Container();
            label5 = new Label();
            edoffsettext = new NumericUpDown();
            label2 = new Label();
            edoffseticon = new NumericUpDown();
            edicontext = new TextBox();
            btnbuscar = new Button();
            edbuscar = new TextBox();
            panel1 = new Panel();
            tabla = new PictureBox();
            button4 = new Button();
            label1 = new Label();
            namelabel = new Label();
            button3 = new Button();
            lst = new ListBox();
            cbBank = new ComboBox();
            biglabel = new Rop.Winforms9.DuotoneIcons.Controls.IconLabel();
            soloIcon = new Rop.Winforms9.DuotoneIcons.Controls.SoloIconLabel();
            colorDialog1 = new ColorDialog();
            label3 = new Label();
            label6 = new Label();
            label7 = new Label();
            lcb = new Label();
            lc1 = new Label();
            lc2 = new Label();
            btnIcon = new Rop.Winforms9.DuotoneIcons.Controls.IconButton();
            button1 = new Button();
            label4 = new Label();
            edminascent = new NumericUpDown();
            label8 = new Label();
            ediconmarginleft = new NumericUpDown();
            label9 = new Label();
            ediconmarginright = new NumericUpDown();
            edusesuffix = new CheckBox();
            label10 = new Label();
            edminheight = new NumericUpDown();
            label11 = new Label();
            edpaddingright = new NumericUpDown();
            label12 = new Label();
            edpaddingleft = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)edoffsettext).BeginInit();
            ((System.ComponentModel.ISupportInitialize)edoffseticon).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tabla).BeginInit();
            ((System.ComponentModel.ISupportInitialize)edminascent).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ediconmarginleft).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ediconmarginright).BeginInit();
            ((System.ComponentModel.ISupportInitialize)edminheight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)edpaddingright).BeginInit();
            ((System.ComponentModel.ISupportInitialize)edpaddingleft).BeginInit();
            SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(730, 327);
            label5.Name = "label5";
            label5.Size = new Size(60, 15);
            label5.TabIndex = 42;
            label5.Text = "OffsetText";
            // 
            // edoffsettext
            // 
            edoffsettext.Location = new Point(828, 325);
            edoffsettext.Maximum = new decimal(new int[] { 16, 0, 0, 0 });
            edoffsettext.Minimum = new decimal(new int[] { 16, 0, 0, int.MinValue });
            edoffsettext.Name = "edoffsettext";
            edoffsettext.Size = new Size(66, 23);
            edoffsettext.TabIndex = 41;
            edoffsettext.ValueChanged += edoffsettext_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(730, 299);
            label2.Name = "label2";
            label2.Size = new Size(62, 15);
            label2.TabIndex = 40;
            label2.Text = "OffsetIcon";
            // 
            // edoffseticon
            // 
            edoffseticon.Location = new Point(828, 297);
            edoffseticon.Maximum = new decimal(new int[] { 16, 0, 0, 0 });
            edoffseticon.Minimum = new decimal(new int[] { 16, 0, 0, int.MinValue });
            edoffseticon.Name = "edoffseticon";
            edoffseticon.Size = new Size(66, 23);
            edoffseticon.TabIndex = 39;
            edoffseticon.ValueChanged += edoffseticon_ValueChanged;
            // 
            // edicontext
            // 
            edicontext.Location = new Point(828, 237);
            edicontext.Name = "edicontext";
            edicontext.Size = new Size(100, 23);
            edicontext.TabIndex = 38;
            edicontext.Text = "icon";
            edicontext.TextChanged += edicontext_TextChanged;
            // 
            // btnbuscar
            // 
            btnbuscar.Location = new Point(839, 73);
            btnbuscar.Name = "btnbuscar";
            btnbuscar.Size = new Size(69, 23);
            btnbuscar.TabIndex = 37;
            btnbuscar.Text = "Buscar";
            btnbuscar.UseVisualStyleBackColor = true;
            btnbuscar.Click += btnbuscar_Click;
            // 
            // edbuscar
            // 
            edbuscar.Location = new Point(735, 73);
            edbuscar.Name = "edbuscar";
            edbuscar.Size = new Size(100, 23);
            edbuscar.TabIndex = 36;
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(tabla);
            panel1.Location = new Point(47, 104);
            panel1.Name = "panel1";
            panel1.Size = new Size(470, 406);
            panel1.TabIndex = 35;
            // 
            // tabla
            // 
            tabla.BackColor = Color.White;
            tabla.Location = new Point(3, 3);
            tabla.Name = "tabla";
            tabla.Size = new Size(447, 420);
            tabla.TabIndex = 14;
            tabla.TabStop = false;
            tabla.Click += tabla_Click;
            tabla.MouseEnter += tabla_MouseEnter;
            tabla.MouseLeave += tabla_MouseLeave;
            tabla.MouseMove += tabla_MouseMove;
            // 
            // button4
            // 
            button4.Location = new Point(839, 117);
            button4.Margin = new Padding(2, 3, 2, 3);
            button4.Name = "button4";
            button4.Size = new Size(69, 25);
            button4.TabIndex = 34;
            button4.Text = "Copy";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(735, 107);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 33;
            label1.Text = "Name";
            // 
            // namelabel
            // 
            namelabel.AutoSize = true;
            namelabel.Location = new Point(735, 127);
            namelabel.Margin = new Padding(2, 0, 2, 0);
            namelabel.Name = "namelabel";
            namelabel.Size = new Size(31, 15);
            namelabel.TabIndex = 32;
            namelabel.Text = "xxxx";
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button3.Location = new Point(894, 596);
            button3.Margin = new Padding(2, 3, 2, 3);
            button3.Name = "button3";
            button3.Size = new Size(69, 25);
            button3.TabIndex = 31;
            button3.Text = "Exit";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // lst
            // 
            lst.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lst.DrawMode = DrawMode.OwnerDrawFixed;
            lst.FormattingEnabled = true;
            lst.IntegralHeight = false;
            lst.ItemHeight = 32;
            lst.Location = new Point(522, 104);
            lst.Margin = new Padding(2, 3, 2, 3);
            lst.Name = "lst";
            lst.Size = new Size(195, 463);
            lst.TabIndex = 24;
            lst.DrawItem += lst_DrawItem;
            lst.SelectedIndexChanged += lst_SelectedIndexChanged;
            // 
            // cbBank
            // 
            cbBank.DropDownStyle = ComboBoxStyle.DropDownList;
            cbBank.FormattingEnabled = true;
            cbBank.Items.AddRange(new object[] { "GoogleMaterial", "FontAwesone", "MaterialDesign" });
            cbBank.Location = new Point(522, 73);
            cbBank.Margin = new Padding(2, 3, 2, 3);
            cbBank.Name = "cbBank";
            cbBank.Size = new Size(195, 23);
            cbBank.TabIndex = 23;
            cbBank.SelectedIndexChanged += cbBank_SelectedIndexChanged;
            // 
            // biglabel
            // 
            biglabel.AutoSize = true;
            biglabel.BackColor = Color.FromArgb(255, 224, 192);
            biglabel.BankIcon = null;
            biglabel.IconCode = "apple";
            biglabel.IconDisabled = "";
            biglabel.IconMarginLeft = 0F;
            biglabel.IconMarginRight = 0F;
            biglabel.IconScale = 150;
            biglabel.IsSuffix = false;
            biglabel.Location = new Point(748, 240);
            biglabel.MinAscent = 0;
            biglabel.MinHeight = 0;
            biglabel.Name = "biglabel";
            biglabel.OffsetIcon = 0F;
            biglabel.OffsetText = 0F;
            biglabel.ShowToolTip = false;
            biglabel.Size = new Size(28, 17);
            biglabel.TabIndex = 43;
            biglabel.Text = "Hola";
            biglabel.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            biglabel.ToolTipText = "";
            biglabel.UseIconColor = false;
            // 
            // soloIcon
            // 
            soloIcon.AutoSize = true;
            soloIcon.BankIcon = null;
            soloIcon.Font = new Font("Segoe UI", 24F);
            soloIcon.IconCode = null;
            soloIcon.IconDisabled = "";
            soloIcon.IconScale = 125;
            soloIcon.Location = new Point(748, 179);
            soloIcon.MinAscent = 0;
            soloIcon.MinHeight = 0;
            soloIcon.Name = "soloIcon";
            soloIcon.OffsetIcon = 0F;
            soloIcon.ShowToolTip = false;
            soloIcon.Size = new Size(16, 16);
            soloIcon.TabIndex = 44;
            soloIcon.ToolTipText = "";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(735, 519);
            label3.Name = "label3";
            label3.Size = new Size(64, 15);
            label3.TabIndex = 45;
            label3.Text = "Color Back";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(754, 549);
            label6.Name = "label6";
            label6.Size = new Size(45, 15);
            label6.TabIndex = 46;
            label6.Text = "Color 1";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(752, 576);
            label7.Name = "label7";
            label7.Size = new Size(45, 15);
            label7.TabIndex = 47;
            label7.Text = "Color 2";
            // 
            // lcb
            // 
            lcb.BorderStyle = BorderStyle.FixedSingle;
            lcb.Location = new Point(828, 518);
            lcb.Name = "lcb";
            lcb.Size = new Size(45, 23);
            lcb.TabIndex = 48;
            lcb.Click += lcb_Click;
            // 
            // lc1
            // 
            lc1.BorderStyle = BorderStyle.FixedSingle;
            lc1.Location = new Point(828, 542);
            lc1.Name = "lc1";
            lc1.Size = new Size(45, 23);
            lc1.TabIndex = 49;
            lc1.Click += lc1_Click;
            // 
            // lc2
            // 
            lc2.BorderStyle = BorderStyle.FixedSingle;
            lc2.Location = new Point(828, 568);
            lc2.Name = "lc2";
            lc2.Size = new Size(45, 23);
            lc2.TabIndex = 50;
            lc2.Click += lc2_Click;
            // 
            // btnIcon
            // 
            btnIcon.BankIcon = null;
            btnIcon.IconCode = null;
            btnIcon.IconDisabled = "";
            btnIcon.IconMarginLeft = 0F;
            btnIcon.IconMarginRight = 0F;
            btnIcon.IconScale = 125;
            btnIcon.IsSuffix = false;
            btnIcon.Location = new Point(828, 179);
            btnIcon.MinAscent = 0;
            btnIcon.MinHeight = 0;
            btnIcon.Name = "btnIcon";
            btnIcon.OffsetIcon = 0F;
            btnIcon.OffsetText = 0F;
            btnIcon.ShowToolTip = false;
            btnIcon.Size = new Size(114, 34);
            btnIcon.TabIndex = 51;
            btnIcon.Text = "iconButton1";
            btnIcon.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            btnIcon.ToolTipText = "";
            btnIcon.UseIconColor = false;
            btnIcon.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(839, 148);
            button1.Margin = new Padding(2, 3, 2, 3);
            button1.Name = "button1";
            button1.Size = new Size(103, 25);
            button1.TabIndex = 52;
            button1.Text = "Copy Bitmap";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(730, 356);
            label4.Name = "label4";
            label4.Size = new Size(64, 15);
            label4.TabIndex = 54;
            label4.Text = "MinAscent";
            // 
            // edminascent
            // 
            edminascent.Location = new Point(828, 354);
            edminascent.Maximum = new decimal(new int[] { 32, 0, 0, 0 });
            edminascent.Minimum = new decimal(new int[] { 16, 0, 0, int.MinValue });
            edminascent.Name = "edminascent";
            edminascent.Size = new Size(66, 23);
            edminascent.TabIndex = 53;
            edminascent.ValueChanged += edminascent_ValueChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(730, 420);
            label8.Name = "label8";
            label8.Size = new Size(88, 15);
            label8.TabIndex = 56;
            label8.Text = "IconMarginLeft";
            // 
            // ediconmarginleft
            // 
            ediconmarginleft.Location = new Point(828, 418);
            ediconmarginleft.Maximum = new decimal(new int[] { 16, 0, 0, 0 });
            ediconmarginleft.Minimum = new decimal(new int[] { 16, 0, 0, int.MinValue });
            ediconmarginleft.Name = "ediconmarginleft";
            ediconmarginleft.Size = new Size(66, 23);
            ediconmarginleft.TabIndex = 55;
            ediconmarginleft.ValueChanged += ediconmarginleft_ValueChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(730, 449);
            label9.Name = "label9";
            label9.Size = new Size(96, 15);
            label9.TabIndex = 58;
            label9.Text = "IconMarginRight";
            // 
            // ediconmarginright
            // 
            ediconmarginright.Location = new Point(828, 447);
            ediconmarginright.Maximum = new decimal(new int[] { 16, 0, 0, 0 });
            ediconmarginright.Minimum = new decimal(new int[] { 16, 0, 0, int.MinValue });
            ediconmarginright.Name = "ediconmarginright";
            ediconmarginright.Size = new Size(66, 23);
            ediconmarginright.TabIndex = 57;
            ediconmarginright.ValueChanged += ediconmarginright_ValueChanged;
            // 
            // edusesuffix
            // 
            edusesuffix.AutoSize = true;
            edusesuffix.Location = new Point(828, 476);
            edusesuffix.Name = "edusesuffix";
            edusesuffix.Size = new Size(56, 19);
            edusesuffix.TabIndex = 59;
            edusesuffix.Text = "Suffix";
            edusesuffix.UseVisualStyleBackColor = true;
            edusesuffix.CheckedChanged += edusesuffix_CheckedChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(730, 384);
            label10.Name = "label10";
            label10.Size = new Size(64, 15);
            label10.TabIndex = 61;
            label10.Text = "MinHeight";
            // 
            // edminheight
            // 
            edminheight.Location = new Point(828, 382);
            edminheight.Maximum = new decimal(new int[] { 32, 0, 0, 0 });
            edminheight.Minimum = new decimal(new int[] { 16, 0, 0, int.MinValue });
            edminheight.Name = "edminheight";
            edminheight.Size = new Size(66, 23);
            edminheight.TabIndex = 60;
            edminheight.ValueChanged += edminheight_ValueChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(911, 328);
            label11.Name = "label11";
            label11.Size = new Size(79, 15);
            label11.TabIndex = 65;
            label11.Text = "PaddingRight";
            // 
            // edpaddingright
            // 
            edpaddingright.Location = new Point(1009, 326);
            edpaddingright.Maximum = new decimal(new int[] { 16, 0, 0, 0 });
            edpaddingright.Minimum = new decimal(new int[] { 16, 0, 0, int.MinValue });
            edpaddingright.Name = "edpaddingright";
            edpaddingright.Size = new Size(66, 23);
            edpaddingright.TabIndex = 64;
            edpaddingright.ValueChanged += edpaddingright_ValueChanged;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(911, 299);
            label12.Name = "label12";
            label12.Size = new Size(71, 15);
            label12.TabIndex = 63;
            label12.Text = "PaddingLeft";
            // 
            // edpaddingleft
            // 
            edpaddingleft.Location = new Point(1009, 297);
            edpaddingleft.Maximum = new decimal(new int[] { 16, 0, 0, 0 });
            edpaddingleft.Minimum = new decimal(new int[] { 16, 0, 0, int.MinValue });
            edpaddingleft.Name = "edpaddingleft";
            edpaddingleft.Size = new Size(66, 23);
            edpaddingleft.TabIndex = 62;
            edpaddingleft.ValueChanged += edpaddingleft_ValueChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1094, 646);
            Controls.Add(label11);
            Controls.Add(edpaddingright);
            Controls.Add(label12);
            Controls.Add(edpaddingleft);
            Controls.Add(label10);
            Controls.Add(edminheight);
            Controls.Add(edusesuffix);
            Controls.Add(label9);
            Controls.Add(ediconmarginright);
            Controls.Add(label8);
            Controls.Add(ediconmarginleft);
            Controls.Add(label4);
            Controls.Add(edminascent);
            Controls.Add(button1);
            Controls.Add(btnIcon);
            Controls.Add(lc2);
            Controls.Add(lc1);
            Controls.Add(lcb);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label3);
            Controls.Add(soloIcon);
            Controls.Add(biglabel);
            Controls.Add(label5);
            Controls.Add(edoffsettext);
            Controls.Add(label2);
            Controls.Add(edoffseticon);
            Controls.Add(edicontext);
            Controls.Add(btnbuscar);
            Controls.Add(edbuscar);
            Controls.Add(panel1);
            Controls.Add(button4);
            Controls.Add(label1);
            Controls.Add(namelabel);
            Controls.Add(button3);
            Controls.Add(lst);
            Controls.Add(cbBank);
            Name = "Form1";
            Text = "Select Bank Icon Two Tones";
            ((System.ComponentModel.ISupportInitialize)edoffsettext).EndInit();
            ((System.ComponentModel.ISupportInitialize)edoffseticon).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)tabla).EndInit();
            ((System.ComponentModel.ISupportInitialize)edminascent).EndInit();
            ((System.ComponentModel.ISupportInitialize)ediconmarginleft).EndInit();
            ((System.ComponentModel.ISupportInitialize)ediconmarginright).EndInit();
            ((System.ComponentModel.ISupportInitialize)edminheight).EndInit();
            ((System.ComponentModel.ISupportInitialize)edpaddingright).EndInit();
            ((System.ComponentModel.ISupportInitialize)edpaddingleft).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label5;
        private NumericUpDown edoffsettext;
        private Label label2;
        private NumericUpDown edoffseticon;
        private TextBox edicontext;
        private Button btnbuscar;
        private TextBox edbuscar;
        private Panel panel1;
        private PictureBox tabla;
        private Button button4;
        private Label label1;
        private Label namelabel;
        private Button button3;
        private ListBox lst;
        private ComboBox cbBank;
        private Rop.Winforms9.DuotoneIcons.Controls.IconLabel biglabel;
        private Rop.Winforms9.DuotoneIcons.Controls.SoloIconLabel soloIcon;
        private ColorDialog colorDialog1;
        private Label label3;
        private Label label6;
        private Label label7;
        private Label lcb;
        private Label lc1;
        private Label lc2;
        private Rop.Winforms9.DuotoneIcons.Controls.IconButton btnIcon;
        private Button button1;
        private Label label4;
        private NumericUpDown edminascent;
        private Label label8;
        private NumericUpDown ediconmarginleft;
        private Label label9;
        private NumericUpDown ediconmarginright;
        private CheckBox edusesuffix;
        private Label label10;
        private NumericUpDown edminheight;
        private Label label11;
        private NumericUpDown edpaddingright;
        private Label label12;
        private NumericUpDown edpaddingleft;
    }
}
