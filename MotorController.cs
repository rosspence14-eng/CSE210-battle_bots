using System;

namespace battlebots
{
    /*--------------------------------------------
    PARAMETERS:
        PortName (string): Communication port for motor controller (inherited from CommunicationBase)
        BaudRate (int): Data transmission rate for communication (inherited from CommunicationBase)
        _currentSpeed_RP (int): Tracks the current motor speed setting
        _isActive_RP (bool): Indicates if the motor controller is active and responding to commands
    FUNCTION:
        Controls motor operations through a communication interface.
        Inherits from CommunicationBase to send/receive motor control commands.
        Simulates sending PWM signals to Electronic Speed Controllers (ESCs).
        
        SetMotorSpeed(int): Adjusts the speed of connected motors
        ActivateController(): Powers on the motor controller
        DeactivateController(): Powers off the motor controller
        GetStatus(): Returns current controller status information
    OUTPUTS:
        SetMotorSpeed(): void - no return value, prints command to console
        ActivateController(): void - no return value
        DeactivateController(): void - no return value
        GetStatus(): string - returns status information
    ---------------------------------------------*/
    public class MotorController : CommunicationBase
    {
        // Track current motor speed (range: -100 to 100, where negative is reverse)
        private int _currentSpeed_RP;

        // Track if the controller is powered on and active
        private bool _isActive_RP;

        /// <summary>
        /// Constructor to initialize the motor controller with default settings
        /// </summary>
        public MotorController()
        {
            _currentSpeed_RP = 0;
            _isActive_RP = false;
            PortName = "COM3";
            BaudRate = 9600;

            Console.WriteLine("[MOTOR CONTROLLER] Motor Controller initialized");
            Console.WriteLine($"[MOTOR CONTROLLER] Port: {PortName}, Baud Rate: {BaudRate}");
            Console.WriteLine("[MOTOR CONTROLLER] Status: STANDBY");
        }

        /// <summary>
        /// Constructor to initialize with specific port and baud rate
        /// </summary>
        /// <param name="portName_RP">Communication port name</param>
        /// <param name="baudRate_RP">Data transmission speed</param>
        public MotorController(string portName_RP, int baudRate_RP)
        {
            _currentSpeed_RP = 0;
            _isActive_RP = false;
            PortName = portName_RP;
            BaudRate = baudRate_RP;

            Console.WriteLine("[MOTOR CONTROLLER] Motor Controller initialized");
            Console.WriteLine($"[MOTOR CONTROLLER] Port: {PortName}, Baud Rate: {BaudRate}");
            Console.WriteLine("[MOTOR CONTROLLER] Status: STANDBY");
        }

        /// <summary>
        /// Sets the speed of the connected motors
        /// Speed range: -100 (full reverse) to 100 (full forward), 0 = stop
        /// </summary>
        /// <param name="speed_RP">Desired motor speed (-100 to 100)</param>
        public void SetMotorSpeed(int speed_RP)
        {
            // Check if controller is active
            if (!_isActive_RP)
            {
                Console.WriteLine("[MOTOR CONTROLLER] ERROR: Controller not active! Cannot set speed.");
                return;
            }

            // Validate speed range
            if (speed_RP < -100 || speed_RP > 100)
            {
                Console.WriteLine($"[MOTOR CONTROLLER] WARNING: Speed {speed_RP} out of range! Clamping to valid range.");
                speed_RP = Math.Max(-100, Math.Min(100, speed_RP));
            }

            // Update current speed
            int previousSpeed_RP = _currentSpeed_RP;
            _currentSpeed_RP = speed_RP;

            // Create command message to send to ESC
            string command_RP = $"MOTOR_SPEED:{_currentSpeed_RP}";

            // Send the command (simulated)
            SendMessage(command_RP);

            // Display what happened
            Console.WriteLine($"[MOTOR CONTROLLER] Speed changed: {previousSpeed_RP}% -> {_currentSpeed_RP}%");

            if (_currentSpeed_RP == 0)
            {
                Console.WriteLine("[MOTOR CONTROLLER] Motors STOPPED");
            }
            else if (_currentSpeed_RP > 0)
            {
                Console.WriteLine($"[MOTOR CONTROLLER] Motors moving FORWARD at {_currentSpeed_RP}% power");
            }
            else
            {
                Console.WriteLine($"[MOTOR CONTROLLER] Motors moving REVERSE at {Math.Abs(_currentSpeed_RP)}% power");
            }
        }

