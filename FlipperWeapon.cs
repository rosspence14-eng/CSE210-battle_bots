using System;

namespace battlebots
{
    /*--------------------------------------------
    PARAMETERS:
        Inherits from Weapon class for base weapon functionality
        _flipAngle_RP (int): The angle in degrees the flipper arm lifts
        _hydraulicPressure_RP (double): Pressure level of hydraulic system
        _isDeployed_RP (bool): Whether the flipper is currently deployed and ready
    FUNCTION:
        Specialized weapon class implementing a flipper mechanism.
        Used to flip or launch opponent bots out of the arena.
        
        Attack(): Activates the flipper weapon to perform an attack maneuver
                 by lifting the hydraulic arm and flipping opponents
        Deploy(): Extends the flipper into position
        Retract(): Pulls the flipper back for safety
    OUTPUTS:
        Attack(): void - no return value, prints attack sequence to console
        Deploy(): void - no return value
        Retract(): void - no return value
    ---------------------------------------------*/
    public class FlipperWeapon : Weapon
    {
        // The angle the flipper arm lifts (degrees)
        private int _flipAngle_RP;

        // Hydraulic pressure level for the flipper mechanism
        private double _hydraulicPressure_RP;

        // Whether the flipper is currently deployed and ready to attack
        private bool _isDeployed_RP;

        /// <summary>
        /// Constructor to initialize the flipper weapon with default values
        /// </summary>
        public FlipperWeapon() : base("FLIPPER-001", 75)
        {
            _flipAngle_RP = 90; // Default lift angle of 90 degrees
            _hydraulicPressure_RP = 0.0;
            _isDeployed_RP = false;

            Console.WriteLine("[FLIPPER] Flipper weapon system initialized");
            Console.WriteLine($"[FLIPPER] Max Lift Angle: {_flipAngle_RP}°");
            Console.WriteLine($"[FLIPPER] Damage Output: {GetDamageOutput()}%");
        }

        /// <summary>
        /// Constructor to initialize with custom parameters
        /// </summary>
        /// <param name="flipAngle_RP">Maximum lift angle in degrees</param>
        /// <param name="damageOutput_RP">Damage potential of the flipper</param>
        public FlipperWeapon(int flipAngle_RP, int damageOutput_RP) : base("FLIPPER-001", damageOutput_RP)
        {
            _flipAngle_RP = flipAngle_RP;
            _hydraulicPressure_RP = 0.0;
            _isDeployed_RP = false;

            Console.WriteLine("[FLIPPER] Flipper weapon system initialized");
            Console.WriteLine($"[FLIPPER] Max Lift Angle: {_flipAngle_RP}°");
            Console.WriteLine($"[FLIPPER] Damage Output: {GetDamageOutput()}%");
        }

        /// <summary>
        /// Performs the flipper attack sequence
        /// Deploys the flipper, builds hydraulic pressure, then launches opponent
        /// </summary>
        public override void Attack()
        {
            // Check if we can attack
            if (!CanAttack())
            {
                Console.WriteLine("[FLIPPER] Attack aborted - weapon not ready!");
                return;
            }

            // Check if flipper is deployed
            if (!_isDeployed_RP)
            {
                Console.WriteLine("[FLIPPER] Deploying flipper arm...");
                Deploy();
            }

            Console.WriteLine("\n[FLIPPER] === ATTACK SEQUENCE INITIATED ===");
            Console.WriteLine("[FLIPPER] Building hydraulic pressure...");

            // Simulate building pressure
            _hydraulicPressure_RP = 0;
            int pressureSteps_RP = 5;
            for (int i_RP = 1; i_RP <= pressureSteps_RP; i_RP++)
            {
                _hydraulicPressure_RP += 20;
                Console.WriteLine($"[FLIPPER] Pressure: {_hydraulicPressure_RP}% ({i_RP}/{pressureSteps_RP})");
                System.Threading.Thread.Sleep(100); // Simulate delay
            }

            Console.WriteLine("[FLIPPER] Maximum pressure reached!");
            Console.WriteLine($"[FLIPPER] Lifting flipper arm to {_flipAngle_RP}°...");

            // Perform the flip
            Console.WriteLine("[FLIPPER] FLIPPING OPPONENT!!!");
            Console.WriteLine($"[FLIPPER] Opponent launched {_flipAngle_RP}° into the air!");
            Console.WriteLine($"[FLIPPER] Damage dealt: {GetDamageOutput()}%");

            // Increment attack counter and start cooldown
            IncrementAttackCount();
            StartCooldown();

            // Release pressure after attack
            _hydraulicPressure_RP = 0;
            Console.WriteLine("[FLIPPER] Hydraulic pressure released");
            Console.WriteLine("[FLIPPER] === ATTACK COMPLETE ===\n");
        }

