namespace Rop.Winforms9.DoutoneIconBuilder
{
    partial class FormAddNewIcons
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
            panel1 = new Panel();
            barra = new ProgressBar();
            btncancel = new Button();
            btnok = new Button();
            button1 = new Button();
            listado1 = new ListBox();
            listado2 = new ListBox();
            btnadd = new Button();
            btnaddall = new Button();
            btndel = new Button();
            pb = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pb).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(barra);
            panel1.Controls.Add(btncancel);
            panel1.Controls.Add(btnok);
            panel1.Controls.Add(button1);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 417);
            panel1.Name = "panel1";
            panel1.Size = new Size(632, 33);
            panel1.TabIndex = 4;
            // 
            // barra
            // 
            barra.Location = new Point(93, 9);
            barra.Name = "barra";
            barra.Size = new Size(436, 12);
            barra.Style = ProgressBarStyle.Marquee;
            barra.TabIndex = 4;
            barra.Visible = false;
            // 
            // btncancel
            // 
            btncancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btncancel.Location = new Point(545, 5);
            btncancel.Name = "btncancel";
            btncancel.Size = new Size(75, 23);
            btncancel.TabIndex = 3;
            btncancel.Text = "Cancel";
            btncancel.UseVisualStyleBackColor = true;
            // 
            // btnok
            // 
            btnok.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnok.Location = new Point(12, 5);
            btnok.Name = "btnok";
            btnok.Size = new Size(75, 23);
            btnok.TabIndex = 2;
            btnok.Text = "Ok";
            btnok.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.Location = new Point(638, -60);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "Cancel";
            button1.UseVisualStyleBackColor = true;
            // 
            // listado1
            // 
            listado1.FormattingEnabled = true;
            listado1.IntegralHeight = false;
            listado1.Location = new Point(12, 12);
            listado1.Name = "listado1";
            listado1.Size = new Size(261, 399);
            listado1.TabIndex = 5;
            // 
            // listado2
            // 
            listado2.FormattingEnabled = true;
            listado2.IntegralHeight = false;
            listado2.Location = new Point(360, 12);
            listado2.Name = "listado2";
            listado2.Size = new Size(261, 399);
            listado2.TabIndex = 6;
            // 
            // btnadd
            // 
            btnadd.Location = new Point(279, 43);
            btnadd.Name = "btnadd";
            btnadd.Size = new Size(75, 23);
            btnadd.TabIndex = 7;
            btnadd.Text = ">";
            btnadd.UseVisualStyleBackColor = true;
            // 
            // btnaddall
            // 
            btnaddall.Location = new Point(279, 72);
            btnaddall.Name = "btnaddall";
            btnaddall.Size = new Size(75, 23);
            btnaddall.TabIndex = 8;
            btnaddall.Text = ">>>";
            btnaddall.UseVisualStyleBackColor = true;
            // 
            // btndel
            // 
            btndel.Location = new Point(279, 150);
            btndel.Name = "btndel";
            btndel.Size = new Size(75, 23);
            btndel.TabIndex = 9;
            btndel.Text = "<";
            btndel.UseVisualStyleBackColor = true;
            // 
            // pb
            // 
            pb.BackColor = Color.White;
            pb.Location = new Point(284, 296);
            pb.Name = "pb";
            pb.Size = new Size(64, 64);
            pb.TabIndex = 10;
            pb.TabStop = false;
            // 
            // FormAddNewIcons
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(632, 450);
            Controls.Add(pb);
            Controls.Add(btndel);
            Controls.Add(btnaddall);
            Controls.Add(btnadd);
            Controls.Add(listado2);
            Controls.Add(listado1);
            Controls.Add(panel1);
            Name = "FormAddNewIcons";
            Text = "FormAddNewIcons";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pb).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button button1;
        public Button btnok;
        public Button btncancel;
        public Button btnaddall;
        public ListBox listado1;
        public ListBox listado2;
        public Button btnadd;
        public Button button5;
        public Button btndel;
        private ListBox listBox1;
        public ProgressBar barra;
        public PictureBox pb;
    }
}