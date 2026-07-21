using System;

namespace battlebots
{
    /*--------------------------------------------
    PARAMETERS:
        _driveSystem_RP (DriveSystem): Controls the bot's movement system
        _battery_RP (Battery): Provides power to the battlebot
        _weapon_RP (Weapon): The offensive weapon system mounted on the bot
        _motorController_RP (MotorController): Interfaces with motors for speed control
        _remoteControl_RP (RemoteControl): Handles keyboard input from operator
    FUNCTION:
        Main battlebot class that integrates all subsystems (drive, battery, weapon).
        Acts as the central coordinator for all bot operations.
        
        Initialize(): Sets up and prepares all components for operation
        StartBattle(): Begins the battle sequence and activates combat systems
        DisplayStatus(): Shows current status of all systems
        Shutdown(): Safely shuts down all systems
        MoveForward(): Commands the drive system to move forward
        TurnLeft(): Commands the drive system to turn left
        TurnRight(): Commands the drive system to turn right
        PerformAttack(): Executes a weapon attack if ready
    OUTPUTS:
        Initialize(): void - no return value
        StartBattle(): void - no return value
        DisplayStatus(): void - prints status to console
        Shutdown(): void - no return value
        MoveForward(): void - no return value
        TurnLeft(): void - no return value
        TurnRight(): void - no return value
        PerformAttack(): void - no return value
    ---------------------------------------------*/
    public class BattleBot
    {
        // Drive system for movement control
        private DriveSystem _driveSystem_RP;

        // Battery providing power to all systems
        private Battery _battery_RP;

        // Weapon system for combat
        private Weapon _weapon_RP;

        // Motor controller for ESC communication
        private MotorController _motorController_RP;

        // Remote control for operator input
        private RemoteControl _remoteControl_RP;

        // Bot identification
        private string _botName_RP;

        // Overall operational status
        private bool _isOperational_RP;

        /// <summary>
        /// Constructor to initialize the BattleBot with default components
        /// </summary>
        public BattleBot()
        {
            _botName_RP = "BATTLE-BOT-001";
            _isOperational_RP = false;

            Console.WriteLine("[BOT] BattleBot instance created");
        }

        /// <summary>
        /// Initializes all subsystems and prepares the bot for operation
        /// </summary>
        public void Initialize()
        {
            Console.WriteLine("\n[BOT] === INITIALIZING ALL SYSTEMS ===");

            // Initialize drive system with left and right motors
            _driveSystem_RP = new DriveSystem();
            Console.WriteLine("[BOT] Drive System initialized");

            // Initialize battery with starting values
            _battery_RP = new Battery();
            _battery_RP.Voltage = 7.4; // Typical 2S LiPo battery voltage
            _battery_RP.ChargeLevel = 100.0;
            Console.WriteLine("[BOT] Battery System initialized");

            // Initialize weapon system (using FlipperWeapon as default)
            _weapon_RP = new FlipperWeapon();
            Console.WriteLine("[BOT] Weapon System initialized");

            // Initialize motor controller for ESC communication
            _motorController_RP = new MotorController("COM3", 9600);
            Console.WriteLine("[BOT] Motor Controller initialized");

            // Initialize remote control for operator input
            _remoteControl_RP = new RemoteControl();
            Console.WriteLine("[BOT] Remote Control initialized");

            Console.WriteLine("[BOT] === ALL SYSTEMS INITIALIZED ===\n");
        }

        /// <summary>
        /// Starts the battle sequence - activates all systems for combat
        /// </summary>
        public void StartBattle()
        {
            Console.WriteLine("\n[BOT] === STARTING BATTLE SEQUENCE ===");

            // Activate the motor controller
            _motorController_RP.ActivateController();

            // Activate the weapon system
            if (_weapon_RP != null)
            {
                _weapon_RP.Activate();
            }

            // Set bot to operational status
            _isOperational_RP = true;

            Console.WriteLine("[BOT] Battle systems ONLINE");
            Console.WriteLine($"[BOT] Bot Name: {_botName_RP}");
            Console.WriteLine($"[BOT] Battery Voltage: {_battery_RP.Voltage}V");
            Console.WriteLine($"[BOT] Battery Charge: {_battery_RP.ChargeLevel}%");
            Console.WriteLine("[BOT] === READY FOR COMBAT ===\n");
        }

        /// <summary>
        /// Displays current status of all systems
        /// </summary>
        public void DisplayStatus()
        {
            Console.WriteLine("┌─────────────────────────────────────┐");
            Console.WriteLine("│      BATTLE BOT STATUS REPORT       │");
            Console.WriteLine("├─────────────────────────────────────┤");
            Console.WriteLine($"│ Bot Name: {_botName_RP,-21}│");
            Console.WriteLine($"│ Operational: {_isOperational_RP,-19}│");
            Console.WriteLine($"│ Battery Voltage: {_battery_RP.Voltage,4:F1}V          │");
            Console.WriteLine($"│ Battery Charge: {_battery_RP.ChargeLevel,3:F1}%          │");
            Console.WriteLine("├─────────────────────────────────────┤");

            // Display weapon info if available
            if (_weapon_RP != null)
            {
                Console.WriteLine("│ Weapon Status:                     │");
                _weapon_RP.DisplayWeaponInfo();
            }

            // Display motor controller status
            Console.WriteLine("├─────────────────────────────────────┤");
            _motorController_RP.PrintStatus();

            Console.WriteLine("└─────────────────────────────────────┘");
        }

