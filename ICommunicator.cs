using System;

namespace battlebots
{
    /*--------------------------------------------
    parameeters:
        None - interface definition only
    funtion:
        Defines the contract for communication functionality
        SendMessage(string): Declares method to send messages through a communication channel
        ReceiveMessage(): Declares method to receive messages from a communication channel
    outputs:
        SendMessage(): void (to be implemented by derived classes)
        ReceiveMessage(): string (to be implemented by derived classes)
    ---------------------------------------------*/
    public interface ICommunicator
    {
        void SendMessage(string message);
        string ReceiveMessage();
    }
}