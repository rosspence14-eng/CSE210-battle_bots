using System;

namespace battlebots
{
    /*--------------------------------------------
    PARAMETERS:
        Voltage (double): The current voltage level of the battery
        ChargeLevel (double): The remaining charge percentage/level
    FUNCTION:
        Manages the power source for the battlebot, tracking voltage and charge levels.
        Supports basic charging and discharge operations for the robot's power system.
    OUTPUTS:
        IsCharging(): bool - returns whether the battery is currently charging
    ---------------------------------------------*/
    public class Battery
    {
        private bool _npIsCharging;

        public double Voltage { get; set; }
        public double ChargeLevel { get; set; }

        public Battery()
        {
            Voltage = 12.0;
            ChargeLevel = 100.0;
            _npIsCharging = false;
        }

        public Battery(double voltage, double chargeLevel)
        {
            Voltage = voltage;
            ChargeLevel = Math.Max(0, Math.Min(100, chargeLevel));
            _npIsCharging = false;
        }

        public bool IsCharging()
        {
            return _npIsCharging;
        }

        public void StartCharging()
        {
            _npIsCharging = true;
            Console.WriteLine("[BATTERY] Charging started");
        }

        public void StopCharging()
        {
            _npIsCharging = false;
            Console.WriteLine("[BATTERY] Charging stopped");
        }

        public void Drain(double amount)
        {
            ChargeLevel = Math.Max(0, ChargeLevel - amount);
            Console.WriteLine($"[BATTERY] Battery drained to {ChargeLevel:F1}%");
        }

        public void Recharge(double amount)
        {
            ChargeLevel = Math.Min(100, ChargeLevel + amount);
            Console.WriteLine($"[BATTERY] Battery recharged to {ChargeLevel:F1}%");
        }

        public void DisplayStatus()
        {
            Console.WriteLine($"[BATTERY] Voltage: {Voltage:F1}V | Charge: {ChargeLevel:F1}% | Charging: {_npIsCharging}");
        }
    }
}