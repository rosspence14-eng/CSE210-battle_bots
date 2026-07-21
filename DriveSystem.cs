using System;

namespace battlebots
{
    /*--------------------------------------------
    PARAMETERS:
        _leftMotor_RP (Motor): The motor controlling the left-side wheels/tracks
        _rightMotor_RP (Motor): The motor controlling the right-side wheels/tracks
        _currentDirection_RP (string): Tracks the current movement direction
        _isMoving_RP (bool): Indicates if the bot is currently moving
    FUNCTION:
        Manages the movement and navigation of the battlebot.
        Controls differential drive using two independent motors.
        
        Constructor(): Initializes left and right motors with unique IDs
        MoveForward(): Drives the bot forward by activating both motors equally
        TurnLeft(): Rotates the bot to the left by reducing/sparing right motor speed
        TurnRight(): Rotates the bot to the right by reducing/stopping left motor speed
        Stop(): Halts all movement
    OUTPUTS:
        MoveForward(): void - no return value, prints action to console
        TurnLeft(): void - no return value, prints action to console
        TurnRight(): void - no return value, prints action to console
        Stop(): void - no return value, prints action to console
    ---------------------------------------------*/
    public class DriveSystem
    {
        // The motor controlling the left-side wheels/tracks
        private Motor _leftMotor_RP;

        // The motor controlling the right-side wheels/tracks
        private Motor _rightMotor_RP;

        // Tracks the current movement direction
        private string _currentDirection_RP;

        // Indicates if the bot is currently moving
        private bool _isMoving_RP;

        /// <summary>
        /// Constructor to initialize the drive system with left and right motors
        /// </summary>
        public DriveSystem()
        {
            // Initialize left motor
            _leftMotor_RP = new Motor();
            _leftMotor_RP.Id = "LEFT-MOTOR-01";

            // Initialize right motor
            _rightMotor_RP = new Motor();
            _rightMotor_RP.Id = "RIGHT-MOTOR-01";

            // Set initial state
            _currentDirection_RP = "STOPPED";
            _isMoving_RP = false;

            Console.WriteLine("[DRIVE] Drive system initialized");
            Console.WriteLine($"[DRIVE] Left Motor ID: {_leftMotor_RP.Id}");
            Console.WriteLine($"[DRIVE] Right Motor ID: {_rightMotor_RP.Id}");
        }

        /// <summary>
        /// Drives the bot forward by activating both motors at equal speed
        /// </summary>
        public void MoveForward()
        {
            Console.WriteLine("\n[DRIVE] === MOVING FORWARD ===");

            // Activate both motors if not already active
            _leftMotor_RP.Activate();
            _rightMotor_RP.Activate();

            // Set both motors to same positive speed for forward movement
            int forwardSpeed_RP = 75; // 75% power
            _leftMotor_RP.Speed = forwardSpeed_RP;
            _rightMotor_RP.Speed = forwardSpeed_RP;

            _currentDirection_RP = "FORWARD";
            _isMoving_RP = true;

            Console.WriteLine($"[DRIVE] Left Motor Speed: {_leftMotor_RP.Speed}%");
            Console.WriteLine($"[DRIVE] Right Motor Speed: {_rightMotor_RP.Speed}%");
            Console.WriteLine($"[DRIVE] Direction: {_currentDirection_RP}");
            Console.WriteLine("[DRIVE] Both motors driving forward at equal power");
        }

        /// <summary>
        /// Rotates the bot to the left by reducing right motor speed or reversing it
        /// Differential drive: left motor goes faster, right motor slower/reverse
        /// </summary>
        public void TurnLeft()
        {
            Console.WriteLine("\n[DRIVE] === TURNING LEFT ===");

            // Activate both motors if not already active
            _leftMotor_RP.Activate();
            _rightMotor_RP.Activate();

            // Left motor goes forward at high power, right motor at reduced speed
            int leftSpeed_RP = 75;   // Left motor forward
            int rightSpeed_RP = 25;  // Right motor slower (creates turn)

            _leftMotor_RP.Speed = leftSpeed_RP;
            _rightMotor_RP.Speed = rightSpeed_RP;

            _currentDirection_RP = "TURNING LEFT";
            _isMoving_RP = true;

            Console.WriteLine($"[DRIVE] Left Motor Speed: {_leftMotor_RP.Speed}% (FORWARD)");
            Console.WriteLine($"[DRIVE] Right Motor Speed: {_rightMotor_RP.Speed}% (REDUCED for turn)");
            Console.WriteLine($"[DRIVE] Direction: {_currentDirection_RP}");
            Console.WriteLine("[DRIVE] Differential drive - turning left");
        }

