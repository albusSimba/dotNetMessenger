using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace dotNetMessengerDemo
{
    public class Ghost : Subscriber
    {
        public Ghost(string name) : base(name)
        {

        }
        internal override void RunProgram()
        {
            Letter letter = inLetters.Take();
            string message = (string)letter.Content;
            Console.WriteLine($"{letter.Sender} : {message}");
            //Send(letter.Sender, message + " too");

        }

    }
}
