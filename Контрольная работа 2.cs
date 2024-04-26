using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace контр_р_2
{
    abstract class Task
    {
        protected string text ;

        public abstract string Process(string text);

        public string Text
        {
            get => text;
            protected set => text = value;
        }

        public Task(string text)
        {
            this.text = text;
        } 
    }

    class Task1 : Task
    {
        public Task1(string text) : base(text) { }

        public override string ToString()
        {
            return this.text;
        }

        public override string Process(string text)
        {
            string[] words = text.Split(' ');
            string word1 = words[0];
            string word2 = words[1];

            foreach (char c in word2)
            {
                word1 = word1.Replace(c.ToString(), "");
            }

            return word1 + " "+ word2;
        }
    }
   
    class Program
    {
        static void Main()
        {
            Task[] tasks =
            {
            new Task1("Яркие краски"),
           
        };

           
            foreach (var task in tasks)
            {
                Console.WriteLine(task.Process(task.Text));
            }
        }
    }

}
