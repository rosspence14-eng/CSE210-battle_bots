using System;

namespace battlebots
{
    public class Motor : ActuatorBase
    {
        public int Speed { get; set; }

        public override void Activate() { }
        public override void Deactivate() { }
    }
}