        /// <summary>
        /// Powers on the motor controller and prepares it for commands
        /// </summary>
        public void ActivateController()
        {
            _isActive_RP = true;
            Console.WriteLine("[MOTOR CONTROLLER] Controller ACTIVATED");
            Console.WriteLine("[MOTOR CONTROLLER] Ready to accept speed commands");
            Console.WriteLine($"[MOTOR CONTROLLER] Current Speed: {_currentSpeed_RP}%");
        }

        /// <summary>
        /// Powers off the motor controller and stops all motors
        /// </summary>
        public void DeactivateController()
        {
            // First, stop all motors for safety
            _currentSpeed_RP = 0;
            Console.WriteLine("[MOTOR CONTROLLER] Emergency stop - all motors halted");

            _isActive_RP = false;
            Console.WriteLine("[MOTOR CONTROLLER] Controller DEACTIVATED");
            Console.WriteLine("[MOTOR CONTROLLER] No longer accepting commands");
        }

        /// <summary>
        /// Gets the current speed setting
        /// </summary>
        /// <returns>Current motor speed value (-100 to 100)</returns>
        public int GetCurrentSpeed()
        {
            return _currentSpeed_RP;
        }

        /// <summary>
        /// Checks if the controller is currently active
        /// </summary>
        /// <returns>True if active, false otherwise</returns>
        public bool IsControllerActive()
        {
            return _isActive_RP;
        }

        /// <summary>
        /// Gets a detailed status report of the motor controller
        /// </summary>
        /// <returns>String containing status information</returns>
        public string GetStatus()
        {
            string status_RP = "[MOTOR CONTROLLER] Status Report:\n";
            status_RP += $"  - Port: {PortName}\n";
            status_RP += $"  - Baud Rate: {BaudRate}\n";
            status_RP += $"  - Active: {_isActive_RP}\n";
            status_RP += $"  - Current Speed: {_currentSpeed_RP}%\n";
            status_RP += $"  - Direction: {( _currentSpeed_RP > 0 ? "FORWARD" : (_currentSpeed_RP < 0 ? "REVERSE" : "STOPPED"))}";

            return status_RP;
        }

        /// <summary>
        /// Prints the current status to the console
        /// </summary>
        public void PrintStatus()
        {
            Console.WriteLine(GetStatus());
        }

        /// <summary>
        /// Override SendMessage to add logging for motor commands
        /// </summary>
        /// <param name="message_RP">Message to send</param>
        public override void SendMessage(string message_RP)
        {
            base.SendMessage(message_RP);
            Console.WriteLine($"[MOTOR CONTROLLER] Sending command: {message_RP}");
        }

        /// <summary>
        /// Override ReceiveMessage to simulate ESC feedback
        /// </summary>
        /// <returns>Feedback message from the ESC</returns>
        public override string ReceiveMessage()
        {
            string baseMessage_RP = base.ReceiveMessage();
            string feedback_RP = $"ESC_Feedback:Speed={_currentSpeed_RP},Active={_isActive_RP}";

            Console.WriteLine($"[MOTOR CONTROLLER] Received feedback: {feedback_RP}");
            return feedback_RP;
        }

        /// <summary>
        /// Performs a calibration sequence for the ESCs (simulated)
        /// </summary>
        public void CalibrateMotors()
        {
            if (!_isActive_RP)
            {
                Console.WriteLine("[MOTOR CONTROLLER] ERROR: Cannot calibrate - controller not active!");
                return;
            }

            Console.WriteLine("[MOTOR CONTROLLER] Starting ESC calibration sequence...");
            Console.WriteLine("[MOTOR CONTROLLER] Setting maximum throttle...");
            _currentSpeed_RP = 100;
            System.Threading.Thread.Sleep(500); // Simulated delay

            Console.WriteLine("[MOTOR CONTROLLER] Setting minimum throttle...");
            _currentSpeed_RP = 0;
            System.Threading.Thread.Sleep(500);

            Console.WriteLine("[MOTOR CONTROLLER] Calibration complete!");
            Console.WriteLine($"[MOTOR CONTROLLER] Current Speed: {_currentSpeed_RP}%");
        }
    }
}