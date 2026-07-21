using System;

namespace battlebots
{
    public class MotorController
    {
        private string _port;
        private int _baudRate;
        private bool _isActive = false;
        private double _currentSpeed = 0;

        public MotorController(string port, int baudRate)
        {
            _port = port;
            _baudRate = baudRate;
        }

        public void ActivateController()
        {
            _isActive = true;
        }

        public void DeactivateController()
        {
            _isActive = false;
            _currentSpeed = 0;
        }

        public void SetMotorSpeed(double speed)
        {
            _currentSpeed = Math.Max(0, Math.Min(100, speed));
        }

        public void PrintStatus()
        {
            Console.WriteLine("│ Motor Controller Status:             │");
            Console.WriteLine($"│ Port: {_port,-23}│");
            Console.WriteLine($"│ Active: {_isActive,-20}│");
            Console.WriteLine($"│ Speed: {_currentSpeed,3:F1}%                   │");
        }
    }
}