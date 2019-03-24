using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace TestStringSerialization
{
    public class DataObject
    {
        [XmlElement("name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [XmlElement("surname")]
        [JsonProperty("surname")]
        public string Surname { get; set; }

        [XmlArray("diseases")]
        [JsonProperty("diseases")]
        public List<string> Diseases { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public int Age = 20;


        public override string ToString()
        {
            return this.GetSerializedToXml();
        }


        public string GetSerializedToXml()
        {
           // Note, it is important to use toSerialize.GetType() instead of typeof(T)
           // in XmlSerializer constructor: if you use the first one the code covers
           // all possible subclasses of T(which are valid for the method), while using
           // the latter one will fail when passing a type derived from T.
           XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, this);
                return textWriter.ToString();
            }
        }


        public string GetSerializedToJson()
        {
            return JsonConvert.SerializeObject(this);
        }


        public void DeserializeFromXmlString(string xmlStr)
        {
            XmlSerializer serializer = new XmlSerializer(this.GetType());
            DataObject obj;

            using (TextReader reader = new StringReader(xmlStr))
            {
                obj = (DataObject)serializer.Deserialize(reader);
            }

            copyAllPublicPropsFrom(obj);
        }

        public void DeserializeFromJsonString(string jsonStr)
        {
            DataObject obj = JsonConvert.DeserializeObject<DataObject>(jsonStr);
            copyAllPublicPropsFrom(obj);
        }


        private void copyAllPublicPropsFrom(DataObject obj)
        {
            var publicProperties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo propertyInfo in publicProperties)
            {
                propertyInfo.SetValue(this, propertyInfo.GetValue(obj));
            }
        }

    }
}
