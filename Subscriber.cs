using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotNetMessengerDemo
{
    public class Subscriber: IMessage
    {
        public Subscriber(string name)
        {
            this.name = name;
            var tokenSource = new CancellationTokenSource();
            ct = tokenSource.Token;
            inLetters = new BlockingCollection<Letter>();

            Task.Run(() =>
            {
                try
                {

                    while (ct.IsCancellationRequested == false)
                    {
                        RunProgram();
                    }

                }
                catch (InvalidOperationException)
                {
                    // An InvalidOperationException means that Take() was called on a completed collection
                    Console.WriteLine($"{name} : That's All!");
                }

            });

        }

        internal string name;
        private CancellationToken ct;
        internal BlockingCollection<Letter> inLetters;
        PostmanService postmanService = null;

        public string Name()
        {
            return name;
        }

        public void Send(string reciever, object content)
        {
            if (postmanService != null)
            {
                Letter letter = new Letter()
                {
                    Sender = name,
                    Reciever = reciever,
                    Content = content
                };

                postmanService(letter);
            }
        }

        public void Receive(Letter letter)
        {
            inLetters.Add(letter);
        }

        public void PostalService(PostmanService postmanService)
        {
            this.postmanService = postmanService;
        }

        public void Stop()
        {
            inLetters.CompleteAdding();
        }

        public void PostFailure(Letter letter)
        {
            Console.WriteLine($"Fail to send letter to {letter.Reciever}");
        }

        internal virtual void RunProgram()
        {
            Letter letter = inLetters.Take();
            string message = (string)letter.Content;
            Console.WriteLine($"{letter.Sender} : {message}");
            Send(letter.Sender, message + " too");

        }
    }
}
