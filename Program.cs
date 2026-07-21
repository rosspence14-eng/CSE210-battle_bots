using System;
using System.Threading;

namespace battlebots
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("   BATTLE BOT SIMULATION SYSTEM");
            Console.WriteLine("  [REAL-TIME MODE]");
            Console.WriteLine("========================================");
            Console.WriteLine();

            BattleBot battleBot_RP = new BattleBot();
            battleBot_RP.Initialize();
            Console.WriteLine("[SYSTEM] BattleBot initialized successfully!");
            Console.WriteLine();

            Console.WriteLine("CONTROLS:");
            Console.WriteLine("  W  - Move Forward (hold)");
            Console.WriteLine("  A  - Turn Left (hold)");
            Console.WriteLine("  D  - Turn Right (hold)");
            Console.WriteLine("  SPACE - Attack!");
            Console.WriteLine("  Q  - Quit Simulation");
            Console.WriteLine();
            Console.WriteLine("----------------------------------------");

            battleBot_RP.StartBattle();

            bool running_RP = true;
            const int updateIntervalMs = 100; // Update every 100ms (10 times/sec)
            long lastActionTime = 0;
            const int actionCooldownMs = 200; // Minimum time between actions when holding key

            while (running_RP)
            {
                // Clear screen and display status
                Console.Clear();
                battleBot_RP.DisplayStatus();
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Hold W/A/D to move, SPACE to attack, Q to quit");
                Console.Write("----------------------------------------\nPress key: ");

                // Check for input with timeout
                while (!Console.KeyAvailable && running_RP)
                {
                    Thread.Sleep(10);
                }

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    string input = keyInfo.Key.ToString().ToUpper();

                    long currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.Q:
                            running_RP = false;
                            break;
                        case ConsoleKey.W:
                            if (currentTime - lastActionTime >= actionCooldownMs)
                            {
                                battleBot_RP.MoveForward();
                                lastActionTime = currentTime;
                            }
                            break;
                        case ConsoleKey.A:
                            if (currentTime - lastActionTime >= actionCooldownMs)
                            {
                                battleBot_RP.TurnLeft();
                                lastActionTime = currentTime;
                            }
                            break;
                        case ConsoleKey.D:
                            if (currentTime - lastActionTime >= actionCooldownMs)
                            {
                                battleBot_RP.TurnRight();
                                lastActionTime = currentTime;
                            }
                            break;
                        case ConsoleKey.Spacebar:
                            battleBot_RP.PerformAttack();
                            lastActionTime = currentTime;
                            break;
                    }
                }

                // Keep the loop running smoothly
                Thread.Sleep(updateIntervalMs);
            }

            Console.Clear();
            Console.WriteLine("\n[SYSTEM] Shutting down BattleBot...");
            battleBot_RP.Shutdown();
            Console.WriteLine("[SYSTEM] Goodbye!");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}