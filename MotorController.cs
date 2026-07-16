using System;

namespace battlebots
{
    /*--------------------------------------------
    parameeters:
        PortName (string): Communication port for motor controller (inherited from CommunicationBase)
        BaudRate (int): Data transmission rate for communication (inherited from CommunicationBase)
    funtion:
        Controls motor operations through a communication interface
        Inherits from CommunicationBase to send/receive motor control commands
        SetMotorSpeed(int): Adjusts the speed of connected motors
    outputs:
        SetMotorSpeed(): void - no return value
    ---------------------------------------------*/
    public class MotorController : CommunicationBase
    {
        public void SetMotorSpeed(int speed) { }
    }
}