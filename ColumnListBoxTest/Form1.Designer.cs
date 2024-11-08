namespace ColumnListBoxTest
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
            columnListBox1 = new Rop.Winforms9.ColumnsListBox.ColumnListBox();
            materialDesignBank1 = new Rop.Winforms9.DuotoneIcons.MaterialDesign.MaterialDesignBank();
            SuspendLayout();
            // 
            // columnListBox1
            // 
            columnListBox1.BankIcon = materialDesignBank1;
            columnListBox1.BorderStyle = BorderStyle.Fixed3D;
            columnListBox1.ColumnDefinitions.Add(new Rop.Winforms9.DuotoneIcons.ColumnDefinition(ContentAlignment.MiddleLeft, 50, "Id", true, ""));
            columnListBox1.ColumnDefinitions.Add(new Rop.Winforms9.DuotoneIcons.ColumnDefinition(ContentAlignment.MiddleLeft, 100, "Nombre", true, ""));
            columnListBox1.ColumnDefinitions.Add(new Rop.Winforms9.DuotoneIcons.ColumnDefinition(ContentAlignment.MiddleLeft, 100, "Apellidos", true, ""));
            columnListBox1.ColumnsPadding = new Padding(3);
            columnListBox1.HeaderBackColor = SystemColors.Control;
            columnListBox1.HeaderBorderRaised = true;
            columnListBox1.HeaderBorderStyle = BorderStyle.Fixed3D;
            columnListBox1.HeaderInteriorBorder = true;
            // 
            // 
            // 
            columnListBox1.Location = new Point(85, 86);
            columnListBox1.Name = "columnListBox1";
            columnListBox1.Size = new Size(334, 261);
            columnListBox1.TabIndex = 2;
            columnListBox1.Text = "columnListBox1";
            columnListBox1.SortItems += columnListBox1_SortItems;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(columnListBox1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion
        private Rop.Winforms9.ColumnsListBox.ColumnListBox columnListBox1;
        private Rop.Winforms9.DuotoneIcons.MaterialDesign.MaterialDesignBank materialDesignBank1;
    }
}
