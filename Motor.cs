using System;

namespace battlebots
{
    /*--------------------------------------------
    parameeters:
        Speed (int): The rotation speed of the motor (RPM or percentage)
        Id (string): Unique identifier for the actuator (inherited from ActuatorBase)
    funtion:
        Represents a physical motor used in the battlebot's movement system
        Inherits from ActuatorBase to provide standard actuator functionality
        Activate(): Powers on and starts the motor rotation
        Deactivate(): Powers off and stops the motor rotation
    outputs:
        Activate(): void - no return value
        Deactivate(): void - no return value
    ---------------------------------------------*/
    public class Motor : ActuatorBase
    {
        public int Speed { get; set; }

        public override void Activate() { }
        public override void Deactivate() { }
    }
}