using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McExample.BLL
{
    public static class Common
    {
        public static void WriteToFile(this Exception ex)
        {
            using (StreamWriter sw = new StreamWriter("app.log", true))
            {
                sw.WriteLine
                (
                    $"{DateTime.Now}\n{ex}\n"
                );
            }
        }
    }
}
