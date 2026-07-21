using System;

namespace battlebots
{
    /*--------------------------------------------
    PARAMETERS:
        Inherits from Weapon class for base weapon functionality
        _spinSpeed_RP (int): The rotational speed of the spinner in RPM
        _maxSpinSpeed_RP (int): The maximum achievable spin speed
        _isSpinning_RP (bool): Whether the spinner is currently rotating
        _bladeType_RP (string): Type of blade attached to the spinner
    FUNCTION:
        Specialized weapon class implementing a spinning blade/mechanism.
        Used to damage opponent bots through high-speed rotation and impact.
        
        Attack(): Activates the spinner weapon to perform an attack maneuver
                 by spinning up to max speed and striking opponents
        SpinUp(): Gradually increases spin speed to maximum
        SpinDown(): Safely decreases spin speed to zero
    OUTPUTS:
        Attack(): void - no return value, prints attack sequence to console
        SpinUp(): void - no return value
        SpinDown(): void - no return value
    ---------------------------------------------*/
    public class SpinnerWeapon : Weapon
    {
        // Current rotational speed of the spinner (RPM)
        private int _spinSpeed_RP;

        // Maximum achievable spin speed (RPM)
        private int _maxSpinSpeed_RP;

        // Whether the spinner is currently rotating
        private bool _isSpinning_RP;

        // Type of blade attached to the spinner
        private string _bladeType_RP;

        /// <summary>
        /// Constructor to initialize the spinner weapon with default values
        /// </summary>
        public SpinnerWeapon() : base("SPINNER-001", 85)
        {
            _spinSpeed_RP = 0;
            _maxSpinSpeed_RP = 3000; // 3000 RPM maximum
            _isSpinning_RP = false;
            _bladeType_RP = "Standard Blade";

            Console.WriteLine("[SPINNER] Spinner weapon system initialized");
            Console.WriteLine($"[SPINNER] Max Spin Speed: {_maxSpinSpeed_RP} RPM");
            Console.WriteLine($"[SPINNER] Blade Type: {_bladeType_RP}");
            Console.WriteLine($"[SPINNER] Damage Output: {GetDamageOutput()}%");
        }

        /// <summary>
        /// Constructor to initialize with custom parameters
        /// </summary>
        /// <param name="maxSpinSpeed_RP">Maximum spin speed in RPM</param>
        /// <param name="damageOutput_RP">Damage potential of the spinner</param>
        /// <param name="bladeType_RP">Type of blade attached</param>
        public SpinnerWeapon(int maxSpinSpeed_RP, int damageOutput_RP, string bladeType_RP) : base("SPINNER-001", damageOutput_RP)
        {
            _spinSpeed_RP = 0;
            _maxSpinSpeed_RP = maxSpinSpeed_RP;
            _isSpinning_RP = false;
            _bladeType_RP = bladeType_RP;

            Console.WriteLine("[SPINNER] Spinner weapon system initialized");
            Console.WriteLine($"[SPINNER] Max Spin Speed: {_maxSpinSpeed_RP} RPM");
            Console.WriteLine($"[SPINNER] Blade Type: {_bladeType_RP}");
            Console.WriteLine($"[SPINNER] Damage Output: {GetDamageOutput()}%");
        }

        /// <summary>
        /// Performs the spinner attack sequence
        /// Spins up to maximum speed, then strikes the opponent
        /// </summary>
        public override void Attack()
        {
            // Check if we can attack
            if (!CanAttack())
            {
                Console.WriteLine("[SPINNER] Attack aborted - weapon not ready!");
                return;
            }

            Console.WriteLine("\n[SPINNER] === ATTACK SEQUENCE INITIATED ===");
            Console.WriteLine("[SPINNER] Engaging spinner motor...");

            // Spin up to maximum speed
            SpinUp();

            // Hold at max speed for impact
            Console.WriteLine($"[SPINNER] Spinner at MAXIMUM SPEED: {_spinSpeed_RP} RPM!");
            Console.WriteLine("[SPINNER] Moving toward opponent...");
            System.Threading.Thread.Sleep(300); // Simulate approach time

            // Perform the strike
            Console.WriteLine("[SPINNER] IMPACT!!!");
            Console.WriteLine($"[SPINNER] { _bladeType_RP} striking at {_spinSpeed_RP} RPM!");
            Console.WriteLine($"[SPINNER] Damage dealt: {GetDamageOutput()}%");

            // Increment attack counter and start cooldown
            IncrementAttackCount();
            StartCooldown();

            // Spin down after attack
            Console.WriteLine("[SPINNER] Retreating from opponent...");
            SpinDown();

            Console.WriteLine("[SPINNER] === ATTACK COMPLETE ===\n");
        }

        /// <summary>
        /// Gradually spins up the spinner to maximum speed
        /// Simulates the time it takes for the motor to reach full RPM
        /// </summary>
        public void SpinUp()
        {
            if (_isSpinning_RP && _spinSpeed_RP >= _maxSpinSpeed_RP)
            {
                Console.WriteLine("[SPINNER] Spinner already at maximum speed!");
                return;
            }

            _isSpinning_RP = true;
            int spinSteps_RP = 10;
            int increment_RP = _maxSpinSpeed_RP / spinSteps_RP;

            Console.WriteLine("[SPINNER] Spinning up...");
            for (int i_RP = 1; i_RP <= spinSteps_RP; i_RP++)
            {
                _spinSpeed_RP += increment_RP;
                if (_spinSpeed_RP > _maxSpinSpeed_RP)
                {
                    _spinSpeed_RP = _maxSpinSpeed_RP;
                }

                Console.WriteLine($"[SPINNER] Speed: {_spinSpeed_RP} RPM ({i_RP}/{spinSteps_RP})");
                System.Threading.Thread.Sleep(50); // Simulate spin-up time
            }

            Console.WriteLine("[SPINNER] Maximum speed reached!");
        }

