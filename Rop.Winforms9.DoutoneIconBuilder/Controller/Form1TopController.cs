using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rop.Winforms8._1.DoutoneIconBuilder.Controller
{
    public class Form1TopController:BaseController<Form1>
    {
        public Form1Controller Controller => ParentForm.Controller;
        public Form1Status Status => Controller.Status;
        public Form1TopController(Form1 parentForm) : base(parentForm)
        {
            
        }
        protected override void Init()
        {
            ParentForm.labelcabecera.Text = ParentForm.Controller.BankJson.BankPath.ToString();
            ParentForm.btnclose.Click += Btnclose_Click;
            ParentForm.btntop.Click += Btntop_Click;
            ParentForm.Controller.StatusChanged += Controller_StatusChanged;
        }

        private void Controller_StatusChanged(object? sender, EventArgs e)
        {
            switch (Status)
            {
                case Form1Status.None:
                    ParentForm.btntop.Enabled = true;
                    break;
                case Form1Status.Selected:
                    ParentForm.btntop.Enabled = true;
                    break;
                case Form1Status.Editing:
                    ParentForm.btntop.Enabled = false;
                    break;
                default:
                    ParentForm.btntop.Enabled = false;
                    break;
            }
        }
        private void Btntop_Click(object? sender, EventArgs e)
        {
            using var f = new InputDialog();
            f.BankName = Controller.BankJson.Name;
            f.BaseSize = Controller.BankJson.BaseSize;
            var r=f.ShowDialog();
            if (r!=DialogResult.OK) return;
            Controller.UpdateBankJson(Controller.BankJson with
            {
                Name = f.BankName,
                BaseSize = f.BaseSize,
                Initials = f.Initials
            });

        }

        private void Btnclose_Click(object? sender, EventArgs e)
        {
            ParentForm.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
