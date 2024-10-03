namespace Rop.Winforms8._1.DoutoneIconBuilder
{
    partial class InputDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            edname = new TextBox();
            label1 = new Label();
            edinitials = new TextBox();
            panel1 = new Panel();
            button2 = new Button();
            button1 = new Button();
            label2 = new Label();
            edw = new TextBox();
            label3 = new Label();
            label4 = new Label();
            edh = new TextBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // edname
            // 
            edname.Location = new Point(12, 28);
            edname.Name = "edname";
            edname.Size = new Size(270, 23);
            edname.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 10);
            label1.Name = "label1";
            label1.Size = new Size(97, 15);
            label1.TabIndex = 1;
            label1.Text = "Icon Bank Name:";
            // 
            // edinitials
            // 
            edinitials.Location = new Point(288, 28);
            edinitials.Name = "edinitials";
            edinitials.ReadOnly = true;
            edinitials.Size = new Size(46, 23);
            edinitials.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 108);
            panel1.Name = "panel1";
            panel1.Size = new Size(434, 30);
            panel1.TabIndex = 3;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button2.Location = new Point(3, 4);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 1;
            button2.Text = "OK";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button1.Location = new Point(356, 4);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Cancel";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 57);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 4;
            label2.Text = "Base Size:";
            // 
            // edw
            // 
            edw.Location = new Point(32, 75);
            edw.Name = "edw";
            edw.Size = new Size(46, 23);
            edw.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 78);
            label3.Name = "label3";
            label3.Size = new Size(21, 15);
            label3.TabIndex = 6;
            label3.Text = "W:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(85, 78);
            label4.Name = "label4";
            label4.Size = new Size(19, 15);
            label4.TabIndex = 8;
            label4.Text = "H:";
            // 
            // edh
            // 
            edh.Location = new Point(105, 75);
            edh.Name = "edh";
            edh.Size = new Size(46, 23);
            edh.TabIndex = 7;
            // 
            // InputDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(434, 138);
            Controls.Add(label4);
            Controls.Add(edh);
            Controls.Add(label3);
            Controls.Add(edw);
            Controls.Add(label2);
            Controls.Add(panel1);
            Controls.Add(edinitials);
            Controls.Add(label1);
            Controls.Add(edname);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "InputDialog";
            Text = "Bank Name Dialog";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox edname;
        private Label label1;
        private TextBox edinitials;
        private Panel panel1;
        private Button button2;
        private Button button1;
        private Label label2;
        private TextBox edw;
        private Label label3;
        private Label label4;
        private TextBox edh;
    }
}