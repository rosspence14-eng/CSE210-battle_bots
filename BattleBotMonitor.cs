using System;
using System.Threading;

namespace battlebots
{
    /// <summary>
    /// Real-time GUI monitor for BattleBot system status
    /// Displays all bot information in an updated dashboard format
    /// </summary>
    public class BattleBotMonitor
    {
        private BattleBot _bot;
        private bool _isRunning;
        private Thread _monitorThread;
        private int _updateIntervalMs = 500; // Update every 500ms

        public BattleBotMonitor(BattleBot bot)
        {
            _bot = bot;
            _isRunning = false;
        }

        /// <summary>
        /// Start the real-time monitoring in a separate thread
        /// </summary>
        public void StartMonitoring()
        {
            _isRunning = true;
            _monitorThread = new Thread(RunMonitorLoop);
            _monitorThread.IsBackground = true;
            _monitorThread.Start();
            Console.WriteLine("[MONITOR] Real-time monitoring started");
        }

        /// <summary>
        /// Stop the monitoring
        /// </summary>
        public void StopMonitoring()
        {
            _isRunning = false;
            if (_monitorThread != null && _monitorThread.IsAlive)
            {
                _monitorThread.Join();
            }
            Console.WriteLine("[MONITOR] Monitoring stopped");
        }

