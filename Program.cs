using System;

namespace battlebots
{
    /*--------------------------------------------
    PARAMETERS:
        args (string[]): Command-line arguments passed to the application
    FUNCTION:
        Entry point of the battlebot simulation application.
        Creates and initializes a BattleBot, sets up remote control,
        and runs the main game loop reading keyboard input to control
        the bot's movement and weapon system.
        
        KEYBOARD CONTROLS:
        - W: Move Forward
        - A: Turn Left
        - D: Turn Right
        - Spacebar: Attack with weapon
        - Q: Quit the simulation
    OUTPUTS:
        Main(): void - Runs the complete battlebot simulation
    ---------------------------------------------*/
    class Program
    {
        static void Main(string[] args)
        {
            // Display welcome message
            Console.WriteLine("========================================");
            Console.WriteLine("   BATTLE BOT SIMULATION SYSTEM");
            Console.WriteLine("========================================");
            Console.WriteLine();

            // Create the battlebot instance
            BattleBot battleBot_RP = new BattleBot();

            // Initialize all systems
            Console.WriteLine("[SYSTEM] Initializing BattleBot...");
            battleBot_RP.Initialize();
            Console.WriteLine("[SYSTEM] BattleBot initialized successfully!");
            Console.WriteLine();

            // Display controls
            Console.WriteLine("CONTROLS:");
            Console.WriteLine("  W  - Move Forward");
            Console.WriteLine("  A  - Turn Left");
            Console.WriteLine("  D  - Turn Right");
            Console.WriteLine("  SPACE - Attack!");
            Console.WriteLine("  Q  - Quit Simulation");
            Console.WriteLine();
            Console.WriteLine("----------------------------------------");

            // Create remote control for reading inputs
            RemoteControl remoteControl_RP = new RemoteControl();

            // Start the battle sequence
            battleBot_RP.StartBattle();

            // Main game loop
            bool running_RP = true;
            while (running_RP)
            {
                // Display current status
                battleBot_RP.DisplayStatus();
                Console.WriteLine();

                // Read keyboard input and get action command
                Console.Write("Enter command: ");
                string input_RP = Console.ReadLine()?.ToUpper();

                if (input_RP == "Q")
                {
                    // Quit the simulation
                    running_RP = false;
                    Console.WriteLine("\n[SYSTEM] Shutting down BattleBot...");
                    battleBot_RP.Shutdown();
                    Console.WriteLine("[SYSTEM] Goodbye!");
                }
                else if (input_RP == "W")
                {
                    // Move forward
                    battleBot_RP.MoveForward();
                }
                else if (input_RP == "A")
                {
                    // Turn left
                    battleBot_RP.TurnLeft();
                }
                else if (input_RP == "D")
                {
                    // Turn right
                    battleBot_RP.TurnRight();
                }
                else if (input_RP == " ")
                {
                    // Attack with weapon
                    battleBot_RP.PerformAttack();
                }
                else if (string.IsNullOrEmpty(input_RP))
                {
                    // Empty input, just continue
                    Console.WriteLine("No action taken.");
                }
                else
                {
                    // Invalid command
                    Console.WriteLine("[ERROR] Unknown command! Use W, A, D, SPACE, or Q");
                }

                Console.WriteLine("----------------------------------------");
            }
        }
    }
}
