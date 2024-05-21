using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using LR9_1.Serialize;

namespace лаб_7_уровень_1_задание_1
{
    [Serializable]
    [ProtoContract]
    public class Participant
    {
        private string surname;
        private string society;
        private int result1;
        private int result2;
        private bool disqualified;

        [JsonConstructor]
        public Participant(string surname, string society, int result1, int result2, bool disqualified)
        {
            Surname = surname;
            Society = society;
            Result1 = result1;
            Result2 = result2;
            Disqualified = disqualified;
        }

        public Participant() { }
        [XmlAttribute("Surname")]
        [ProtoMember(1)]
        public string Surname
        {
            get { return surname; }
            set {  surname = value; }
        }

        [XmlAttribute("Society")]
        [ProtoMember(2)]
        public string Society
        {
            get { return society; }
            set { society = value; }
        }

        [XmlAttribute("FirstAttempt")]
        [ProtoMember(3)]
        public int Result1
        {
            get { return result1; }
            set {  result1 = value; }
        }

        [XmlAttribute("SecondAttemt")]
        [ProtoMember(4)]
        public int Result2
        {
            get { return result2; }
            set { result2 = value; }
        }

        [XmlAttribute("Disqualified")]
        [ProtoMember(5)]
        public bool Disqualified
        {
            get { return disqualified; }
            set { disqualified = value; }
        }
        [JsonIgnore]
        public int TotalScore
        {
            get { return result1 + result2; }
        }

        public void Disqualification()
        {
            if (!disqualified) disqualified = true;
        }

        public void Print()
        {
            Console.WriteLine("Фамилия {0} \t {1} общество \t\t {2} {3}", surname, society, result1, result2);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Participant[] participant = new Participant[4]
            {
            new Participant("Иванова"," A",5,4, true),
            new Participant("Петрова"," B",4,6, false),
            new Participant("Зайцева"," A",6,8, false),
            new Participant("Симонова"," C",5,2, false)
            };

            for (int i = 0; i < participant.Length - 1; i++)
            {
                for (int j = 0; j < participant.Length - i - 1; j++)
                {
                    if (participant[j].TotalScore < participant[j + 1].TotalScore)
                    {
                        Participant temp = participant[j];
                        participant[j] = participant[j + 1];
                        participant[j + 1] = temp;
                    }
                }
            }

            string path = @"C:\Users\user\Desktop"; //путь до рабочего стола
            string folderName = "Test";
            path = Path.Combine(path, folderName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Serialize[] mySerializers = [
                new JsonSer(),
                new XmlSer(),
                new BinSer()];
            string[] file_names = new string[]
            {
                "result.json",
                "result.xml",
                "result.bin"
            };

            for (int i = 0; i < mySerializers.Length; i++)
            {
                mySerializers[i].Write(participant, Path.Combine(path, file_names[i]));
            }

            for (int i = 0; i < mySerializers.Length; i++)
            {
                var ans = mySerializers[i].Read(Path.Combine(path, file_names[i]));
                for (int j = 0; j < ans.Length; j++)
                {
                    if (ans[j].Disqualified == false)
                    {
                        ans[j] = new Participant(ans[j].Surname, ans[j].Society, ans[j].Result1, ans[j].Result2, ans[j].Disqualified);
                        ans[j].Print();
                    }
                }
            }
            
        }
    }
}
