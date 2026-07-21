using System;

namespace battlebots
{
    /*--------------------------------------------
    PARAMETERS:
        _currentKey_RP (ConsoleKey): Stores the most recent key pressed by the user
        _inputString_RP (string): Stores the string input from the user
    FUNCTION:
        Handles remote control input operations for the battlebot.
        Uses keyboard inputs instead of a physical controller.
        
        ReadInputs(): Captures and processes user inputs from keyboard
        GetDirection(): Returns movement direction based on key press
        IsAttackPressed(): Checks if attack button was pressed
        ShouldQuit(): Checks if quit command was entered
    OUTPUTS:
        ReadInputs(): string - returns the action to perform
        GetDirection(): string - returns "FORWARD", "LEFT", "RIGHT", or "NONE"
        IsAttackPressed(): bool - returns true if attack should be performed
        ShouldQuit(): bool - returns true if simulation should end
    ---------------------------------------------*/
    public class RemoteControl
    {
        // Store the current input state
        private string _currentInput_RP;
        private bool _isConnected_RP;

        /// <summary>
        /// Constructor to initialize the remote control system
        /// </summary>
        public RemoteControl()
        {
            _isConnected_RP = true;
            Console.WriteLine("[REMOTE CONTROL] Keyboard controller connected successfully!");
        }

        /// <summary>
        /// Reads keyboard input and returns the action command
        /// This replaces reading from a physical RC transmitter
        /// </summary>
        /// <returns>String representing the action to perform</returns>
        public string ReadInputs()
        {
            // Wait for user input
            Console.Write("[REMOTE CONTROL] Awaiting command... ");
            _currentInput_RP = Console.ReadLine()?.ToUpper();

            // Validate and return the input
            if (string.IsNullOrEmpty(_currentInput_RP))
            {
                Console.WriteLine("[REMOTE CONTROL] No input detected. Standing by.");
                return "NONE";
            }

            Console.WriteLine($"[REMOTE CONTROL] Command received: {_currentInput_RP}");
            return _currentInput_RP;
        }

        /// <summary>
        /// Determines the movement direction based on keyboard input
        /// </summary>
        /// <param name="input_RP">The key/command entered by user</param>
        /// <returns>Direction string: FORWARD, LEFT, RIGHT, or NONE</returns>
        public string GetDirection(string input_RP)
        {
            switch (input_RP)
            {
                case "W":
                    Console.WriteLine("[REMOTE CONTROL] Forward command activated");
                    return "FORWARD";

                case "A":
                    Console.WriteLine("[REMOTE CONTROL] Left turn command activated");
                    return "LEFT";

                case "D":
                    Console.WriteLine("[REMOTE CONTROL] Right turn command activated");
                    return "RIGHT";

                default:
                    return "NONE";
            }
        }

        /// <summary>
        /// Checks if the attack button (SPACE) was pressed
        /// </summary>
        /// <param name="input_RP">The key/command entered by user</param>
        /// <returns>True if attack should be performed, false otherwise</returns>
        public bool IsAttackPressed(string input_RP)
        {
            // Check for space or "SPACE" as attack command
            if (input_RP == " " || input_RP == "SPACE")
            {
                Console.WriteLine("[REMOTE CONTROL] Attack button pressed!");
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if the quit command was entered
        /// </summary>
        /// <param name="input_RP">The key/command entered by user</param>
        /// <returns>True if simulation should quit, false otherwise</returns>
        public bool ShouldQuit(string input_RP)
        {
            if (input_RP == "Q" || input_RP == "QUIT")
            {
                Console.WriteLine("[REMOTE CONTROL] Quit command received");
                return true;
            }

            return false;
        }

        /// <summary>
        /// Displays the current status of the remote control connection
        /// </summary>
        public void DisplayConnectionStatus()
        {
            Console.WriteLine("[REMOTE CONTROL] Connection Status: " + (_isConnected_RP ? "CONNECTED" : "DISCONNECTED"));
            Console.WriteLine("[REMOTE CONTROL] Input Method: KEYBOARD");
        }

        /// <summary>
        /// Simulates disconnecting the remote control
        /// </summary>
        public void Disconnect()
        {
            _isConnected_RP = false;
            Console.WriteLine("[REMOTE CONTROL] Controller disconnected!");
        }

        /// <summary>
        /// Validates if a command is recognized by the system
        /// </summary>
        /// <param name="command_RP">The command to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValidCommand(string command_RP)
        {
            string[] validCommands_RP = { "W", "A", "D", "SPACE", " ", "Q", "QUIT" };

            foreach (string valid_RP in validCommands_RP)
            {
                if (command_RP == valid_RP)
                {
                    return true;
                }
            }

            Console.WriteLine($"[REMOTE CONTROL] Invalid command: {command_RP}");
            return false;
        }
    }
}