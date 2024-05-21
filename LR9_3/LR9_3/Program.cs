using LR9_3.Serialize;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using лаба_7_задание_5_уровень_3;

namespace лаба_7_задание_5_уровень_3
{
    [Serializable]
    [ProtoContract]
    [XmlInclude(typeof(WomenFootballTeam)), ProtoInclude(10,typeof(WomenFootballTeam))]
    [XmlInclude(typeof(MenFootballTeam)), ProtoInclude(11, typeof(MenFootballTeam))]
    public class FootballTeam
    {
        protected string Name;
        protected int GoalsScored;
        protected int GoalsConceded;
        protected int Points;
        protected int GoalsDifference;
        protected string Gender;

        public FootballTeam(string name, int goalsScored, int goalsConceded)
        {
            Name = name;
            GoalsConceded = goalsConceded;
            GoalsScored = goalsScored;
            GoalsDifference = goalsScored - goalsConceded;

        }
        public FootballTeam() { 
        }
        [XmlAttribute("Point")]
        [ProtoMember(1)]
        public int points
        {
            get { return Points; }
            set { Points = value; }
        }

        [XmlAttribute("Name")]
        [ProtoMember(2)]
        public string name
        {
            get { return Name; }
            set { Name = value; }
        }

        [XmlAttribute("GoalsScored")]
        [ProtoMember(3)]
        public int goalsScoard
        {
            get { return GoalsScored; }
            set { GoalsScored = value; }

        }
        [XmlAttribute("GoalsConceded")]
        [ProtoMember(4)]
        public int goalsConceded
        {
            get { return GoalsConceded; }
            set { GoalsConceded = value; }
        }

        [XmlAttribute("GoalsDifference")]
        [ProtoMember(5)]
        public int goalsDifference
        {
            get { return GoalsDifference; }
            set { GoalsDifference = value; }
        }

        [XmlAttribute("Genders")]
        [ProtoMember(6)]
        public string gender
        {
            get { return Gender; }
            set { Gender = value; }
        }
        public void Print()
        {
            Console.WriteLine("Название команды {0} {1} \t {2}; {3}; {4}", Name, Gender, GoalsScored, GoalsConceded, Points);
        }
    }

    [Serializable]
    public class WomenFootballTeam : FootballTeam
    {
        public WomenFootballTeam(string name, int goalsScored, int goalsConceded) : base(name, goalsScored, goalsConceded)
        {
            Gender = "женская команда";
        }
        public WomenFootballTeam() { }
    }

    [Serializable]
    public class MenFootballTeam : FootballTeam
    {
        public MenFootballTeam(string name, int goalsScored, int goalsConceded) : base(name, goalsScored, goalsConceded)
        {
            Gender = "мужская команда";
        }
        public MenFootballTeam() { }
    }


    class Program
    {
        static void Main(string[] args)
        {
            FootballTeam[] womenteams = new WomenFootballTeam[3];
            womenteams[0] = new WomenFootballTeam("команда 1", 0, 2);
            womenteams[1] = new WomenFootballTeam("команда 2", 4, 7);
            womenteams[2] = new WomenFootballTeam("команда 3", 4, 0);


            FootballTeam[] menteams = new MenFootballTeam[2];
            menteams[0] = new MenFootballTeam("команда 4", 1, 8);
            menteams[1] = new MenFootballTeam("команда 5", 2, 3);

            for (int i = 0; i < womenteams.Length; i++)
            {
                if (womenteams[i].goalsScoard > womenteams[i].goalsConceded)
                    womenteams[i].points = 3;
                else if (womenteams[i].goalsScoard == womenteams[i].goalsConceded)
                    womenteams[i].points = 1;
            }

            for (int i = 0; i < menteams.Length; i++)
            {
                if (menteams[i].goalsScoard > menteams[i].goalsConceded)
                    menteams[i].points = 3;
                else if (menteams[i].goalsScoard == menteams[i].goalsConceded)
                    menteams[i].points = 1;
            }
            FootballTeam[] teams = new FootballTeam[womenteams.Length + menteams.Length];
            for (int i = 0; i < womenteams.Length; i++)
            {
                teams[i] = womenteams[i];
            }
            for (int i = 0; i < menteams.Length; i++)
            {
                teams[womenteams.Length + i] = menteams[i];
            }
            for (int i = 0; i < teams.Length - 1; i++)
            {
                for (int j = i + 1; j < teams.Length; j++)
                {
                    if (teams[i].points < teams[j].points ||
                        (teams[i].points == teams[j].points && (teams[i].goalsDifference) < (teams[j].goalsDifference)))
                    {
                        FootballTeam temp = teams[i];
                        teams[i] = teams[j];
                        teams[j] = temp;
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
                mySerializers[i].Write(teams, Path.Combine(path, file_names[i]));
            }

            for(int i = 0; i < 3; ++i)
            {
                var ans = mySerializers[i].Read(Path.Combine(path, file_names[i]));
                foreach (var t in ans)
                {
                    t.Print();
                }
            }
        }
    }
}