        /// <summary>
        /// Rotates the bot to the right by reducing left motor speed or reversing it
        /// Differential drive: right motor goes faster, left motor slower/reverse
        /// </summary>
        public void TurnRight()
        {
            Console.WriteLine("\n[DRIVE] === TURNING RIGHT ===");

            // Activate both motors if not already active
            _leftMotor_RP.Activate();
            _rightMotor_RP.Activate();

            // Right motor goes forward at high power, left motor at reduced speed
            int leftSpeed_RP = 25;   // Left motor slower (creates turn)
            int rightSpeed_RP = 75;  // Right motor forward

            _leftMotor_RP.Speed = leftSpeed_RP;
            _rightMotor_RP.Speed = rightSpeed_RP;

            _currentDirection_RP = "TURNING RIGHT";
            _isMoving_RP = true;

            Console.WriteLine($"[DRIVE] Left Motor Speed: {_leftMotor_RP.Speed}% (REDUCED for turn)");
            Console.WriteLine($"[DRIVE] Right Motor Speed: {_rightMotor_RP.Speed}% (FORWARD)");
            Console.WriteLine($"[DRIVE] Direction: {_currentDirection_RP}");
            Console.WriteLine("[DRIVE] Differential drive - turning right");
        }

        /// <summary>
        /// Stops all movement by deactivating both motors
        /// </summary>
        public void Stop()
        {
            Console.WriteLine("\n[DRIVE] === STOPPING ===");

            // Stop both motors
            _leftMotor_RP.Speed = 0;
            _rightMotor_RP.Speed = 0;
            _leftMotor_RP.Deactivate();
            _rightMotor_RP.Deactivate();

            _currentDirection_RP = "STOPPED";
            _isMoving_RP = false;

            Console.WriteLine($"[DRIVE] Left Motor Speed: {_leftMotor_RP.Speed}%");
            Console.WriteLine($"[DRIVE] Right Motor Speed: {_rightMotor_RP.Speed}%");
            Console.WriteLine($"[DRIVE] Direction: {_currentDirection_RP}");
            Console.WriteLine("[DRIVE] All motors stopped");
        }

        /// <summary>
        /// Gets the current movement direction
        /// </summary>
        /// <returns>Current direction string</returns>
        public string GetCurrentDirection()
        {
            return _currentDirection_RP;
        }

        /// <summary>
        /// Checks if the bot is currently moving
        /// </summary>
        /// <returns>True if moving, false if stopped</returns>
        public bool IsMoving()
        {
            return _isMoving_RP;
        }

        /// <summary>
        /// Gets the current speed of the left motor
        /// </summary>
        /// <returns>Left motor speed percentage</returns>
        public int GetLeftMotorSpeed()
        {
            return _leftMotor_RP.Speed;
        }

        /// <summary>
        /// Gets the current speed of the right motor
        /// </summary>
        /// <returns>Right motor speed percentage</returns>
        public int GetRightMotorSpeed()
        {
            return _rightMotor_RP.Speed;
        }

        /// <summary>
        /// Displays detailed drive system status
        /// </summary>
        public void DisplayStatus()
        {
            Console.WriteLine("[DRIVE] === Drive System Status ===");
            Console.WriteLine($"[DRIVE] Left Motor ID: {_leftMotor_RP.Id}");
            Console.WriteLine($"[DRIVE] Right Motor ID: {_rightMotor_RP.Id}");
            Console.WriteLine($"[DRIVE] Left Speed: {_leftMotor_RP.Speed}%");
            Console.WriteLine($"[DRIVE] Right Speed: {_rightMotor_RP.Speed}%");
            Console.WriteLine($"[DRIVE] Direction: {_currentDirection_RP}");
            Console.WriteLine($"[DRIVE] Moving: {_isMoving_RP}");
            Console.WriteLine("[DRIVE] ============================");
        }
    }
}