        /// <summary>
        /// Safely spins down the spinner to a stop
        /// Gradually decreases speed to prevent damage or injury
        /// </summary>
        public void SpinDown()
        {
            if (!_isSpinning_RP || _spinSpeed_RP == 0)
            {
                Console.WriteLine("[SPINNER] Spinner already stopped!");
                return;
            }

            Console.WriteLine("[SPINNER] Spinning down...");
            int spinSteps_RP = 10;
            int decrement_RP = _spinSpeed_RP / spinSteps_RP;

            for (int i_RP = 1; i_RP <= spinSteps_RP; i_RP++)
            {
                _spinSpeed_RP -= decrement_RP;
                if (_spinSpeed_RP < 0)
                {
                    _spinSpeed_RP = 0;
                }

                Console.WriteLine($"[SPINNER] Speed: {_spinSpeed_RP} RPM ({i_RP}/{spinSteps_RP})");
                System.Threading.Thread.Sleep(50); // Simulate spin-down time
            }

            _isSpinning_RP = false;
            _spinSpeed_RP = 0;
            Console.WriteLine("[SPINNER] Spinner fully stopped");
        }

        /// <summary>
        /// Gets the current spin speed
        /// </summary>
        /// <returns>Current spin speed in RPM</returns>
        public int GetSpinSpeed()
        {
            return _spinSpeed_RP;
        }

        /// <summary>
        /// Gets the maximum achievable spin speed
        /// </summary>
        /// <returns>Maximum spin speed in RPM</returns>
        public int GetMaxSpinSpeed()
        {
            return _maxSpinSpeed_RP;
        }

        /// <summary>
        /// Sets the maximum spin speed (can be adjusted for different conditions)
        /// </summary>
        /// <param name="speed_RP">New maximum spin speed in RPM</param>
        public void SetMaxSpinSpeed(int speed_RP)
        {
            if (speed_RP < 0 || speed_RP > 5000)
            {
                Console.WriteLine($"[SPINNER] WARNING: Speed {speed_RP} RPM out of valid range (0-5000)!");
                speed_RP = Math.Max(0, Math.Min(5000, speed_RP));
            }

            _maxSpinSpeed_RP = speed_RP;

            // If current speed exceeds new max, reduce it
            if (_spinSpeed_RP > _maxSpinSpeed_RP)
            {
                Console.WriteLine("[SPINNER] Current speed exceeded new maximum - reducing speed");
                _spinSpeed_RP = _maxSpinSpeed_RP;
            }

            Console.WriteLine($"[SPINNER] Maximum spin speed adjusted to {_maxSpinSpeed_RP} RPM");
        }

        /// <summary>
        /// Gets the type of blade currently attached
        /// </summary>
        /// <returns>Blade type string</returns>
        public string GetBladeType()
        {
            return _bladeType_RP;
        }

        /// <summary>
        /// Changes the blade type on the spinner
        /// </summary>
        /// <param name="bladeType_RP">New blade type to attach</param>
        public void ChangeBlade(string bladeType_RP)
        {
            if (_isSpinning_RP)
            {
                Console.WriteLine("[SPINNER] ERROR: Cannot change blade while spinning! Spin down first.");
                return;
            }

            string oldBlade_RP = _bladeType_RP;
            _bladeType_RP = bladeType_RP;

            Console.WriteLine($"[SPINNER] Blade changed: {oldBlade_RP} -> { _bladeType_RP}");
        }

        /// <summary>
        /// Checks if the spinner is currently spinning
        /// </summary>
        /// <returns>True if spinning, false if stopped</returns>
        public bool IsSpinning()
        {
            return _isSpinning_RP;
        }

        /// <summary>
        /// Gets the percentage of max speed currently achieved
        /// </summary>
        /// <returns>Percentage of maximum speed (0-100)</returns>
        public double GetSpeedPercentage()
        {
            if (_maxSpinSpeed_RP == 0)
            {
                return 0;
            }

            return ((double)_spinSpeed_RP / _maxSpinSpeed_RP) * 100;
        }

        /// <summary>
        /// Displays spinner-specific status information
        /// </summary>
        public override void DisplayWeaponInfo()
        {
            base.DisplayWeaponInfo();
            Console.WriteLine("[SPINNER] --- Spinner Specifics ---");
            Console.WriteLine($"[SPINNER] Currently Spinning: {_isSpinning_RP}");
            Console.WriteLine($"[SPINNER] Current Speed: {_spinSpeed_RP} RPM");
            Console.WriteLine($"[SPINNER] Max Speed: {_maxSpinSpeed_RP} RPM");
            Console.WriteLine($"[SPINNER] Speed Percentage: {GetSpeedPercentage():F1}%");
            Console.WriteLine($"[SPINNER] Blade Type: {_bladeType_RP}");
            Console.WriteLine("[SPINNER] -------------------------");
        }

        /// <summary>
        /// Override Activate to initialize spinner systems
        /// </summary>
        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("[SPINNER] Spinner motor engaged and ready");
            Console.WriteLine("[SPINNER] Safety interlocks disengaged");
        }

        /// <summary>
        /// Override Deactivate to safely shut down spinner systems
        /// </summary>
        public override void Deactivate()
        {
            // Spin down for safety before deactivating
            if (_isSpinning_RP)
            {
                Console.WriteLine("[SPINNER] Emergency spin-down initiated!");
                SpinDown();
            }

            base.Deactivate();
            Console.WriteLine("[SPINNER] Spinner motor powered OFF");
        }
    }
}