namespace DropControlTest
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
            materialDesignBank1 = new Rop.Winforms9.DuotoneIcons.MaterialDesign.MaterialDesignBank();
            fileDropControl2 = new Rop.Winforms9.DropControls.FileDropControl();
            linkDropControl1 = new Rop.Winforms9.DropControls.LinkDropControl();
            fileContentDropControl1 = new Rop.Winforms9.DropControls.FileContentDropControl();
            fileDropControl3 = new Rop.Winforms9.DropControls.FileDropControl();
            textBox1 = new TextBox();
            dropImage1 = new Rop.Winforms9.DropControls.DropImage();
            SuspendLayout();
            // 
            // fileDropControl2
            // 
            fileDropControl2.AllowDrop = true;
            fileDropControl2.AllowedExtensions = new string[]
    {
    "pdf",
    "png",
    "jpg"
    };
            fileDropControl2.Location = new Point(120, 138);
            fileDropControl2.MinimumSize = new Size(100, 23);
            fileDropControl2.Name = "fileDropControl2";
            fileDropControl2.ShowEditTool = true;
            fileDropControl2.ShowOpenFile = true;
            fileDropControl2.Size = new Size(525, 23);
            fileDropControl2.TabIndex = 0;
            fileDropControl2.Text = "fileDropControl2";
            // 
            // linkDropControl1
            // 
            linkDropControl1.AllowDrop = true;
            linkDropControl1.AllowedExtensions = new string[]
    {
    "pdf"
    };
            linkDropControl1.Location = new Point(120, 210);
            linkDropControl1.MinimumSize = new Size(100, 23);
            linkDropControl1.Name = "linkDropControl1";
            linkDropControl1.Size = new Size(531, 37);
            linkDropControl1.TabIndex = 1;
            linkDropControl1.Text = "linkDropControl1";
            // 
            // fileContentDropControl1
            // 
            fileContentDropControl1.AllowDrop = true;
            fileContentDropControl1.AllowedExtensions = new string[]
    {
    "pdf",
    "png",
    "jpg"
    };
            fileContentDropControl1.Location = new Point(122, 308);
            fileContentDropControl1.MinimumSize = new Size(100, 23);
            fileContentDropControl1.Name = "fileContentDropControl1";
            fileContentDropControl1.Size = new Size(529, 46);
            fileContentDropControl1.TabIndex = 2;
            fileContentDropControl1.Text = "fileContentDropControl1";
            // 
            // fileDropControl3
            // 
            fileDropControl3.AllowDrop = true;
            fileDropControl3.AllowedExtensions = new string[]
    {
    "pdf"
    };
            fileDropControl3.Location = new Point(514, 60);
            fileDropControl3.MinimumSize = new Size(100, 23);
            fileDropControl3.Name = "fileDropControl3";
            fileDropControl3.Size = new Size(212, 23);
            fileDropControl3.TabIndex = 3;
            fileDropControl3.Text = "fileDropControl3";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(408, 60);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 4;
            // 
            // dropImage1
            // 
            dropImage1.AllowCopy = true;
            dropImage1.AllowDelete = true;
            dropImage1.AllowedDropTypes = new Rop.Winforms9.DropControls.DropTypes[]
    {
    Rop.Winforms9.DropControls.DropTypes.FileDrop,
    Rop.Winforms9.DropControls.DropTypes.FileGroupDescriptor
    };
            dropImage1.AllowedExtensions = new string[]
    {
    "png",
    "jpg",
    "bmp",
    "tif",
    "gif",
    "jpeg",
    "tiff"
    };
            dropImage1.AllowedSize = new Size(80, 80);
            dropImage1.AllowOpen = true;
            dropImage1.AllowPaste = true;
            dropImage1.AllowReSize = true;
            
            dropImage1.BorderStyle = BorderStyle.FixedSingle;
            dropImage1.Location = new Point(156, 416);
            dropImage1.Name = "dropImage1";
            dropImage1.ShowCopyPaste = true;
            dropImage1.ShowSize = true;
            dropImage1.Size = new Size(100, 100);
            dropImage1.TabIndex = 5;
            dropImage1.Text = "dropImage1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 708);
            Controls.Add(dropImage1);
            Controls.Add(textBox1);
            Controls.Add(fileDropControl3);
            Controls.Add(fileContentDropControl1);
            Controls.Add(linkDropControl1);
            Controls.Add(fileDropControl2);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Rop.Winforms9.DuotoneIcons.MaterialDesign.MaterialDesignBank materialDesignBank1;
        private Rop.Winforms9.DropControls.FileDropControl fileDropControl1;
        private Rop.Winforms9.DropControls.FileDropControl fileDropControl2;
        private Rop.Winforms9.DropControls.LinkDropControl linkDropControl1;
        private Rop.Winforms9.DropControls.FileContentDropControl fileContentDropControl1;
        private Rop.Winforms9.DropControls.FileDropControl fileDropControl3;
        private TextBox textBox1;
        private Rop.Winforms9.DropControls.DropImage dropImage1;
    }
}