        /// <summary>
        /// Main monitoring loop that runs in background thread
        /// </summary>
        private void RunMonitorLoop()
        {
            while (_isRunning)
            {
                try
                {
                    DisplayDashboard();
                    Thread.Sleep(_updateIntervalMs);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[MONITOR] Error: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Display the full dashboard with all system information
        /// </summary>
        public void DisplayDashboard()
        {
            Console.Clear();
            
            // Get current timestamp
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            
            // Header
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    BATTLE BOT REAL-TIME MONITOR                        ║");
            Console.WriteLine($"║                    [{timestamp}]                              ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════════════════════════╣");
            Console.ResetColor();

            // Bot Overview Section
            DisplayBotOverview();
            
            // Battery Section
            DisplayBatteryStatus();
            
            // Drive System Section
            DisplayDriveSystemStatus();
            
            // Weapon System Section
            DisplayWeaponStatus();
            
            // Motor Controller Section
            DisplayMotorControllerStatus();
            
            // Footer
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════╝");
            Console.WriteLine("║ Press ESC to exit monitor                                               ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
        }

        /// <summary>
        /// Display bot overview information
        /// </summary>
        private void DisplayBotOverview()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("┌─ BOT OVERVIEW ─────────────────────────────────────────────────────────┐");
            Console.ResetColor();
            
            string status = _bot.IsOperational ? "ONLINE ✓" : "OFFLINE ✗";
            ConsoleColor statusColor = _bot.IsOperational ? ConsoleColor.Green : ConsoleColor.Red;
            
            Console.WriteLine($"│ Bot Name:      {_bot.BotName,-49}│");
            Console.Write("│ Status:        ");
            Console.ForegroundColor = statusColor;
            Console.Write(status);
            Console.ResetColor();
            Console.WriteLine(new string(' ', 50 - status.Length) + "│");
            Console.WriteLine("└────────────────────────────────────────────────────────────────────────┘");
        }

        /// <summary>
        /// Display battery information with visual charge bar
        /// </summary>
        private void DisplayBatteryStatus()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("┌─ BATTERY SYSTEM ──────────────────────────────────────────────────────┐");
            Console.ResetColor();
            
            if (_bot.Battery != null)
            {
                double voltage = _bot.Battery.Voltage;
                double charge = _bot.Battery.ChargeLevel;
                
                // Create visual battery bar
                string batteryBar = CreateBatteryBar(charge);
                ConsoleColor barColor = GetBatteryColor(charge);
                
                Console.WriteLine($"│ Voltage:         {voltage,4:F2}V{new string(' ', 47)}│");
                Console.WriteLine($"│ Charge Level:    {charge,4:F1}%{new string(' ', 45)}│");
                
                Console.Write("│ Battery:       [");
                Console.ForegroundColor = barColor;
                Console.Write(batteryBar);
                Console.ResetColor();
                Console.WriteLine("]");
                
                string chargingStatus = _bot.Battery.IsCharging() ? "Charging ⚡" : "Discharging";
                Console.WriteLine($"│ Status:          {chargingStatus,-51}│");
            }
            else
            {
                Console.WriteLine("│ Battery system not initialized{-48}│");
            }
            
            Console.WriteLine("└────────────────────────────────────────────────────────────────────────┘");
        }

        /// <summary>
        /// Display drive system information
        /// </summary>
        private void DisplayDriveSystemStatus()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("┌─ DRIVE SYSTEM ────────────────────────────────────────────────────────┐");
            Console.ResetColor();
            
            if (_bot.DriveSystem != null)
            {
                string direction = _bot.DriveSystem.CurrentDirection;
                double speed = _bot.DriveSystem.Speed;
                double heading = _bot.DriveSystem.Heading;
                
                // Visual direction indicator
                string directionArrow = GetDirectionArrow(direction);
                ConsoleColor arrowColor = GetDirectionColor(direction);
                
                Console.WriteLine($"│ Direction:       {direction,-50}│");
                
                Console.Write("│ Arrow:         ");
                Console.ForegroundColor = arrowColor;
                Console.Write(directionArrow);
                Console.ResetColor();
                Console.WriteLine(new string(' ', 51 - directionArrow.Length) + "│");
                
                Console.WriteLine($"│ Speed:           {speed,4:F1}%{new string(' ', 45)}│");
                Console.WriteLine($"│ Heading:         {heading,4:F0}°{new string(' ', 45)}│");
                
                // Simple compass visualization
                DisplayCompass(heading);
            }
            else
            {
                Console.WriteLine("│ Drive system not initialized{-49}│");
            }
            
            Console.WriteLine("└────────────────────────────────────────────────────────────────────────┘");
        }

        /// <summary>
        /// Display weapon system information
        /// </summary>
        private void DisplayWeaponStatus()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("┌─ WEAPON SYSTEM ───────────────────────────────────────────────────────┐");
            Console.ResetColor();
            
            if (_bot.Weapon != null)
            {
                string weaponType = _bot.Weapon.GetType().Name;
                bool isActive = _bot.Weapon.IsActivated();
                int damage = _bot.Weapon.GetDamageOutput();
                bool charging = _bot.Weapon.IsCharging();
                bool onCooldown = !_bot.Weapon.CanAttack() && isActive;
                int attackCount = _bot.Weapon.GetAttackCount();
                
                ConsoleColor weaponColor = isActive ? ConsoleColor.Green : ConsoleColor.Red;
                
                Console.WriteLine($"│ Weapon Type:     {weaponType,-50}│");
                
                Console.Write("│ Active:          ");
                Console.ForegroundColor = weaponColor;
                Console.Write(isActive ? "YES ✓" : "NO ✗");
                Console.ResetColor();
                Console.WriteLine(new string(' ', 51 - (isActive ? 5 : 4)) + "│");
                
                Console.WriteLine($"│ Damage Output:   {damage,4}%{new string(' ', 47)}│");
                
                string chargingText = charging ? "Charging... ⚡" : "Ready";
                ConsoleColor chargeColor = charging ? ConsoleColor.Yellow : ConsoleColor.Green;
                Console.Write("│ Charge Status:   ");
                Console.ForegroundColor = chargeColor;
                Console.Write(chargingText);
                Console.ResetColor();
                Console.WriteLine(new string(' ', 51 - chargingText.Length) + "│");
                
                string cooldownText = onCooldown ? "ON COOLDOWN ⏱" : "READY TO FIRE ✓";
                ConsoleColor cooldownColor = onCooldown ? ConsoleColor.Red : ConsoleColor.Green;
                Console.Write("│ Fire Status:     ");
                Console.ForegroundColor = cooldownColor;
                Console.Write(cooldownText);
                Console.ResetColor();
                Console.WriteLine(new string(' ', 51 - cooldownText.Length) + "│");
                
                Console.WriteLine($"│ Attacks Fired:   {attackCount,-50}│");
                
                // Display weapon-specific info if available
                DisplayWeaponSpecificInfo();
            }
            else
            {
                Console.WriteLine("│ Weapon system not initialized{-49}│");
            }
            
            Console.WriteLine("└────────────────────────────────────────────────────────────────────────┘");
        }

        /// <summary>
        /// Display motor controller information
        /// </summary>
        private void DisplayMotorControllerStatus()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("┌─ MOTOR CONTROLLER ────────────────────────────────────────────────────┐");
            Console.ResetColor();
            
