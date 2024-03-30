using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace лаб_7_уровень_1_задание_1
{

    class Participant
    {
        private string surname;
        private string society;
        private int result1;
        private int result2;
        private bool disqualified;

        public Participant(string sur, string soc, int r1, int r2, bool dis)
        {
            surname = sur;
            society = soc;
            result1 = r1;
            result2 = r2;
            disqualified = dis;
        }

        public string Surname
        {
            get { return surname; }
        }

        public string Society
        {
            get { return society; }
        }

        public int FirstAttempt
        {
            get { return result1; }
        }

        public int SecondAttempt
        {
            get { return result2; }
        }

        public bool Disqualified
        {
            get { return disqualified; }
        }

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

            for (int i = 0; i < 4; i++)
            {
                if (participant[i].Disqualified == false)
                {
                    participant[i] = new Participant(participant[i].Surname, participant[i].Society, participant[i].FirstAttempt, participant[i].SecondAttempt, participant[i].Disqualified);
                    participant[i].Print();
                }
            }
        }
    }
}
