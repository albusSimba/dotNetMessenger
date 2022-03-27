using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetMessengerDemo
{
    public class Letter
    {
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        public string Sender { get; set; } = "";
        public string Reciever { get; set; } = "";
        public object Content { get; set; } = null;

    }
}
