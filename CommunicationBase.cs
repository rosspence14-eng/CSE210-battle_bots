using System;

namespace battlebots
{
    /*--------------------------------------------
    parameeters:
        PortName (string): The communication port identifier (e.g., COM1, /dev/ttyUSB0)
        BaudRate (int): The data transmission speed in bits per second
    funtion:
        Abstract base class providing core communication functionality
        Implements ICommunicator interface for sending and receiving messages
        SendMessage(string): Transmits a message through the communication port
        ReceiveMessage(): Retrieves incoming messages from the communication port
    outputs:
        SendMessage(): void - no return value
        ReceiveMessage(): string - returns the received message (empty string placeholder)
    ---------------------------------------------*/
    public abstract class CommunicationBase : ICommunicator
    {
        public string PortName { get; set; }
        public int BaudRate { get; set; }

        public virtual void SendMessage(string message) { }
        public virtual string ReceiveMessage() { return ""; }
    }
}