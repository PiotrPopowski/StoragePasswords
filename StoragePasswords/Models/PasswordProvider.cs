using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoragePasswords.Models
{
    class PasswordProvider
    {
        public PasswordProvider(Passwords passwordObject)
        {
            Passwords = passwordObject;
        }
        public PasswordProvider() { }

        public Passwords Passwords { get; private set; }

        /// <summary>
        /// Determinates wheter given arguments equals encrypted
        /// </summary>
        /// <param name="password"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        public bool VerifyPassword(string password, string login)
        {
            string saltKey = CryptographyService.Decrypt(Passwords.SaltKey, password, Passwords.VIKey, login);
            string encryptedLogin = CryptographyService.Encrypt(login, password, Passwords.VIKey, saltKey);
            string encryptedPassword = CryptographyService.Encrypt(password, login, Passwords.VIKey, saltKey);

            if ((encryptedLogin + encryptedPassword).Equals(Passwords.Login + Passwords.MainPassword))
                return true;
            return false;
        }
        public void Decrypt(string password,string login)
        {
            string saltKey = CryptographyService.Decrypt(Passwords.SaltKey, password, Passwords.VIKey, login);

            ObservableCollection<string> pswdCollection = new ObservableCollection<string>();
            pswdCollection = CryptographyService.Decrypt(Passwords.Collection, password, Passwords.VIKey, saltKey);
            Passwords = new Passwords(login, password, saltKey, Passwords.VIKey, pswdCollection);
        }
        public void Encrypt()
        {
            string mainPassword = CryptographyService.Encrypt(Passwords.MainPassword, 
                Passwords.Login, Passwords.VIKey, Passwords.SaltKey);
            string login = CryptographyService.Encrypt(Passwords.Login, Passwords.MainPassword,
                Passwords.VIKey, Passwords.SaltKey);
            string saltKey = CryptographyService.Encrypt(Passwords.SaltKey, Passwords.MainPassword,
                Passwords.VIKey, Passwords.Login);

            ObservableCollection<string> pswCollection=
                CryptographyService.Encrypt(
                    Passwords.Collection,Passwords.MainPassword,Passwords.SaltKey,Passwords.Login);

            Passwords = new Passwords(login, mainPassword, saltKey, Passwords.VIKey,pswCollection);
        }
        public void Load(string fileName)
        {
            Passwords= BinaryIOService.Load(fileName) as Passwords;
        }
        public void Save(string fileName)
        {
            BinaryIOService.Save(Passwords, fileName);
        }
    }
}
