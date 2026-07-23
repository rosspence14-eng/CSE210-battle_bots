using System;
using System.Threading;

namespace battlebots
{
    public class BattleBot
    {
        private DriveSystem _driveSystem_RP;
        private Battery _battery_RP;
        private Weapon _weapon_RP;
        private MotorController _motorController_RP;
        private RemoteControl _remoteControl_RP;
        private string _botName_RP;
        private bool _isOperational_RP;

        // Public getters for GUI monitoring
        public DriveSystem DriveSystem => _driveSystem_RP;
        public Battery Battery => _battery_RP;
        public Weapon Weapon => _weapon_RP;
        public MotorController MotorCtrl => _motorController_RP;
        public RemoteControl RemoteCtrl => _remoteControl_RP;
        public string BotName => _botName_RP;
        public bool IsOperational => _isOperational_RP;

        public BattleBot()
        {
            _botName_RP = "BATTLE-BOT-001";
            _isOperational_RP = false;
            Console.WriteLine("[BOT] BattleBot instance created");
        }

        public void Initialize()
        {
            Console.WriteLine("\n[BOT] === INITIALIZING ALL SYSTEMS ===");

            _driveSystem_RP = new DriveSystem();
            Console.WriteLine("[BOT] Drive System initialized");

            _battery_RP = new Battery();
            _battery_RP.Voltage = 7.4;
            _battery_RP.ChargeLevel = 100.0;
            Console.WriteLine("[BOT] Battery System initialized");

            _weapon_RP = new FlipperWeapon();
            Console.WriteLine("[BOT] Weapon System initialized");

            _motorController_RP = new MotorController("COM3", 9600);
            Console.WriteLine("[BOT] Motor Controller initialized");

            _remoteControl_RP = new RemoteControl();
            Console.WriteLine("[BOT] Remote Control initialized");

            Console.WriteLine("[BOT] === ALL SYSTEMS INITIALIZED ===\n");
        }

        public void StartBattle()
        {
            Console.WriteLine("\n[BOT] === STARTING BATTLE SEQUENCE ===");

            _motorController_RP.ActivateController();

            if (_weapon_RP != null)
            {
                _weapon_RP.Activate();
            }

            _isOperational_RP = true;

            Console.WriteLine("[BOT] Battle systems ONLINE");
            Console.WriteLine($"[BOT] Bot Name: {_botName_RP}");
            Console.WriteLine($"[BOT] Battery Voltage: {_battery_RP.Voltage}V");
            Console.WriteLine($"[BOT] Battery Charge: {_battery_RP.ChargeLevel}%");
            Console.WriteLine("[BOT] === READY FOR COMBAT ===\n");

            // Small delay so user can see the startup message before Clear() hides it
            System.Threading.Thread.Sleep(1500);
        }

        public void DisplayStatus()
        {
            // Calculate padding to keep lines exactly 38 chars (inside the box)
            string PadLine(string content)
            {
                if (content.Length >= 38) return content.Substring(0, 38);
                return content.PadRight(38);
            }

            Console.WriteLine("┌─────────────────────────────────────┐");
            Console.WriteLine("│      BATTLE BOT STATUS REPORT       │");
            Console.WriteLine("├─────────────────────────────────────┤");
            Console.WriteLine($"│ {PadLine($"Bot Name: {_botName_RP}")}│");
            Console.WriteLine($"│ {PadLine($"Operational: {_isOperational_RP}")}│");
            Console.WriteLine($"│ {PadLine($"Battery Voltage: {_battery_RP.Voltage:F1}V")}│");
            Console.WriteLine($"│ {PadLine($"Battery Charge: {_battery_RP.ChargeLevel:F1}%")}│");
            Console.WriteLine($"│ {PadLine($"Weapon Active: {_weapon_RP.IsActivated()}")}│");
            Console.WriteLine($"│ {PadLine($"Drive Dir: {_driveSystem_RP.CurrentDirection}")}│");
            Console.WriteLine("└─────────────────────────────────────┘");
        }

        public void StopBattle()
        {
            Console.WriteLine("\n[BOT] === STOPPING BATTLE SEQUENCE ===");

            if (_weapon_RP != null)
            {
                _weapon_RP.Deactivate();
            }

            _motorController_RP.DeactivateController();
            _isOperational_RP = false;

            Console.WriteLine("[BOT] Battle systems OFFLINE");
            Console.WriteLine("[BOT] === BATTLE ENDED ===\n");
        }

        public void Shutdown()
        {
            Console.WriteLine("\n[BOT] === SHUTTING DOWN ALL SYSTEMS ===");

            StopBattle();

            _battery_RP.ChargeLevel = Math.Max(0, _battery_RP.ChargeLevel - 0.5); // Minimal drain from shutdown process
            Console.WriteLine("[BOT] Power conservation mode engaged");
            Console.WriteLine($"[BOT] Final Battery Charge: {_battery_RP.ChargeLevel:F1}%");
            Console.WriteLine("[BOT] === SYSTEM SHUTDOWN COMPLETE ===\n");
        }

        public void MoveForward()
        {
            if (!_isOperational_RP)
            {
                Console.WriteLine("[BOT] ERROR: Cannot move - systems not operational!");
                return;
            }

            _driveSystem_RP.MoveForward();
        }

        public void TurnLeft()
        {
            if (!_isOperational_RP)
            {
                Console.WriteLine("[BOT] ERROR: Cannot turn - systems not operational!");
                return;
            }

            _driveSystem_RP.TurnLeft();
        }

        public void TurnRight()
        {
            if (!_isOperational_RP)
            {
                Console.WriteLine("[BOT] ERROR: Cannot turn - systems not operational!");
                return;
            }

            _driveSystem_RP.TurnRight();
        }

        public void PerformAttack()
        {
            if (!_isOperational_RP)
            {
                Console.WriteLine("[BOT] ERROR: Cannot attack - systems not operational!");
                return;
            }

            if (_weapon_RP != null && _weapon_RP.CanAttack())
            {
                _weapon_RP.Attack();
            }
        }
    }
}

