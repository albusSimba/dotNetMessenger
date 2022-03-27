using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetMessengerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Postman postman = new Postman();
            Subscriber a = new Subscriber("Person A");
            Ghost b = new Ghost("Person B");

            a.PostalService(postman.Mailbox);
            b.PostalService(postman.Mailbox);

            postman.AddSubscriber(a);
            postman.AddSubscriber(b);

            Console.ReadKey();
            //a.Send("Person B", "HelloWorld");
            b.Send(a.Name(), "HelloWorld");
            Console.ReadKey();
            postman.Stop();
            a.Stop();
            b.Stop();

            Console.ReadKey();




        }
    }
}
