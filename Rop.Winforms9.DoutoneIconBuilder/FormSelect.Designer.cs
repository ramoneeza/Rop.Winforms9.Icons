namespace Rop.Winforms8._1.DoutoneIconBuilder
{
    partial class FormSelect
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
            listado = new ListBox();
            panel1 = new Panel();
            button2 = new Button();
            button1 = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            barra = new ProgressBar();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // listado
            // 
            listado.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listado.FormattingEnabled = true;
            listado.ItemHeight = 15;
            listado.Location = new Point(12, 12);
            listado.Name = "listado";
            listado.Size = new Size(509, 154);
            listado.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(barra);
            panel1.Controls.Add(button2);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 204);
            panel1.Name = "panel1";
            panel1.Size = new Size(529, 30);
            panel1.TabIndex = 1;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button2.Location = new Point(451, 3);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 3;
            button2.Text = "Exit";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button1.Location = new Point(12, 172);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 2;
            button1.Text = "New...";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // barra
            // 
            barra.Location = new Point(12, 8);
            barra.Name = "barra";
            barra.Size = new Size(433, 16);
            barra.TabIndex = 4;
            barra.Visible = false;
            // 
            // FormSelect
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(529, 234);
            Controls.Add(button1);
            Controls.Add(panel1);
            Controls.Add(listado);
            Name = "FormSelect";
            Text = "Select Bank";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ListBox listado;
        private Panel panel1;
        private Button button2;
        private Button button1;
        private FolderBrowserDialog folderBrowserDialog1;
        public ProgressBar barra;
    }
}