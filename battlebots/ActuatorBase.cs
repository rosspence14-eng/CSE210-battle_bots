using System;

namespace battlebots
{
    /*--------------------------------------------
    parameeters:
        Id (string): Unique identifier for the actuator component
    funtion:
        Abstract base class providing standard functionality for all actuators in the system
        Implements IActuator interface to define common actuator behavior
        Activate(): Virtual method to power on/enable the actuator
        Deactivate(): Virtual method to power off/disable the actuator
    outputs:
        Activate(): void - no return value (to be implemented by derived classes)
        Deactivate(): void - no return value (to be implemented by derived classes)
    ---------------------------------------------*/
    public abstract class ActuatorBase : IActuator
    {
        public string Id { get; set; }

        public virtual void Activate() { }
        public virtual void Deactivate() { }
    }
}