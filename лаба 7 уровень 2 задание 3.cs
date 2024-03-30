using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace лаба_7_задание_3_уровень_2
{

    abstract class Discipline
    {
        protected string disciplineName;

        public abstract void DisplayDisciplineName();

        public string GetDisciplineName()
        {
            return disciplineName;
        }
    }

    class LongJump : Discipline
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

    class HighJump : Discipline
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

    class Participant
    {
        private string surname;
        private int result1;
        private int result2;
        private int result3;

        public Participant(string sur, int res1, int res2, int res3)
        {
            surname = sur;
            result1 = res1;
            result2 = res2;
            result3 = res3;
        }

        public string Surname
        {
            get { return surname; }
        }

        public int Res1
        {
            get { return result1; }
        }
        public int Res2
        {
            get { return result2; }
        }
        public int Res3
        {
            get { return result3; }
        }
        public void Print()
        {
            Console.WriteLine("Фамилия {0}  \t {1}; {2}; {3}", surname, result1, result2, result3);
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
                    if (Math.Max(Math.Max(longJumpparticipants[j].Res1, longJumpparticipants[j].Res2), longJumpparticipants[j].Res3) > Math.Max(Math.Max(longJumpparticipants[i].Res1, longJumpparticipants[i].Res2), longJumpparticipants[i].Res3))
                    {
                        Participant temp = longJumpparticipants[i]; longJumpparticipants[i] = longJumpparticipants[j];
                        longJumpparticipants[j] = temp;
                    }
                }
            }
            foreach (Participant p in longJumpparticipants)
            {
                p.Print();
            }

            Console.WriteLine();

            HighJump highJump = new HighJump();
            highJump.DisplayDisciplineName();

            for (int i = 0; i < highJumpparticipants.Length - 1; i++)
            {
                for (int j = i + 1; j < highJumpparticipants.Length; j++)
                {
                    if (Math.Max(Math.Max(highJumpparticipants[j].Res1, highJumpparticipants[j].Res2), highJumpparticipants[j].Res3) > Math.Max(Math.Max(highJumpparticipants[i].Res1, highJumpparticipants[i].Res2), highJumpparticipants[i].Res3))
                    {
                        Participant temp = highJumpparticipants[i]; highJumpparticipants[i] = highJumpparticipants[j];
                        highJumpparticipants[j] = temp;
                    }
                }
            }
            foreach (Participant p in highJumpparticipants)
            {
                p.Print();
            }
        }
    }
}

