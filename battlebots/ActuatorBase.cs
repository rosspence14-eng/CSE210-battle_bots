using System;

namespace battlebots
{
    public abstract class ActuatorBase : IActuator
    {
        public string Id { get; set; }

        public virtual void Activate() { }
        public virtual void Deactivate() { }
    }
}