using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace tokoton15_03
{
    public class AutoFlagsAttribute : Attribute { }

    public class FlagAttribute : Attribute { }

    [AutoFlags]
    class A
    {
        [Flag]
        public static int ValueA = 123;
    }

    class B
    {
        public static int ValueB = 456;
    }

    

    class Program
    {
        private static void walkAll(Action<System.Reflection.FieldInfo> action)
        {
            foreach(var assem in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach(var t in assem.GetTypes().Where(x => x.GetCustomAttributes(typeof(AutoFlagsAttribute),true).Length != 0))
                {
                    foreach(var f in t.GetFields(BindingFlags.Public | BindingFlags.Static))
                    {
                        var query1 = from n2 in f.GetCustomAttributes(true)
                                     where n2 is FlagAttribute
                                     select n2;
                        if (query1.Count() > 0) action(f);
                    }
                }
            }
        }

        private static void save(string filename)
        {
            using (XmlTextWriter writer = new XmlTextWriter(filename, System.Text.Encoding.UTF8))
            {
                writer.WriteStartElement("flags");
                walkAll((field) =>
                {
                    if(field.FieldType == typeof(int))
                    {
                        writer.WriteElementString(field.Name, ((int)field.GetValue(null)).ToString());
                    }
                }
                );
            }

        }

        private static void load(string filename)
        {
            var x = XDocument.Load(filename);
            walkAll((field) => 
            {
                if(field.FieldType == typeof(int))
                {
                    field.SetValue(null, int.Parse(x.Element("flags").Element(field.Name).Value));
                }
            });
        }



        static void Main(string[] args)
        {
            string filename = "file_name_here.xml";

            save(filename);

            A.ValueA = -1;
            B.ValueB = -2;

            load(filename);

            Console.WriteLine(A.ValueA);
            Console.WriteLine(B.ValueB);

            Console.ReadLine();
        }
    }
}
