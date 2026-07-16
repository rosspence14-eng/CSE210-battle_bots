using System;

namespace battlebots
{
    /*--------------------------------------------
    parameeters:
        Id (string): Unique identifier for the weapon actuator (inherited from ActuatorBase)
    funtion:
        Abstract base class for all weapon types in the battlebot system
        Inherits from ActuatorBase to provide standard actuator functionality
        Attack(): Virtual method that derived weapons override to implement specific attack behaviors
    outputs:
        Attack(): void - no return value (to be implemented by derived classes)
    ---------------------------------------------*/
    public abstract class Weapon : ActuatorBase
    {
        public virtual void Attack() { }
    }
}