using System;

namespace battlebots
{
    public class DriveSystem
    {
        private readonly Motor _npLeftMotor;
        private readonly Motor _npRightMotor;
        private string _npCurrentDirection = "IDLE";
        private double _npSpeed = 0.0;
        private double _npHeading = 0.0; // Degrees, 0 = north

        public string CurrentDirection => _npCurrentDirection;
        public double Speed => _npSpeed;
        public double Heading => _npHeading;

        public DriveSystem()
        {
            _npLeftMotor = new Motor("LEFT-MOTOR");
            _npRightMotor = new Motor("RIGHT-MOTOR");
        }

        public DriveSystem(Motor leftMotor, Motor rightMotor)
        {
            _npLeftMotor = leftMotor ?? new Motor("LEFT-MOTOR");
            _npRightMotor = rightMotor ?? new Motor("RIGHT-MOTOR");
        }

        public void MoveForward()
        {
            _npCurrentDirection = "FORWARD";
            _npSpeed = 50.0;
            _npLeftMotor.SetSpeed(50);
            _npRightMotor.SetSpeed(50);
            Console.WriteLine("[DRIVE] Moving FORWARD at 50% speed");
        }

        public void MoveBackward()
        {
            _npCurrentDirection = "BACKWARD";
            _npSpeed = 50.0;
            _npLeftMotor.SetSpeed(-50);
            _npRightMotor.SetSpeed(-50);
            Console.WriteLine("[DRIVE] Moving BACKWARD at 50% speed");
        }

        public void TurnLeft()
        {
            _npCurrentDirection = "TURNING LEFT";
            _npHeading = (_npHeading + 15) % 360;
            _npSpeed = 25.0;
            _npLeftMotor.SetSpeed(25);
            _npRightMotor.SetSpeed(0);
            Console.WriteLine($"[DRIVE] Turning LEFT, new heading: {_npHeading:F0}°");
        }

        public void TurnRight()
        {
            _npCurrentDirection = "TURNING RIGHT";
            _npHeading = (_npHeading - 15 + 360) % 360;
            _npSpeed = 25.0;
            _npLeftMotor.SetSpeed(0);
            _npRightMotor.SetSpeed(25);
            Console.WriteLine($"[DRIVE] Turning RIGHT, new heading: {_npHeading:F0}°");
        }

        public void Stop()
        {
            _npCurrentDirection = "IDLE";
            _npSpeed = 0.0;
            _npLeftMotor.SetSpeed(0);
            _npRightMotor.SetSpeed(0);
        }

        public void DisplayStatus()
        {
            Console.WriteLine("│ Drive System Status:                 │");
            Console.WriteLine($"│ Direction: {_npCurrentDirection,-20}│");
            Console.WriteLine($"│ Speed: {_npSpeed,3:F1}%                   │");
            Console.WriteLine($"│ Heading: {_npHeading,3:F0}°                    │");
        }
    }
}