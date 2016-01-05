using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public static class Common
    {
        private const string fileName = "log.txt";
        static Common()
        {
            if (File.Exists(fileName))
            {
                File.Create(fileName);
            }
        }
        public static void WriteText(string message)
        {
            using (StreamWriter sw = File.AppendText(fileName))
            {
                sw.WriteLine(message);
            }
        } 
    }
}
