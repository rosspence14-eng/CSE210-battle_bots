using System;

namespace battlebots
{
    /*--------------------------------------------
    PARAMETERS:
        None - interface definition only
    FUNCTION:
        Defines the contract for all actuator components in the battlebot system.
        An actuator is any hardware component that performs physical action,
        such as motors, weapons, flipper arms, spinner blades, etc.
        
        All actuators must implement this interface to ensure consistent
        behavior across different component types. This allows the BattleBot
        class to control any actuator without knowing its specific type.
        
        Activate(): Declares method to power on/enable an actuator
        Deactivate(): Declares method to power off/disable an actuator
    OUTPUTS:
        Activate(): void (to be implemented by derived classes)
        Deactivate(): void (to be implemented by derived classes)
    ---------------------------------------------*/
    public interface IActuator
    {
        /// <summary>
        /// Unique identifier for this actuator component.
        /// Used to distinguish between multiple actuators of the same type
        /// (e.g., left motor vs right motor, or different weapons).
        /// </summary>
        string _npId { get; set; }

        /// <summary>
        /// Indicates whether the actuator is currently active.
        /// </summary>
        bool _npIsActive { get; }

        /// <summary>
        /// Powers on or enables the actuator.
        /// Implementation should:
        /// - Initialize hardware resources
        /// - Set initial state values
        /// - Perform any startup checks
        /// - Log activation status
        /// </summary>
        void Activate();

        /// <summary>
        /// Powers off or disables the actuator.
        /// Implementation should:
        /// - Stop all active operations safely
        /// - Release hardware resources
        /// - Reset state values if needed
        /// - Log deactivation status
        /// </summary>
        void Deactivate();
    }
}