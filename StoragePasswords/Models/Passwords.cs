using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoragePasswords.Models
{
    [Serializable]
    class Passwords
    {
        public Passwords(string login, string password, string saltKey, string vIKey)
        {
            int bcount = Encoding.UTF8.GetByteCount(vIKey);
            Login = login;
            MainPassword = password;
            SaltKey = saltKey;
            VIKey = vIKey;
            Collection = new ObservableCollection<string>();
        }

        public Passwords(string login, string password, string saltKey, string vIKey,ObservableCollection<string> passwordCollection)
        {
            Login = login;
            MainPassword = password;
            SaltKey = saltKey;
            VIKey = vIKey;
            Collection = passwordCollection;
        }

        public string Login { get; }
        public string MainPassword { get; }
        public string SaltKey { get;}
        public string VIKey { get; }

        public ObservableCollection<string> Collection { get; }
    }
}
