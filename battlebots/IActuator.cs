using System;

namespace battlebots
{
    /*--------------------------------------------
    parameeters:
        None - interface definition only
    funtion:
        Defines the contract for all actuator components in the battlebot system
        Activate(): Declares method to power on/enable an actuator
        Deactivate(): Declares method to power off/disable an actuator
    outputs:
        Activate(): void (to be implemented by derived classes)
        Deactivate(): void (to be implemented by derived classes)
    ---------------------------------------------*/
    public interface IActuator
    {
        void Activate();
        void Deactivate();
    }
}