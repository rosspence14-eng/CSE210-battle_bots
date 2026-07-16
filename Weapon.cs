using System;

namespace battlebots
{
    public abstract class Weapon : ActuatorBase
    {
        public virtual void Attack() { }
    }
}