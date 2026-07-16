# Battle Bots - C# Project

## Overview
This project implements a battle bot control system using Object-Oriented Programming principles in C#. The software architecture manages hoverboard-based robot components including motors, weapons, battery systems, and remote control inputs.

---

## Class Descriptions

### Interfaces

#### 1. `IActuator`
**Purpose:** Defines the common contract for all movable hardware components.
**Methods:**
- `Activate()` – Starts the actuator.
- `Deactivate()` – Stops the actuator.

#### 2. `ICommunicator`
**Purpose:** Standardizes communication protocols for sending and receiving data.
**Methods:**
- `SendMessage(string message)` – Transmits a message.
- `ReceiveMessage()` – Retrieves an incoming message.

---

### Base Classes

#### 3. `ActuatorBase`
**Purpose:** Abstract base class implementing `IActuator`. Provides shared functionality (e.g., ID, active state) for all actuators.
**Key Members:**
- `Id` – Unique identifier.
- `Activate()` / `Deactivate()` – Virtual methods for customization.

#### 4. `CommunicationBase`
**Purpose:** Abstract base class implementing `ICommunicator`. Handles basic communication setup like port configuration.
**Key Members:**
- `PortName` – Serial/communication port name.
- `BaudRate` – Data transmission speed.
- `SendMessage()` / `ReceiveMessage()` – Virtual methods for implementation.

---

### Hardware Components

#### 5. `Motor`
**Purpose:** Represents a hoverboard motor with speed and direction control.
**Inherits from:** `ActuatorBase`
**Key Members:**
- `Speed` – Current motor speed value.

#### 6. `MotorController`
**Purpose:** Interfaces with Electronic Speed Controllers (ESCs) to command motors.
**Inherits from:** `CommunicationBase`
**Key Members:**
- `SetMotorSpeed(int speed)` – Sends speed commands to ESCs.

#### 7. `Battery`
**Purpose:** Monitors and manages the lithium-ion battery pack status.
**Key Members:**
- `Voltage` – Current voltage level.
- `ChargeLevel` – Percentage of remaining charge.
- `IsCharging()` – Checks if the battery is being charged.

#### 8. `Weapon`
**Purpose:** Abstract base class for all weapon systems, providing common attack behavior.
**Inherits from:** `ActuatorBase`
**Key Members:**
- `Attack()` – Virtual method for executing an attack.

---

### Weapon Implementations

#### 9. `FlipperWeapon`
**Purpose:** Concrete implementation of a flipper-style weapon.
**Inherits from:** `Weapon`
**Behavior:** Activates a flipping mechanism to toss opponents.

#### 10. `SpinnerWeapon`
**Purpose:** Concrete implementation of a spinning blade/hammer weapon.
**Inherits from:** `Weapon`
**Behavior:** Spins at high velocity to strike opponents.

---

### Bot Structure & Control

#### 11. `DriveSystem`
**Purpose:** Manages the left and right motors for locomotion.
**Key Members:**
- `_leftMotor` – Left motor instance.
- `_rightMotor` – Right motor instance.
- `MoveForward()`, `TurnLeft()`, `TurnRight()` – High-level movement commands.

#### 12. `BattleBot`
**Purpose:** Main orchestrator class that integrates all subsystems (drive, power, weaponry).
**Key Members:**
- `_driveSystem` – Reference to the drive system.
- `_battery` – Reference to the battery manager.
- `_weapon` – Reference to the active weapon.
- `Initialize()` – Sets up all components.
- `StartBattle()` – Begins autonomous or remote-controlled operation.

#### 13. `RemoteControl`
**Purpose:** Reads inputs from an RC transmitter and translates them into bot actions.
**Key Members:**
- `ReadInputs()` – Polls the receiver for stick positions/buttons.

---

### Utility

#### 14. `Logger`
**Purpose:** Records system events, telemetry data, and errors for debugging and analysis.
**Key Members:**
- `LogMessage(string message)` – Writes a timestamped entry to logs.

---

## OOP Principles Demonstrated

| Principle      | Example in Code                                                                 |
|----------------|---------------------------------------------------------------------------------|
| **Abstraction**   | `IActuator` and `ICommunicator` hide complex hardware details behind simple interfaces.             |
| **Encapsulation** | Private fields (e.g., `_battery`) are accessed via public properties/methods.                      |
| **Inheritance**   | `FlipperWeapon` and `SpinnerWeapon` inherit from `Weapon`; `Motor` inherits from `ActuatorBase`.   |
| **Polymorphism**  | `BattleBot` calls `_weapon.Attack()` without knowing the specific weapon type at compile time.      |

---

## How to Run
1. Ensure you have .NET SDK installed.
2. Build the project:
   ```bash
   dotnet build
   ```
3. Run the application:
   ```bash
   dotnet run
   ```

## Safety Notes
⚠️ **Handle lithium-ion batteries with care.** Always use proper discharge procedures and avoid short circuits.
