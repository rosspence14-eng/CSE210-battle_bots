using System;

namespace battlebots
{
    /*--------------------------------------------
    parameeters:
        Voltage (double): The current voltage level of the battery
        ChargeLevel (double): The remaining charge percentage/level
    funtion:
        Manages the power source for the battlebot, tracking voltage and charge levels
        IsCharging(): Checks if the battery is currently charging
    outputs:
        IsCharging(): bool - returns false (placeholder implementation)
    ---------------------------------------------*/
    public class Battery
    {
        public double Voltage { get; set; }
        public double ChargeLevel { get; set; }

        public bool IsCharging() { return false; }
    }
}