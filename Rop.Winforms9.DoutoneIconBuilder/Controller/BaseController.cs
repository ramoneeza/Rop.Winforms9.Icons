using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rop.Winforms8._1.DoutoneIconBuilder.Controller
{
    public abstract class BaseController<T> where T:Form
    {
        public T ParentForm { get; }
        protected BaseController(T parentForm)
        {
            ParentForm = parentForm;
            ParentForm.Load += ParentForm_Load;
        }

        private async void ParentForm_Load(object? sender, EventArgs e)
        {
            Init();
            await InitAsync();
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        protected virtual async ValueTask InitAsync()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
        }

        protected virtual void Init()
        {
        }
    }
}
