using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace StoragePasswords.Models
{
    static class BinaryIOService
    {

        static public void Save(object graph,string name)
        {
            using (FileStream fs = new FileStream(name + ".dat", FileMode.Create, FileAccess.Write))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    formatter.Serialize(fs, graph);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("Couldn't save object {0}.dat info:\n" + e.Message,name);
                    throw;
                }
            }
        }
        static public object Load(string fileName)
        {
            if (!File.Exists(fileName + ".dat"))
            {
                Console.WriteLine("Couldn't find file {0}.dat", fileName);
                return null;
            }
            using (FileStream fs = new FileStream(fileName + ".dat", FileMode.Open, FileAccess.Read))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    return formatter.Deserialize(fs);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("Couldn't open file {0}.dat info:\n" + e.Message, fileName);
                    throw;
                }
            }
        }
    }
}
