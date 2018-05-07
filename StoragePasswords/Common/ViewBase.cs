using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace StoragePasswords.Common
{
    public abstract class ViewBase: UserControl
    {
        public ViewBase()
        {

        }
        public virtual Type TargetDataContext { get; set; }
        public abstract void Reset();
    }
}
