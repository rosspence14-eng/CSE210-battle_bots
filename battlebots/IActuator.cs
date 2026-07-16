using System;

namespace battlebots
{
    public interface IActuator
    {
        void Activate();
        void Deactivate();
    }
}