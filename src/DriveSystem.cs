using System;

namespace battlebots
{
    public class DriveSystem
    {
        private string _currentDirection = "IDLE";
        private double _speed = 0.0;
        private double _heading = 0.0; // Degrees, 0 = north

        public string CurrentDirection => _currentDirection;
        public double Speed => _speed;
        public double Heading => _heading;

        public void MoveForward()
        {
            _currentDirection = "FORWARD";
            _speed = 50.0;
            Console.WriteLine("[DRIVE] Moving FORWARD at 50% speed");
        }

        public void TurnLeft()
        {
            _currentDirection = "TURNING LEFT";
            _heading = (_heading + 15) % 360;
            _speed = 25.0;
            Console.WriteLine($"[DRIVE] Turning LEFT, new heading: {_heading:F0}°");
        }

        public void TurnRight()
        {
            _currentDirection = "TURNING RIGHT";
            _heading = (_heading - 15 + 360) % 360;
            _speed = 25.0;
            Console.WriteLine($"[DRIVE] Turning RIGHT, new heading: {_heading:F0}°");
        }

        public void Stop()
        {
            _currentDirection = "IDLE";
            _speed = 0.0;
        }

        public void DisplayStatus()
        {
            Console.WriteLine("│ Drive System Status:                 │");
            Console.WriteLine($"│ Direction: {_currentDirection,-20}│");
            Console.WriteLine($"│ Speed: {_speed,3:F1}%                   │");
            Console.WriteLine($"│ Heading: {_heading,3:F0}°                    │");
        }
    }
}