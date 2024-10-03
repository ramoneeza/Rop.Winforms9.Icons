using Rop.Winforms9.DuotoneIcons;

namespace DuotoneIconsTest
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
            materialDesignBank1 = new Rop.Winforms9.DuotoneIcons.MaterialDesign.MaterialDesignBank();
            soloIconLabel2 = new Rop.Winforms9.DuotoneIcons.Controls.SoloIconLabel();
            columnPanelMd1 = new Rop.Winforms9.DuotoneIcons.MaterialDesign.ColumnPanelMd();
            switchIconMd1 = new Rop.Winforms9.DuotoneIcons.MaterialDesign.SwitchIconMd();
            columnPanel1 = new Rop.Winforms9.DuotoneIcons.Controls.ColumnPanel();
            panel1 = new Panel();
            iconButton1 = new Rop.Winforms9.DuotoneIcons.Controls.IconButton();
            iconBoolButton1 = new Rop.Winforms9.DuotoneIcons.Controls.IconBoolButton();
            iconBoolLabel1 = new Rop.Winforms9.DuotoneIcons.Controls.IconBoolLabel();
            soloIconBoolLabel1 = new Rop.Winforms9.DuotoneIcons.Controls.SoloIconBoolLabel();
            switchIcon1 = new Rop.Winforms9.DuotoneIcons.Controls.SwitchIcon();
            SuspendLayout();
            // 
            // soloIconLabel2
            // 
            soloIconLabel2.AutoSize = true;
            soloIconLabel2.Location = new Point(0, 0);
            soloIconLabel2.Name = "soloIconLabel2";
            soloIconLabel2.Size = new Size(16, 16);
            soloIconLabel2.TabIndex = 1;
            soloIconLabel2.Text = "soloIconLabel2";
            // 
            // columnPanelMd1
            // 
            columnPanelMd1.BackColor = Color.LightCyan;
            columnPanelMd1.BorderStyle = BorderStyle.Fixed3D;
            columnPanelMd1.ColumnDefinitions.Add(new ColumnDefinition(ContentAlignment.MiddleLeft, 50, "Id", false, ""));
            columnPanelMd1.ColumnDefinitions.Add(new ColumnDefinition(ContentAlignment.MiddleLeft, 120, "Hola", true, "Campo Hola"));
            columnPanelMd1.ColumnDefinitions.Add(new ColumnDefinition(ContentAlignment.MiddleRight, 100, "Adios", true, "Campo Adios"));
            columnPanelMd1.ColumnsBackColor = Color.Gainsboro;
            columnPanelMd1.ColumnsPadding = new Padding(0);
            columnPanelMd1.IconAscending = "ChevronDown";
            columnPanelMd1.IconColorSelected = new DuoToneColor(Color.Black, Color.Transparent);
            columnPanelMd1.IconColorUnSelected = new DuoToneColor(Color.DarkGray, Color.Transparent);
            columnPanelMd1.IconDescending = "ChevronUp";
            columnPanelMd1.IconUnselected = "ChevronDown";
            columnPanelMd1.InteriorBorder = true;
            columnPanelMd1.Location = new Point(190, 81);
            columnPanelMd1.Name = "columnPanelMd1";
            columnPanelMd1.SelectableCursor = Cursors.Hand;
            columnPanelMd1.ShowToolTip = true;
            columnPanelMd1.Size = new Size(391, 29);
            columnPanelMd1.TabIndex = 2;
            // 
            // switchIconMd1
            // 
            switchIconMd1.AutoChange = true;
            switchIconMd1.AutoSize = true;
            switchIconMd1.DefaultIconColor = new DuoToneColor(Color.Black, Color.Transparent);
            switchIconMd1.DefaultIconText = "Switch";
            switchIconMd1.DefaultToolTipText = "";
            switchIconMd1.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            switchIconMd1.IconColorOff = new DuoToneColor(Color.Black, Color.Transparent);
            switchIconMd1.IconColorOn = new DuoToneColor(Color.Black, Color.Transparent);
            switchIconMd1.IconMarginRight = 3F;
            switchIconMd1.IconOff = "ToggleSwitchOffLegacy";
            switchIconMd1.IconOn = "ToggleSwitchLegacy";
            switchIconMd1.IconScale = 150;
            switchIconMd1.Location = new Point(239, 306);
            switchIconMd1.Name = "switchIconMd1";
            switchIconMd1.OffsetIcon = 1F;
            switchIconMd1.Size = new Size(113, 41);
            switchIconMd1.TabIndex = 6;
            // 
            // columnPanel1
            // 
            columnPanel1.BackColor = SystemColors.ActiveBorder;
            columnPanel1.BankIcon = materialDesignBank1;
            columnPanel1.ColumnsBackColor = SystemColors.Control;
            columnPanel1.ColumnsPadding = new Padding(5);
            columnPanel1.IconAscending = "ChevronDown";
            columnPanel1.IconColorSelected = new DuoToneColor(Color.Black, Color.Empty);
            columnPanel1.IconColorUnSelected = new DuoToneColor(Color.Gray, Color.Empty);
            columnPanel1.IconDescending = "ChevronUp";
            columnPanel1.IconUnselected = "ChevronDown";
            columnPanel1.Location = new Point(95, 143);
            columnPanel1.Name = "columnPanel1";
            columnPanel1.SelectableCursor = Cursors.Hand;
            columnPanel1.Size = new Size(672, 49);
            columnPanel1.TabIndex = 7;
            // 
            // panel1
            // 
            panel1.Location = new Point(462, 232);
            panel1.Name = "panel1";
            panel1.Size = new Size(111, 81);
            panel1.TabIndex = 8;
            // 
            // iconButton1
            // 
            iconButton1.BankIcon = materialDesignBank1;
            iconButton1.IconCode = "Abacus";
            iconButton1.IconMarginRight = 3F;
            iconButton1.Location = new Point(556, 365);
            iconButton1.Name = "iconButton1";
            iconButton1.Size = new Size(138, 37);
            iconButton1.TabIndex = 10;
            iconButton1.Text = "iconButton1";
            iconButton1.UseVisualStyleBackColor = true;
            // 
            // iconBoolButton1
            // 
            iconBoolButton1.BankIcon = materialDesignBank1;
            iconBoolButton1.DefaultIconColor = new DuoToneColor(Color.Black, Color.Transparent);
            iconBoolButton1.DefaultToolTipText = "";
            iconBoolButton1.IconColorOff = new DuoToneColor(Color.Black, Color.Transparent);
            iconBoolButton1.IconColorOn = new DuoToneColor(Color.Black, Color.Transparent);
            iconBoolButton1.IconOff = "ChevronDown";
            iconBoolButton1.IconOn = "Abacus";
            iconBoolButton1.Location = new Point(604, 273);
            iconBoolButton1.Name = "iconBoolButton1";
            iconBoolButton1.Size = new Size(109, 40);
            iconBoolButton1.TabIndex = 11;
            iconBoolButton1.Text = "iconBoolButton1";
            iconBoolButton1.TextIconOff = "Off";
            iconBoolButton1.TextIconOn = "On";
            iconBoolButton1.UseVisualStyleBackColor = true;
            // 
            // iconBoolLabel1
            // 
            iconBoolLabel1.AutoSize = true;
            iconBoolLabel1.BankIcon = materialDesignBank1;
            iconBoolLabel1.DefaultIconColor = new DuoToneColor(Color.Black, Color.Transparent);
            iconBoolLabel1.DefaultToolTipText = "";
            iconBoolLabel1.IconColorOff = new DuoToneColor(Color.Black, Color.Transparent);
            iconBoolLabel1.IconColorOn = new DuoToneColor(Color.Black, Color.Transparent);
            iconBoolLabel1.IconOff = "ChevronUp";
            iconBoolLabel1.IconOn = "Abacus";
            iconBoolLabel1.Location = new Point(360, 381);
            iconBoolLabel1.Name = "iconBoolLabel1";
            iconBoolLabel1.Size = new Size(35, 17);
            iconBoolLabel1.TabIndex = 12;
            iconBoolLabel1.Text = "iconBoolLabel1";
            iconBoolLabel1.TextIconOff = "Off";
            iconBoolLabel1.TextIconOn = "On";
            // 
            // soloIconBoolLabel1
            // 
            soloIconBoolLabel1.AutoSize = true;
            soloIconBoolLabel1.BankIcon = materialDesignBank1;
            soloIconBoolLabel1.DefaultIconColor = new DuoToneColor(Color.Black, Color.Transparent);
            soloIconBoolLabel1.DefaultToolTipText = "";
            soloIconBoolLabel1.IconColorOff = new DuoToneColor(Color.Black, Color.Transparent);
            soloIconBoolLabel1.IconColorOn = new DuoToneColor(Color.Black, Color.Transparent);
            soloIconBoolLabel1.IconOff = "ChevronUp";
            soloIconBoolLabel1.IconOn = "Abacus";
            soloIconBoolLabel1.Location = new Point(173, 373);
            soloIconBoolLabel1.Name = "soloIconBoolLabel1";
            soloIconBoolLabel1.SelectedIcon = true;
            soloIconBoolLabel1.Size = new Size(19, 17);
            soloIconBoolLabel1.TabIndex = 13;
            soloIconBoolLabel1.Text = "soloIconBoolLabel1";
            soloIconBoolLabel1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // switchIcon1
            // 
            switchIcon1.AutoSize = true;
            switchIcon1.BankIcon = materialDesignBank1;
            switchIcon1.DefaultIconColor = new DuoToneColor(Color.Black, Color.Transparent);
            switchIcon1.DefaultIconText = "Switch";
            switchIcon1.DefaultToolTipText = "";
            switchIcon1.IconColorOff = new DuoToneColor(Color.IndianRed, Color.Transparent);
            switchIcon1.IconColorOn = new DuoToneColor(Color.IndianRed, Color.Transparent);
            switchIcon1.IconMarginLeft = 3F;
            switchIcon1.IconMarginRight = 3F;
            switchIcon1.IconOff = "_ToggleSwitchOff";
            switchIcon1.IconOn = "_ToggleSwitchOn";
            switchIcon1.IconScale = 150;
            switchIcon1.Location = new Point(100, 234);
            switchIcon1.Name = "switchIcon1";
            switchIcon1.SelectedIcon = true;
            switchIcon1.Size = new Size(62, 20);
            switchIcon1.TabIndex = 14;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(800, 450);
            Controls.Add(switchIcon1);
            Controls.Add(soloIconBoolLabel1);
            Controls.Add(iconBoolLabel1);
            Controls.Add(iconBoolButton1);
            Controls.Add(iconButton1);
            Controls.Add(panel1);
            Controls.Add(columnPanel1);
            Controls.Add(switchIconMd1);
            Controls.Add(columnPanelMd1);
            Controls.Add(soloIconLabel2);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Rop.Winforms9.DuotoneIcons.MaterialDesign.MaterialDesignBank materialDesignBank1;
        private Rop.Winforms9.DuotoneIcons.Controls.SoloIconLabel soloIconLabel2;
        private Rop.Winforms9.DuotoneIcons.MaterialDesign.ColumnPanelMd columnPanelMd1;
        private Rop.Winforms9.DuotoneIcons.MaterialDesign.SwitchIconMd switchIconMd1;
        private Rop.Winforms9.DuotoneIcons.Controls.ColumnPanel columnPanel1;
        private Panel panel1;
        private Rop.Winforms9.DuotoneIcons.Controls.IconButton iconButton1;
        private Rop.Winforms9.DuotoneIcons.Controls.IconBoolButton iconBoolButton1;
        private Rop.Winforms9.DuotoneIcons.Controls.IconBoolLabel iconBoolLabel1;
        private Rop.Winforms9.DuotoneIcons.Controls.SoloIconBoolLabel soloIconBoolLabel1;
        private Rop.Winforms9.DuotoneIcons.Controls.SwitchIcon switchIcon1;
    }
}
