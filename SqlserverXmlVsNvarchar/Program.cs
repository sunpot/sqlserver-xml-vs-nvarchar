using System;

namespace SqlserverXmlVsNvarchar
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var context = new SqlserverXmlVsNvarcharContext();
            context.Add(new NvarcharEntity(){ Data = "test" });
            context.SaveChanges();
        }
    }
}
