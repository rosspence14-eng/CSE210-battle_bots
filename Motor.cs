using System;

namespace battlebots
{
    /*--------------------------------------------
    PARAMETERS:
        Speed (int): The rotation speed of the motor (percentage, 0-100)
        Id (string): Unique identifier for this motor (inherited from ActuatorBase)
        _isRunning_RP (bool): Tracks if the motor is currently active and spinning
    FUNCTION:
        Represents a physical motor used in the battlebot's movement system.
        Inherits from ActuatorBase to provide standard actuator functionality.
        
        Constructor(): Initializes motor with default values
        Activate(): Powers on and starts the motor rotation
        Deactivate(): Powers off and stops the motor rotation safely
    OUTPUTS:
        Activate(): void - no return value, prints activation status
        Deactivate(): void - no return value, prints deactivation status
    ---------------------------------------------*/
    public class Motor : ActuatorBase
    {
        // The rotation speed of the motor as a percentage (0-100)
        public int _npSpeed { get; set; }

        // Tracks if the motor is currently active and spinning
        private bool _isRunning_RP;

        /// <summary>
        /// Constructor to initialize the motor with default values
        /// </summary>
        public Motor()
        {
            _npSpeed = 0;
            _isRunning_RP = false;
            _npId = "MOTOR-UNKNOWN";

            Console.WriteLine($"[MOTOR] Motor instance created (ID will be assigned)");
        }

        /// <summary>
        /// Constructor to initialize with a specific ID
        /// </summary>
        /// <param name="motorId_RP">Unique identifier for this motor</param>
        public Motor(string motorId_RP)
        {
            _npSpeed = 0;
            _isRunning_RP = false;
            _npId = motorId_RP;

            Console.WriteLine($"[MOTOR] Motor initialized: ID={_npId}");
        }

        /// <summary>
        /// Powers on and starts the motor rotation
        /// Activates the motor controller and prepares for speed commands
        /// </summary>
        public override void Activate()
        {
            base.Activate();
            _isRunning_RP = true;
            _npSpeed = 0; // Start at zero speed for safety

            Console.WriteLine($"[MOTOR] {_npId} ACTIVATED");
            Console.WriteLine($"[MOTOR] {_npId} Motor controller ONLINE");
            Console.WriteLine($"[MOTOR] {_npId} Ready to accept speed commands");
            Console.WriteLine($"[MOTOR] {_npId} Current Speed: {_npSpeed}%");
        }

        /// <summary>
        /// Powers off and stops the motor rotation safely
        /// Brings speed to zero before deactivating the controller
        /// </summary>
        public override void Deactivate()
        {
            // Safety: Ensure speed is zero before powering down
            _npSpeed = 0;

            _isRunning_RP = false;
            base.Deactivate();

            Console.WriteLine($"[MOTOR] {_npId} DEACTIVATED");
            Console.WriteLine($"[MOTOR] {_npId} Motor stopped (Speed: {_npSpeed}%)");
            Console.WriteLine($"[MOTOR] {_npId} Controller OFFLINE");
        }

        /// <summary>
        /// Sets the motor speed with validation
        /// </summary>
        /// <param name="speed_RP">Desired speed percentage (0-100)"></param>
        public void SetSpeed(int speed_RP)
        {
            // Validate speed range
            if (speed_RP < 0 || speed_RP > 100)
            {
                Console.WriteLine($"[MOTOR] {_npId} WARNING: Speed {speed_RP}% out of valid range (0-100)!");
                speed_RP = Math.Max(0, Math.Min(100, speed_RP));
            }

            int previousSpeed_RP = _npSpeed;
            _npSpeed = speed_RP;

            Console.WriteLine($"[MOTOR] {_npId} Speed changed: {previousSpeed_RP}% -> {_npSpeed}%");

            if (_npSpeed == 0)
            {
                Console.WriteLine($"[MOTOR] {_npId} Motor STOPPED");
            }
            else
            {
                Console.WriteLine($"[MOTOR] {_npId} Motor running at {_npSpeed}% power");
            }
        }

        /// <summary>
        /// Checks if the motor is currently active
        /// </summary>
        /// <returns>True if activated and running, false otherwise</returns>
        public bool IsRunning()
        {
            return _isRunning_RP;
        }

        /// <summary>
        /// Displays detailed motor status information
        /// </summary>
        public void DisplayStatus()
        {
            Console.WriteLine($"[MOTOR] === Motor Status: {_npId} ===");
            Console.WriteLine($"[MOTOR] Running: {_isRunning_RP}");
            Console.WriteLine($"[MOTOR] Speed: {_npSpeed}%");
            Console.WriteLine($"[MOTOR] ID: {_npId}");
            Console.WriteLine("[MOTOR] ===========================");
        }
    }
}