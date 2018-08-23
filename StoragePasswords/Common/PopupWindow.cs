using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace StoragePasswords.Common
{
    public abstract class PopupWindow: Window
    {
        public PopupWindow()
        {

        }
        public virtual Type TargetDataContext { get; set; }
    }
}