        /// <summary>
        /// Safely shuts down all systems
        /// </summary>
        public void Shutdown()
        {
            Console.WriteLine("\n[BOT] === INITIATING SHUTDOWN SEQUENCE ===");

            // Deactivate weapon first for safety
            if (_weapon_RP != null)
            {
                _weapon_RP.Deactivate();
                Console.WriteLine("[BOT] Weapon system deactivated");
            }

            // Stop all motors
            _motorController_RP.SetMotorSpeed(0);
            Console.WriteLine("[BOT] All motors stopped");

            // Deactivate motor controller
            _motorController_RP.DeactivateController();
            Console.WriteLine("[BOT] Motor controller deactivated");

            // Disconnect remote control
            if (_remoteControl_RP != null)
            {
                _remoteControl_RP.Disconnect();
            }

            // Set bot to non-operational
            _isOperational_RP = false;

            Console.WriteLine("[BOT] === SHUTDOWN COMPLETE ===");
        }

        /// <summary>
        /// Commands the drive system to move forward
        /// </summary>
        public void MoveForward()
        {
            if (!_isOperational_RP)
            {
                Console.WriteLine("[BOT] ERROR: Bot not operational! Initialize first.");
                return;
            }

            Console.WriteLine("\n[BOT] Command: MOVE FORWARD");

            // Check battery level before moving
            if (_battery_RP.ChargeLevel < 10)
            {
                Console.WriteLine("[BOT] WARNING: Low battery! Movement disabled.");
                return;
            }

            // Send speed command to motor controller (positive = forward)
            _motorController_RP.SetMotorSpeed(50); // 50% power forward

            // Command the drive system to move forward
            _driveSystem_RP.MoveForward();

            // Simulate battery drain from movement
            DrainBattery(0.5);

            Console.WriteLine("[BOT] Forward movement engaged at 50% power");
        }

        /// <summary>
        /// Commands the drive system to turn left
        /// </summary>
        public void TurnLeft()
        {
            if (!_isOperational_RP)
            {
                Console.WriteLine("[BOT] ERROR: Bot not operational! Initialize first.");
                return;
            }

            Console.WriteLine("\n[BOT] Command: TURN LEFT");

            // Check battery level before turning
            if (_battery_RP.ChargeLevel < 10)
            {
                Console.WriteLine("[BOT] WARNING: Low battery! Movement disabled.");
                return;
            }

            // Send speed command to motor controller (slower for turning)
            _motorController_RP.SetMotorSpeed(25); // 25% power for turn

            // Command the drive system to turn left
            _driveSystem_RP.TurnLeft();

            // Simulate battery drain from turning
            DrainBattery(0.3);

            Console.WriteLine("[BOT] Left turn engaged at 25% power");
        }

        /// <summary>
        /// Commands the drive system to turn right
        /// </summary>
        public void TurnRight()
        {
            if (!_isOperational_RP)
            {
                Console.WriteLine("[BOT] ERROR: Bot not operational! Initialize first.");
                return;
            }

            Console.WriteLine("\n[BOT] Command: TURN RIGHT");

            // Check battery level before turning
            if (_battery_RP.ChargeLevel < 10)
            {
                Console.WriteLine("[BOT] WARNING: Low battery! Movement disabled.");
                return;
            }

            // Send speed command to motor controller (slower for turning)
            _motorController_RP.SetMotorSpeed(25); // 25% power for turn

            // Command the drive system to turn right
            _driveSystem_RP.TurnRight();

            // Simulate battery drain from turning
            DrainBattery(0.3);

            Console.WriteLine("[BOT] Right turn engaged at 25% power");
        }

        /// <summary>
        /// Performs a weapon attack if the weapon is ready
        /// </summary>
        public void PerformAttack()
        {
            if (!_isOperational_RP)
            {
                Console.WriteLine("[BOT] ERROR: Bot not operational! Initialize first.");
                return;
            }

            Console.WriteLine("\n[BOT] Command: ATTACK");

            // Check battery level before attacking
            if (_battery_RP.ChargeLevel < 10)
            {
                Console.WriteLine("[BOT] WARNING: Low battery! Weapons disabled.");
                return;
            }

            // Check if weapon is ready and perform attack
            if (_weapon_RP != null)
            {
                if (_weapon_RP.CanAttack())
                {
                    _weapon_RP.Attack();

                    // Simulate battery drain from attack
                    DrainBattery(1.5);
                }
                else
                {
                    Console.WriteLine("[BOT] Attack failed - weapon not ready!");
                }
            }
            else
            {
                Console.WriteLine("[BOT] ERROR: No weapon system available!");
            }
        }

        /// <summary>
        /// Simulates battery drain from actions
        /// </summary>
        /// <param name="amount_RP">Amount to drain the battery</param>
        private void DrainBattery(double amount_RP)
        {
            _battery_RP.ChargeLevel -= amount_RP;

            // Ensure charge level doesn't go below 0
            if (_battery_RP.ChargeLevel < 0)
            {
                _battery_RP.ChargeLevel = 0;
                Console.WriteLine("[BOT] CRITICAL: Battery depleted!");
            }

            // Update voltage based on charge level (simplified model)
            // 100% charge = 8.4V (fully charged 2S LiPo)
            // 0% charge = 6.0V (discharged 2S LiPo)
            _battery_RP.Voltage = 6.0 + (_battery_RP.ChargeLevel / 100.0) * 2.4;

            Console.WriteLine($"[BOT] Battery drained: {_battery_RP.ChargeLevel:F1}% remaining, Voltage: {_battery_RP.Voltage:F1}V");
        }
    }
}