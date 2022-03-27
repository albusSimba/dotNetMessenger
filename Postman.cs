using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotNetMessengerDemo
{
    public delegate void PostmanService (Letter letter);
    public class Postman
    {
        public Postman()
        {
            tokenSource = new CancellationTokenSource();
            var ct = tokenSource.Token;
            Letters = new BlockingCollection<Letter>();

            task = Task.Run(() =>
            {
                try
                {

                    while (ct.IsCancellationRequested == false)
                    {
                        Letter letter = Letters.Take();

                        try
                        {                        
                            if (Subscribers.ContainsKey(letter.Reciever))
                            {
                                IMessage subcriber = Subscribers[letter.Reciever];
                                subcriber.Receive(letter);
                                Console.WriteLine($"Sending letter from {letter.Sender} to {letter.Reciever}");
                            }
                            else if (Subscribers.ContainsKey(letter.Sender))
                            {
                                IMessage subcriber = Subscribers[letter.Sender];
                                subcriber.PostFailure(letter);
                            }
                        }
                        catch
                        {

                        }
                    }

                }
                catch (InvalidOperationException)
                {
                    // An InvalidOperationException means that Take() was called on a completed collection
                    Console.WriteLine("Postal service stopped!");
                }

            });

        }

        private Dictionary<string, IMessage> Subscribers = new Dictionary<string, IMessage>();
        private CancellationTokenSource tokenSource;
        private BlockingCollection<Letter> Letters;
        private Task task;

        public void AddSubscriber(IMessage subcriber)
        {
            if (!Subscribers.ContainsKey(subcriber.Name()))
            {
                Subscribers.Add(subcriber.Name(), subcriber);
            }
        }

        public void Mailbox(Letter letter)
        {
            try
            {
                Letters.Add(letter);
            }
            catch
            {

            }
        }

        public void Stop()
        {
            Letters.CompleteAdding();
            //tokenSource.Cancel();
            //await Task.WhenAll(task);
        }
    }
}
