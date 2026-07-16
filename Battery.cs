using System;

namespace battlebots
{
    public class Battery
    {
        public double Voltage { get; set; }
        public double ChargeLevel { get; set; }

        public bool IsCharging() { return false; }
    }
}