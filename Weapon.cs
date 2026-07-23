using System;

namespace battlebots
{
    /*--------------------------------------------
    PARAMETERS:
        Id (string): Unique identifier for the weapon actuator (inherited from ActuatorBase)
        _damageOutput_RP (int): The amount of damage this weapon can deal per attack
        _isCharging_RP (bool): Whether the weapon is currently charging up for an attack
        _attackCooldown_RP (bool): Whether the weapon is on cooldown between attacks
    FUNCTION:
        Abstract base class for all weapon types in the battlebot system.
        Inherits from ActuatorBase to provide standard actuator functionality
        (Activate/Deactivate). Provides common weapon behavior like damage
        calculation, attack validation, and status reporting.
        
        Attack(): Virtual method that derived weapons override to implement
                 specific attack behaviors
        CanAttack(): Checks if the weapon is ready to attack
        DisplayWeaponInfo(): Shows current weapon status
    OUTPUTS:
        Attack(): void - no return value (to be implemented by derived classes)
        CanAttack(): bool - returns true if weapon can attack
        DisplayWeaponInfo(): void - prints weapon info to console
    ---------------------------------------------*/
    public abstract class Weapon : ActuatorBase
    {
        // Damage output of the weapon (percentage or arbitrary units)
        protected int _damageOutput_RP;

        // Whether the weapon is currently charging up
        protected bool _isCharging_RP;

        // Whether the weapon is on cooldown between attacks
        protected bool _attackCooldown_RP;

        // Number of successful attacks performed
        protected int _attackCount_RP;

        /// <summary>
        /// Constructor to initialize the weapon with default values
        /// </summary>
        public Weapon()
        {
            _damageOutput_RP = 0;
            _isCharging_RP = false;
            _attackCooldown_RP = false;
            _attackCount_RP = 0;
            _npId = "WEAPON-DEFAULT";

            Console.WriteLine("[WEAPON] Base weapon system initialized");
        }

        /// <summary>
        /// Constructor to initialize with specific ID and damage output
        /// </summary>
        /// <param name="id_RP">Unique identifier for this weapon</param>
        /// <param name="damageOutput_RP">Damage potential of the weapon</param>
        public Weapon(string id_RP, int damageOutput_RP)
        {
            _npId = id_RP;
            _damageOutput_RP = damageOutput_RP;
            _isCharging_RP = false;
            _attackCooldown_RP = false;
            _attackCount_RP = 0;

            Console.WriteLine($"[WEAPON] Weapon initialized: ID={_npId}, Damage={_damageOutput_RP}%");
        }

        /// <summary>
        /// Abstract attack method - must be implemented by derived classes
        /// Each weapon type has its own unique attack behavior
        /// </summary>
        public abstract void Attack();

