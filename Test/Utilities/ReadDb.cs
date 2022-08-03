using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Test.Models;

namespace Test.Utilities
{
    public class ReadDb
    {
        string PathDb = @"Resources\fakedatabase.js";
        public List<AdressBook> ReadFakeDb()
        {
            List<AdressBook> adressBooks = new List<AdressBook>();
            using (var sr = new StreamReader(PathDb))
            {
                string json = sr.ReadToEnd();
                adressBooks = JsonConvert.DeserializeObject<List<AdressBook>>(json);
                if (adressBooks == null)
                {
                    adressBooks = new List<AdressBook>();
                }
            }
            adressBooks = adressBooks.OrderBy(T => T.name).ToList();
            return adressBooks;

        }

        public void UpdateDeleteDb(List<AdressBook> adressBooks)
        {

            using (FileStream fs = File.OpenWrite(PathDb))
            {
                fs.SetLength(0);
                byte[] info = new UTF8Encoding(true).GetBytes(JsonConvert.SerializeObject(adressBooks));
                fs.Write(info, 0, info.Length);
                fs.Close();
            }
        }
    }
}
