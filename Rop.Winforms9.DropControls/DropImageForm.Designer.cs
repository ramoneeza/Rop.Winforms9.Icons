namespace Rop.Winforms9.DropControls
{
    partial class DropImageForm
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
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "DropImageForm";
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lbbr = new System.Windows.Forms.Label();
            this.lbtr = new System.Windows.Forms.Label();
            this.lbbl = new System.Windows.Forms.Label();
            this.lbtl = new System.Windows.Forms.Label();
            this.picpos = new System.Windows.Forms.PictureBox();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.modifyBitmap1 = new ModifyBitmap();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picpos)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.lbbr);
            this.panel1.Controls.Add(this.lbtr);
            this.panel1.Controls.Add(this.lbbl);
            this.panel1.Controls.Add(this.lbtl);
            this.panel1.Controls.Add(this.picpos);
            this.panel1.Controls.Add(this.btnGrabar);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 498);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(698, 52);
            this.panel1.TabIndex = 13;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(352, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 41);
            this.button2.TabIndex = 17;
            this.button2.Text = "Aj.Alto";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(271, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 41);
            this.button1.TabIndex = 16;
            this.button1.Text = "Aj.Ancho";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbbr
            // 
            this.lbbr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbbr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lbbr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbbr.Location = new System.Drawing.Point(202, 31);
            this.lbbr.Name = "lbbr";
            this.lbbr.Size = new System.Drawing.Size(63, 16);
            this.lbbr.TabIndex = 15;
            this.lbbr.Text = "1000,1000";
            this.lbbr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbtr
            // 
            this.lbtr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbtr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lbtr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbtr.Location = new System.Drawing.Point(202, 5);
            this.lbtr.Name = "lbtr";
            this.lbtr.Size = new System.Drawing.Size(63, 16);
            this.lbtr.TabIndex = 14;
            this.lbtr.Text = "1000,1000";
            this.lbtr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbbl
            // 
            this.lbbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lbbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbbl.Location = new System.Drawing.Point(85, 31);
            this.lbbl.Name = "lbbl";
            this.lbbl.Size = new System.Drawing.Size(63, 16);
            this.lbbl.TabIndex = 13;
            this.lbbl.Text = "1000,1000";
            this.lbbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbtl
            // 
            this.lbtl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbtl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lbtl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbtl.Location = new System.Drawing.Point(85, 5);
            this.lbtl.Name = "lbtl";
            this.lbtl.Size = new System.Drawing.Size(63, 16);
            this.lbtl.TabIndex = 12;
            this.lbtl.Text = "1000,1000";
            this.lbtl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // picpos
            // 
            this.picpos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.picpos.Location = new System.Drawing.Point(154, 5);
            this.picpos.Name = "picpos";
            this.picpos.Size = new System.Drawing.Size(42, 42);
            this.picpos.TabIndex = 11;
            this.picpos.TabStop = false;
            this.picpos.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picpos_MouseClick);
            this.picpos.MouseLeave += new System.EventHandler(this.picpos_MouseLeave);
            this.picpos.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picpos_MouseMove);
            // 
            // btnGrabar
            // 
            this.btnGrabar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGrabar.Location = new System.Drawing.Point(3, 26);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(75, 23);
            this.btnGrabar.TabIndex = 8;
            this.btnGrabar.Text = "Salir";
            this.btnGrabar.UseVisualStyleBackColor = true;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabarMano_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(620, 26);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // modifyBitmap1
            // 
            this.modifyBitmap1.BmpPadding = 10;
            this.modifyBitmap1.DesiredSize = new System.Drawing.Size(320, 200);
            this.modifyBitmap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modifyBitmap1.Location = new System.Drawing.Point(0, 0);
            this.modifyBitmap1.Name = "modifyBitmap1";
            this.modifyBitmap1.OriginalBitmap = null;
            this.modifyBitmap1.Size = new System.Drawing.Size(698, 498);
            this.modifyBitmap1.TabIndex = 14;
            this.modifyBitmap1.Text = "modifyBitmap1";
            this.modifyBitmap1.Changed += new System.EventHandler(this.modifyBitmap1_Changed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(433, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Puede usar el Cursor del teclado";
            // 
            // DropImageForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(698, 550);
            this.ControlBox = false;
            this.Controls.Add(this.modifyBitmap1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "DropImageForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ajustar Imagen";
            this.Load += new System.EventHandler(this.DropImageForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picpos)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        internal Button btnGrabar;
        internal Button btnCancel;
        private ModifyBitmap modifyBitmap1;
        private Label lbbr;
        private Label lbtr;
        private Label lbbl;
        private Label lbtl;
        private PictureBox picpos;
        private Button button2;
        private Button button1;
        private Label label1;
    }
}