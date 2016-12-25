using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace tokoton15_01
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach(var assem in AppDomain.CurrentDomain.GetAssemblies())
            {
                AssemblyTitleAttribute asmttl =
                    (AssemblyTitleAttribute)Attribute.GetCustomAttribute(assem,
                    typeof(AssemblyTitleAttribute));
                if (asmttl != null)
                {
                    Console.WriteLine($"{asmttl.Title}");
                }
            }

            Console.ReadLine();
        }
    }
}
