using System;

namespace battlebots
{
    public interface ICommunicator
    {
        void SendMessage(string message);
        string ReceiveMessage();
    }
}