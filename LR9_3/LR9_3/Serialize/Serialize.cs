using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using лаба_7_задание_5_уровень_3;

namespace LR9_3.Serialize
{
    public abstract class Serialize
    {
        public abstract FootballTeam[] Read(string filePath);
        public abstract void Write(FootballTeam[] obj, string filePath);
    }

    public class XmlSer : Serialize
    {
        public override FootballTeam[] Read(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(FootballTeam[]));
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                return (FootballTeam[])serializer.Deserialize(fs);

            }
        }
        public override void Write(FootballTeam[] obj, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(FootballTeam[]));
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, obj);
            }
        }
    }

    public class JsonSer : Serialize
    {
        public override FootballTeam[] Read(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                return JsonSerializer.Deserialize<FootballTeam[]>(fs);
            }
        }
        public override void Write(FootballTeam[] obj, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize<FootballTeam[]>(fs, obj);
            }
        }
    }
    public class BinSer : Serialize
    {
        public override FootballTeam[] Read(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                return Serializer.Deserialize<FootballTeam[]>(fs);
            }
        }

        public override void Write(FootballTeam[] obj, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                Serializer.Serialize(fs, obj);
            }
        }
    }
}