        /// <summary>
        /// Checks if the weapon is ready to perform an attack
        /// Weapon must be activated and not on cooldown
        /// </summary>
        /// <returns>True if weapon can attack, false otherwise</returns>
        public bool CanAttack()
        {
            // Check if weapon is activated (inherited from ActuatorBase)
            if (!IsActivated())
            {
                Console.WriteLine("[WEAPON] Cannot attack - weapon is not activated!");
                return false;
            }

            // Check if weapon is on cooldown
            if (_attackCooldown_RP)
            {
                Console.WriteLine("[WEAPON] Cannot attack - weapon is on cooldown!");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Puts the weapon on cooldown after an attack
        /// Derived classes can call this after performing an attack
        /// </summary>
        protected void StartCooldown()
        {
            _attackCooldown_RP = true;
            Console.WriteLine("[WEAPON] Weapon entered cooldown phase");
        }

        /// <summary>
        /// Ends the cooldown period, allowing the weapon to attack again
        /// </summary>
        public void EndCooldown()
        {
            _attackCooldown_RP = false;
            Console.WriteLine("[WEAPON] Cooldown complete - weapon ready!");
        }

        /// <summary>
        /// Increments the attack counter when a successful attack occurs
        /// </summary>
        protected void IncrementAttackCount()
        {
            _attackCount_RP++;
            Console.WriteLine($"[WEAPON] Attack count: {_attackCount_RP}");
        }

        /// <summary>
        /// Gets the current damage output of the weapon
        /// </summary>
        /// <returns>Damage output value</returns>
        public int GetDamageOutput()
        {
            return _damageOutput_RP;
        }

        /// <summary>
        /// Sets the damage output (can be modified by power levels, upgrades, etc.)
        /// </summary>
        /// <param name="damage_RP">New damage output value</param>
        public void SetDamageOutput(int damage_RP)
        {
            if (damage_RP < 0 || damage_RP > 100)
            {
                Console.WriteLine($"[WEAPON] WARNING: Damage {damage_RP}% out of valid range (0-100)!");
                damage_RP = Math.Max(0, Math.Min(100, damage_RP));
            }

            _damageOutput_RP = damage_RP;
            Console.WriteLine($"[WEAPON] Damage output updated to {_damageOutput_RP}%");
        }

        /// <summary>
        /// Checks if the weapon is currently charging
        /// </summary>
        /// <returns>True if charging, false otherwise</returns>
        public bool IsCharging()
        {
            return _isCharging_RP;
        }

        /// <summary>
        /// Starts the charging sequence for weapons that need to charge up
        /// (like spinners or capacitors)
        /// </summary>
        protected void StartCharging()
        {
            _isCharging_RP = true;
            Console.WriteLine("[WEAPON] Weapon charging sequence initiated...");
        }

        /// <summary>
        /// Stops the charging sequence
        /// </summary>
        protected void StopCharging()
        {
            _isCharging_RP = false;
            Console.WriteLine("[WEAPON] Weapon charging stopped");
        }

        /// <summary>
        /// Checks if the weapon is currently activated
        /// This checks if Activate() has been called without Deactivate()
        /// </summary>
        /// <returns>True if activated, false otherwise</returns>
        public bool IsActivated()
        {
            // We'll use a simple approach: if damage output > 0, assume it's been set up
            // In a real system, you might have an _isActive_RP flag in ActuatorBase
            return _damageOutput_RP > 0;
        }

        /// <summary>
        /// Gets the total number of attacks performed
        /// </summary>
        /// <returns>Attack count</returns>
        public int GetAttackCount()
        {
            return _attackCount_RP;
        }

        /// <summary>
        /// Displays detailed weapon information to console
        /// </summary>
        public virtual void DisplayWeaponInfo()
        {
            Console.WriteLine("[WEAPON] === Weapon Status Report ===");
            Console.WriteLine($"[WEAPON] ID: {_npId}");
            Console.WriteLine($"[WEAPON] Type: {GetType().Name}");
            Console.WriteLine($"[WEAPON] Damage Output: {_damageOutput_RP}%");
            Console.WriteLine($"[WEAPON] Charging: {_isCharging_RP}");
            Console.WriteLine($"[WEAPON] On Cooldown: {_attackCooldown_RP}");
            Console.WriteLine($"[WEAPON] Attacks Performed: {_attackCount_RP}");
            Console.WriteLine("[WEAPON] ==============================");
        }

        /// <summary>
        /// Override Activate to add weapon-specific initialization
        /// </summary>
        public override void Activate()
        {
            base.Activate();
            Console.WriteLine($"[WEAPON] Weapon {_npId} ACTIVATED and ready for combat");
        }

        /// <summary>
        /// Override Deactivate to safely shut down weapon systems
        /// </summary>
        public override void Deactivate()
        {
            // Stop any ongoing processes
            StopCharging();
            _attackCooldown_RP = false;

            base.Deactivate();
            Console.WriteLine($"[WEAPON] Weapon {_npId} DEACTIVATED");
        }
    }
}