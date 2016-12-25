using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace tokoton15_04
{
    public class Sample
    {
        public void SayHello(string s)
        {
            Console.WriteLine(s);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            foreach(var assem in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach(var t in assem.GetExportedTypes())
                {
                    if (t.Name == "Sample")
                    {
                        var instance = Activator.CreateInstance(t);
                        instance.GetType().InvokeMember(
                            "SayHello"
                            , BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod
                            , null
                            , instance
                            , new[] { "Hello Unknown Method!" }
                            );

                        dynamic instance2 = Activator.CreateInstance(t);
                        instance2.SayHello("Hello .Net Framework 4.0");
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
