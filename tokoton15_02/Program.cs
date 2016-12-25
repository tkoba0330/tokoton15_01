using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tokoton15_02
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach(var assem in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach ( var t in assem.GetExportedTypes().Where(x => x.FullName.IndexOf("System.String") >= 0))
                {
                    //if (t.FullName.IndexOf("System.String") < 0) continue;
                    Console.WriteLine(t.FullName);
                }
            }

            Console.ReadLine();
        }
    }
}