        /// <summary>
        /// Deploys the flipper arm into attacking position
        /// </summary>
        public void Deploy()
        {
            if (_isDeployed_RP)
            {
                Console.WriteLine("[FLIPPER] Flipper already deployed!");
                return;
            }

            Console.WriteLine("[FLIPPER] Extending flipper arm...");
            System.Threading.Thread.Sleep(200); // Simulate deployment time

            _isDeployed_RP = true;
            Console.WriteLine("[FLIPPER] Flipper DEPLOYED and ready to attack!");
        }

        /// <summary>
        /// Retracts the flipper arm for safety or storage
        /// </summary>
        public void Retract()
        {
            if (!_isDeployed_RP)
            {
                Console.WriteLine("[FLIPPER] Flipper already retracted!");
                return;
            }

            Console.WriteLine("[FLIPPER] Retracting flipper arm...");

            // Release any remaining pressure for safety
            _hydraulicPressure_RP = 0;
            Console.WriteLine("[FLIPPER] Hydraulic pressure vented");

            System.Threading.Thread.Sleep(200); // Simulate retraction time

            _isDeployed_RP = false;
            Console.WriteLine("[FLIPPER] Flipper fully RETRACTED");
        }

        /// <summary>
        /// Gets the current hydraulic pressure level
        /// </summary>
        /// <returns>Current pressure as percentage</returns>
        public double GetHydraulicPressure()
        {
            return _hydraulicPressure_RP;
        }

        /// <summary>
        /// Checks if the flipper is currently deployed
        /// </summary>
        /// <returns>True if deployed, false if retracted</returns>
        public bool IsDeployed()
        {
            return _isDeployed_RP;
        }

        /// <summary>
        /// Gets the maximum flip angle
        /// </summary>
        /// <returns>Flip angle in degrees</returns>
        public int GetFlipAngle()
        {
            return _flipAngle_RP;
        }

        /// <summary>
        /// Sets the flip angle (can be adjusted for different attack styles)
        /// </summary>
        /// <param name="angle_RP">New maximum flip angle in degrees</param>
        public void SetFlipAngle(int angle_RP)
        {
            if (angle_RP < 0 || angle_RP > 180)
            {
                Console.WriteLine($"[FLIPPER] WARNING: Angle {angle_RP}° out of valid range (0-180)!");
                angle_RP = Math.Max(0, Math.Min(180, angle_RP));
            }

            _flipAngle_RP = angle_RP;
            Console.WriteLine($"[FLIPPER] Flip angle adjusted to {_flipAngle_RP}°");
        }

        /// <summary>
        /// Displays flipper-specific status information
        /// </summary>
        public override void DisplayWeaponInfo()
        {
            base.DisplayWeaponInfo();
            Console.WriteLine("[FLIPPER] --- Flipper Specifics ---");
            Console.WriteLine($"[FLIPPER] Deployed: {_isDeployed_RP}");
            Console.WriteLine($"[FLIPPER] Flip Angle: {_flipAngle_RP}°");
            Console.WriteLine($"[FLIPPER] Hydraulic Pressure: {_hydraulicPressure_RP}%");
            Console.WriteLine("[FLIPPER] ---------------------------");
        }

        /// <summary>
        /// Override Activate to initialize flipper systems
        /// </summary>
        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("[FLIPPER] Hydraulic system pressurized and ready");
            Console.WriteLine("[FLIPPER] Flipper controls ONLINE");
        }

        /// <summary>
        /// Override Deactivate to safely shut down flipper systems
        /// </summary>
        public override void Deactivate()
        {
            // Retract flipper for safety before deactivating
            if (_isDeployed_RP)
            {
                Console.WriteLine("[FLIPPER] Emergency retraction initiated!");
                Retract();
            }

            // Release all hydraulic pressure
            _hydraulicPressure_RP = 0;
            Console.WriteLine("[FLIPPER] All hydraulic systems depressurized");

            base.Deactivate();
        }
    }
}