            // Note: MotorController fields are private, so we can only show limited info
            // This demonstrates what would be accessible with proper getters
            Console.WriteLine("│ Controller:      Active (Check MotorController for details){new string(' ', 21)}│");
            Console.WriteLine("│ Port:            COM3{new string(' ', 54)}│");
            Console.WriteLine("│ Baud Rate:       9600{new string(' ', 51)}│");
            
            Console.WriteLine("└────────────────────────────────────────────────────────────────────────┘");
        }

        /// <summary>
        /// Display weapon-specific information based on weapon type
        /// </summary>
        private void DisplayWeaponSpecificInfo()
        {
            if (_bot.Weapon is FlipperWeapon flipper)
            {
                Console.WriteLine();
                Console.WriteLine("│ ┌─ FLIPPER WEAPON DETAILS ──────────────────────────────────────────┐");
                // Note: FlipperWeapon specific fields are private
                // Would need public getters to access them
                Console.WriteLine("│ └──────────────────────────────────────────────────────────────────┘");
            }
            else if (_bot.Weapon is SpinnerWeapon spinner)
            {
                Console.WriteLine();
                Console.WriteLine("│ ┌─ SPINNER WEAPON DETAILS ────────────────────────────────────────┐");
                // Note: SpinnerWeapon specific fields are private
                // Would need public getters to access them
                Console.WriteLine("│ └────────────────────────────────────────────────────────────────┘");
            }
        }

        /// <summary>
        /// Create a visual battery charge bar
        /// </summary>
        private string CreateBatteryBar(double chargeLevel)
        {
            int barLength = 30;
            int filledBlocks = (int)(chargeLevel / 100.0 * barLength);
            return new string('█', filledBlocks) + new string('░', barLength - filledBlocks);
        }

        /// <summary>
        /// Get appropriate color for battery level
        /// </summary>
        private ConsoleColor GetBatteryColor(double chargeLevel)
        {
            if (chargeLevel > 60) return ConsoleColor.Green;
            if (chargeLevel > 30) return ConsoleColor.Yellow;
            return ConsoleColor.Red;
        }

        /// <summary>
        /// Get direction arrow based on current direction
        /// </summary>
        private string GetDirectionArrow(string direction)
        {
            return direction.ToUpper() switch
            {
                "FORWARD" => "▲",
                "TURNING LEFT" => "◄",
                "TURNING RIGHT" => "►",
                "IDLE" or "STOPPED" => "■",
                _ => "?"
            };
        }

        /// <summary>
        /// Get color for direction indicator
        /// </summary>
        private ConsoleColor GetDirectionColor(string direction)
        {
            return direction.ToUpper() switch
            {
                "FORWARD" => ConsoleColor.Green,
                "TURNING LEFT" => ConsoleColor.Cyan,
                "TURNING RIGHT" => ConsoleColor.Magenta,
                _ => ConsoleColor.Gray
            };
        }

        /// <summary>
        /// Display a simple compass visualization
        /// </summary>
        private void DisplayCompass(double heading)
        {
            Console.WriteLine("│ Compass:                                                        │");
            Console.WriteLine("│              N                                                 │");
            Console.WriteLine("│             ╱ ╲                                               │");
            
            int normalizedHeading = ((int)heading % 360 + 360) % 360;
            string arrowPos = GetCompassArrow(normalizedHeading);
            
            Console.WriteLine($"│            W {arrowPos} E                                       │");
            Console.WriteLine("│             ╲ ╱                                               │");
            Console.WriteLine("│              S                                                 │");
        }

        /// <summary>
        /// Position arrow on compass based on heading
        /// </summary>
        private string GetCompassArrow(int heading)
        {
            if (heading >= 337 || heading < 23) return "   ►   "; // North
            if (heading >= 23 && heading < 68) return "    ◄  "; // Northeast (showing from E)
            if (heading >= 68 && heading < 113) return "  ◄    "; // East
            if (heading >= 113 && heading < 158) return "      ◄"; // Southeast
            if (heading >= 158 && heading < 203) return "       ▲"; // South
            if (heading >= 203 && heading < 248) return "◄     "; // Southwest
            if (heading >= 248 && heading < 293) return " ◄    "; // West
            if (heading >= 293 && heading < 337) return "  ◄   "; // Northwest
            return "   ►   "; // Default to North
        }

        /// <summary>
        /// Display a single snapshot of the dashboard (for non-threaded use)
        /// </summary>
        public void DisplaySingleUpdate()
        {
            DisplayDashboard();
        }
    }
}
