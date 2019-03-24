using System;
using System.Collections.Generic;

namespace TestStringSerialization
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataObject = new DataObject()
            {
                Name =  "John",
                Surname = "Smith",
                Diseases = new List<string>() { "HIV", "Stroke" }
            };
            string str1 = dataObject.GetSerializedToJson();
            //string str1 = dataObject.GetSerializedToXml();

            Console.WriteLine(str1);
            Console.WriteLine();


            var dataObject2 = new DataObject();
            dataObject2.DeserializeFromJsonString(str1);
            //dataObject2.DeserializeFromXmlString(str1);
            dataObject2.Name = "Abraham";
            dataObject2.Surname = "Willingstone";
            dataObject2.Diseases.Add("Schizophrenia");

            string str2 = dataObject2.GetSerializedToJson();
            //string str2 = dataObject2.GetSerializedToXml();

            Console.WriteLine(str2);
            Console.Read();
        }
    }
}
