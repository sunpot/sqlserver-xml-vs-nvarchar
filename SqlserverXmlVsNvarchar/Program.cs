using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

namespace SqlserverXmlVsNvarchar
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            TestStrRead();
            TestXmlRead();
        }

        static void TestStrRead()
        {
            var list = new List<NvarcharEntity>();
            var allElapsed = new TimeSpan();
            using(var context = new SqlserverXmlVsNvarcharContext())
            {
                for(var i = 0; i < 1000; i++)
                {
                    var sw = System.Diagnostics.Stopwatch.StartNew();
                    var tmp = context.NvarcharEntries.Where(x => x.Id == i).FirstOrDefault();
                    sw.Stop();
                    
                    list.Add(tmp);
                    allElapsed += sw.Elapsed;
                    //Console.WriteLine(sw.Elapsed);
                }
            }
            
            Console.WriteLine($"All elapsed in TestStr: {allElapsed}");
        }

        static void TestXmlRead()
        {
            var list = new List<XmlEntity>();
            var allElapsed = new TimeSpan();
            using(var context = new SqlserverXmlVsNvarcharContext())
            {
                for(var i = 0; i < 1000; i++)
                {
                    var sw = System.Diagnostics.Stopwatch.StartNew();
                    var tmp = context.XmlEntries.Where(x => x.Id == i).FirstOrDefault();
                    sw.Stop();
                    allElapsed += sw.Elapsed;
                    
                    list.Add(tmp);
                    //Console.WriteLine(sw.Elapsed);
                }
            }
            
            Console.WriteLine($"All elapsed in TestStr: {allElapsed}");
        }

        static void TestStrWrite()
        {
            var list = new List<NvarcharEntity>();
            var allElapsed = new TimeSpan();
            for(var i = 0; i < 1000; i++)
            {
                var context = new SqlserverXmlVsNvarcharContext();
                var sw = System.Diagnostics.Stopwatch.StartNew();
                var tmp = new NvarcharEntity{ Data = HugeStringCreator.Get500EmailsStr() };
                context.Add(tmp);
                context.SaveChanges();
                sw.Stop();
                
                allElapsed += sw.Elapsed;
                //Console.WriteLine(sw.Elapsed);
            }
            Console.WriteLine($"All elapsed in TestStr: {allElapsed}");
        }
        
        static void TestXmlWrite()
        {
            var list = new List<XmlEntity>();
            var allElapsed = new TimeSpan();
            for(var i = 0; i < 1000; i++)
            {
                var context = new SqlserverXmlVsNvarcharContext();
                var tmp = new XmlEntity{ Data = HugeStringCreator.Get500EmailsXml() };
                var sw = System.Diagnostics.Stopwatch.StartNew();
                context.Add(tmp);
                context.SaveChanges();
                sw.Stop();
                
                allElapsed += sw.Elapsed;
                //Console.WriteLine(sw.Elapsed);
            }
            Console.WriteLine($"All elapsed in TestXml: {allElapsed}");
        }
    }

    class HugeStringCreator
    {
        public static string Get500EmailsStr()
        {
            var addresses = string.Empty;
            for(var i = 0; i < 500; i++)
            {
                addresses += $"longlongemails.testing-{i}@somedomain.co.jp; ";
            }
            return addresses;
        }

        public static string Get500EmailsXml()
        {
            var items = new TestXmlSerializable();
            for(var i = 0; i < 500; i++)
            {
                items.Params.Add(new ExecutionParam("e-mail", $"longlongemails.testing-{i}@somedomain.co.jp; "));
            }
            var serializer = new XmlSerializer(typeof(TestXmlSerializable));
            var doc = new XmlDocument();
            var xmlstr = string.Empty;
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, items);
                doc.LoadXml(writer.ToString());
                xmlstr = writer.ToString();
            }

            return xmlstr;
        }
    }
    [Serializable]
    public class TestXmlSerializable
    {
        [XmlArray("params")]
        [XmlArrayItem("param")]
        public List<ExecutionParam> Params { get; set; }

        public TestXmlSerializable()
        {
            Params = new List<ExecutionParam>();
        }
    }

    [Serializable]
    public class ExecutionParam
    {
        /// <summary>
        /// システム名
        /// </summary>
        [XmlElement("key")]
        public string Key { get; set; }
        /// <summary>
        /// バージョン
        /// </summary>
        [XmlElement("value")]
        public string Value { get; set; }

        public ExecutionParam()
        {
            Key = string.Empty;
            Value = string.Empty;
        }
        public ExecutionParam(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
