using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using лаб_7_уровень_1_задание_1;
namespace LR9_1.Serialize
{
        public abstract class Serialize
        {
            public abstract Participant[] Read(string filePath);
            public abstract void Write(Participant[] obj, string filePath);
        }

        public class XmlSer : Serialize
        {
            public override Participant[] Read(string filePath)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Participant[]));
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    return (Participant[])serializer.Deserialize(fs);

                }
            }
            public override void Write(Participant[] obj, string filePath)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Participant[]));
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    serializer.Serialize(fs, obj);
                }
            }
        }

        public class JsonSer : Serialize
        {
            public override Participant[] Read(string filePath)
            {
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    return JsonSerializer.Deserialize<Participant[]>(fs);
                }
            }
            public override void Write(Participant[] obj, string filePath)
            {
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    JsonSerializer.Serialize<Participant[]>(fs, obj);
                }
            }
        }
    public class BinSer : Serialize
    {
        public override Participant[] Read(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                return Serializer.Deserialize<Participant[]>(fs);
            }
        }

        public override void Write(Participant[] obj, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                Serializer.Serialize(fs, obj);
            }
        }
    }
}
