using System;

namespace battlebots
{
    public abstract class CommunicationBase : ICommunicator
    {
        public string PortName { get; set; }
        public int BaudRate { get; set; }

        public virtual void SendMessage(string message) { }
        public virtual string ReceiveMessage() { return ""; }
    }
}