namespace Rop.Winforms8._1.DoutoneIconBuilder
{
    partial class FormAliasDialog
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
            label1 = new Label();
            edicon = new TextBox();
            edalias = new TextBox();
            panel1 = new Panel();
            button1 = new Button();
            btnok = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 29);
            label1.Name = "label1";
            label1.Size = new Size(53, 15);
            label1.TabIndex = 0;
            label1.Text = "Alias for:";
            // 
            // edicon
            // 
            edicon.Location = new Point(84, 26);
            edicon.Name = "edicon";
            edicon.ReadOnly = true;
            edicon.Size = new Size(169, 23);
            edicon.TabIndex = 1;
            // 
            // edalias
            // 
            edalias.Location = new Point(84, 55);
            edalias.Multiline = true;
            edalias.Name = "edalias";
            edalias.Size = new Size(169, 164);
            edalias.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(button1);
            panel1.Controls.Add(btnok);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 246);
            panel1.Name = "panel1";
            panel1.Size = new Size(284, 33);
            panel1.TabIndex = 3;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.Location = new Point(206, 7);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "Cancel";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // btnok
            // 
            btnok.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnok.Location = new Point(3, 7);
            btnok.Name = "btnok";
            btnok.Size = new Size(75, 23);
            btnok.TabIndex = 0;
            btnok.Text = "Ok";
            btnok.UseVisualStyleBackColor = true;
            btnok.Click += btnok_Click;
            // 
            // FormAliasDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 279);
            Controls.Add(panel1);
            Controls.Add(edalias);
            Controls.Add(edicon);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "FormAliasDialog";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "FormAliasDialog";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox edicon;
        private TextBox edalias;
        private Panel panel1;
        private Button button1;
        private Button btnok;
    }
}