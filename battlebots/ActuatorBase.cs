using System;

namespace battlebots
{
    /*--------------------------------------------
    parameters:
        Id (string): Unique identifier for the actuator component
    function:
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
        public string _npId { get; set; } = "DefaultActuatorId";
        public bool _npIsActive { get; protected set; }

        public virtual string _npUniqueId
        {
            get => _npId;
            set => _npId = value;
        }

        public virtual void Activate()
        {
            _npIsActive = true;
        }

        public virtual void Deactivate()
        {
            _npIsActive = false;
        }
    }
}