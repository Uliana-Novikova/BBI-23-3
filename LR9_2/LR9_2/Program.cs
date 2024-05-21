using LR9_2.Serialize;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace лаба_7_задание_3_уровень_2
{

    public abstract class Discipline
    {
        protected string disciplineName;

        public abstract void DisplayDisciplineName();

        public string GetDisciplineName()
        {
            return disciplineName;
        }
    }

    public class LongJump : Discipline
    {
        public LongJump()
        {
            disciplineName = "Прыжки в длину";
        }

        public override void DisplayDisciplineName()
        {
            Console.WriteLine("Дисциплина: " + disciplineName);
        }
    }

    public class HighJump : Discipline
    {
        public HighJump()
        {
            disciplineName = "Прыжки в высоту";
        }

        public override void DisplayDisciplineName()
        {
            Console.WriteLine("Дисциплина: " + disciplineName);
        }
    }

    [Serializable]
    [ProtoContract]
    public class Participant
    {
        private string surname;
        private int result1;
        private int result2;
        private int result3;
        [JsonConstructor]
        public Participant(string surname, int result1, int result2, int result3)
        {
            Surname = surname;
            Result1 = result1;
            Result2 = result2;
            Result3 = result3;
        }
        public Participant() { }

        [XmlAttribute("Surname")]
        [ProtoMember(1)]
        public string Surname
        {
            get { return surname; }  set { surname = value; }
        }

        [XmlAttribute("Result1")]
        [ProtoMember(2)]
        public int Result1
        {
            get { return result1; }
            set { result1 = value; }
        }
        [XmlAttribute("Result2")]
        [ProtoMember(3)]
        public int Result2
        {
            get { return result2; }
            set { result2 = value; }
        }
        [XmlAttribute("Result3")]
        [ProtoMember(4)]
        public int Result3
        {
            get { return result3; }
            set { result3 = value; }
        }
        public void Print()
        {
            Console.WriteLine("Фамилия {0}  \t {1}; {2}; {3}", Surname, Result1, Result2, Result3);
        }

    }

    class Program
    {

        static void Main(string[] args)
        {
            Participant[] longJumpparticipants = new Participant[3];
            longJumpparticipants[0] = new Participant("Козлов", 1, 1, 2);
            longJumpparticipants[1] = new Participant("Новиков", 2, 2, 3);
            longJumpparticipants[2] = new Participant("Щербаков", 2, 2, 7);

            Participant[] highJumpparticipants = new Participant[2];
            highJumpparticipants[0] = new Participant("Наумов", 1, 2, 2);
            highJumpparticipants[1] = new Participant("Блинов", 5, 2, 1);

            Console.WriteLine("Протокол соревнований:\n");
            LongJump longJump = new LongJump();
            longJump.DisplayDisciplineName();


            for (int i = 0; i < longJumpparticipants.Length - 1; i++)
            {
                for (int j = i + 1; j < longJumpparticipants.Length; j++)
                {
                    if (Math.Max(Math.Max(longJumpparticipants[j].Result1, longJumpparticipants[j].Result2), longJumpparticipants[j].Result3) > Math.Max(Math.Max(longJumpparticipants[i].Result1, longJumpparticipants[i].Result2), longJumpparticipants[i].Result3))
                    {
                        Participant temp = longJumpparticipants[i]; longJumpparticipants[i] = longJumpparticipants[j];
                        longJumpparticipants[j] = temp;
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
                "example.json",
                "example.xml",
                "example.bin"
            };

            for (int i = 0; i < mySerializers.Length; i++)
            {
                System.IO.File.WriteAllText(Path.Combine(path, file_names[i]), string.Empty);
                mySerializers[i].Write(longJumpparticipants, Path.Combine(path, file_names[i]));
            }

            for (int i = 0; i < mySerializers.Length; i++)
            {
                var ans = mySerializers[i].Read(Path.Combine(path, file_names[i]));
                foreach(var j in ans)
                {
                    j.Print();
                }
            }

            Console.WriteLine();

            HighJump highJump = new HighJump();
            highJump.DisplayDisciplineName();

            for (int i = 0; i < highJumpparticipants.Length - 1; i++)
            {
                for (int j = i + 1; j < highJumpparticipants.Length; j++)
                {
                    if (Math.Max(Math.Max(highJumpparticipants[j].Result1, highJumpparticipants[j].Result2), highJumpparticipants[j].Result3) > Math.Max(Math.Max(highJumpparticipants[i].Result1, highJumpparticipants[i].Result2), highJumpparticipants[i].Result3))
                    {
                        Participant temp = highJumpparticipants[i]; highJumpparticipants[i] = highJumpparticipants[j];
                        highJumpparticipants[j] = temp;
                    }
                }
            }
            for (int i = 0; i < mySerializers.Length; i++)
            {
                System.IO.File.WriteAllText(Path.Combine(path, file_names[i]), string.Empty);
                mySerializers[i].Write(highJumpparticipants, Path.Combine(path, file_names[i]));
            }

            for (int i = 0; i < mySerializers.Length; i++)
            {
                var ans = mySerializers[i].Read(Path.Combine(path, file_names[i]));
                foreach (var j in ans)
                {
                    j.Print();
                }
            }

        }
    }
}

