# Battle Bot Project Plan: Coding with Classes

## 1. Introduction

This document outlines the comprehensive plan for building a battle bot using repurposed hoverboard components, adhering to the "Coding with Classes" project requirements. The project emphasizes cost-effectiveness, utilizing readily available or salvaged parts, particularly from broken hoverboards for propulsion. The software component will be developed in C#, demonstrating core Object-Oriented Programming (OOP) principles: abstraction, encapsulation, inheritance, and polymorphism, through a minimum of 14 distinct classes.

## 2. Hardware Components

The primary goal is to leverage components from broken hoverboards to minimize costs. The core components will be the hoverboard motors and their associated motor controllers. Additional components will be sourced for the chassis, power, control, and weaponry.

### 2.1. Hoverboard Components

| Component | Description | Quantity | Notes |
|---|---|---|---|
| **Hoverboard Motors (Wheels)** | Brushless DC (BLDC) hub motors, typically 250W-350W. | 2 | These will provide the primary propulsion for the bot. |
| **Hoverboard Motor Controllers (ESCs)** | The Electronic Speed Controllers (ESCs) that drive the BLDC motors. | 2 | Often integrated into the main hoverboard control board or as separate units. Hacking their firmware or using an external microcontroller to send commands via UART is crucial [1] [2]. |
| **Hoverboard Battery Pack** | Lithium-ion battery pack (e.g., 36V, 4.4Ah). | 1 | Provides power to the motors and electronics. Ensure it's in good condition and has a Battery Management System (BMS) for safety. |
| **Hoverboard Chassis/Frame** | Parts of the hoverboard's internal frame or outer casing. | Variable | Can be repurposed for structural elements or mounting points. |

### 2.2. Control and Communication

| Component | Description | Quantity | Notes |
|---|---|---|---|
| **Microcontroller (e.g., Arduino Mega, ESP32)** | To interpret control signals and communicate with the hoverboard ESCs. | 1 | An Arduino Mega is often recommended due to multiple UART ports for communication with two ESCs [3]. ESP32 offers Wi-Fi/Bluetooth for wireless control. |
| **Radio Control (RC) System** | Transmitter (remote) and Receiver. | 1 set | Standard RC car/drone remote for manual control. Ensure sufficient channels for movement and weapon activation. |
| **Voltage Regulator (Buck Converter)** | To step down the hoverboard battery voltage (e.g., 36V) to 5V/12V for the microcontroller and other electronics. | 1-2 | Essential for powering sensitive components safely. |

### 2.3. Chassis and Structure

| Component | Description | Quantity | Notes |
|---|---|---|---|
| **Chassis Material** | Plywood, aluminum sheets, or salvaged plastic/metal from other devices. | Variable | Focus on durability and ease of fabrication. Consider modular design for repairs. |
| **Fasteners** | Screws, nuts, bolts, zip ties, strong adhesive. | Variable | For assembling the chassis and mounting components. |

### 2.4. Weaponry (Example)

| Component | Description | Quantity | Notes |
|---|---|---|---|
| **Weapon Motor (e.g., small DC motor, servo)** | For activating a simple weapon mechanism (e.g., flipper, spinner, wedge). | 1 | Depends on the chosen weapon design. A small DC motor with a gear reduction or a high-torque servo could work. |
| **Weapon Mechanism** | Salvaged metal, plastic, or custom-fabricated parts. | 1 | Design for impact and effectiveness while adhering to competition rules (if applicable). |

## 3. Construction Steps

1.  **Disassemble Hoverboards**: Carefully dismantle the broken hoverboards, salvaging motors, motor controllers, and battery packs. Document wiring and connections. **Safety Note**: Lithium-ion batteries can be dangerous if punctured or short-circuited. Handle with extreme care. 
2.  **Chassis Design and Fabrication**: Design a robust and compact chassis. Cut and assemble the chosen materials (plywood, aluminum) to form the bot's frame. Ensure adequate space for all components and easy access for maintenance. 
3.  **Motor Controller Hacking/Interfacing**: This is the most critical step. Research specific hoverboard motor controller models. Many guides exist for flashing custom firmware (e.g., VESC, custom open-source firmware) or interfacing with the existing firmware via UART serial communication [1] [2] [3]. The goal is to control motor speed and direction using signals from the microcontroller. 
4.  **Power System Integration**: Mount the hoverboard battery securely. Install voltage regulators to provide appropriate power to the microcontroller and other accessories. Wire all power connections, ensuring proper polarity and fusing where necessary. 
5.  **Microcontroller Setup**: Program the microcontroller (e.g., Arduino) to receive commands from the RC receiver and translate them into appropriate serial commands for the hoverboard motor controllers. Implement basic motor control logic (forward, backward, turn). 
6.  **RC System Integration**: Connect the RC receiver to the microcontroller. Map RC channels to motor control and weapon activation. 
7.  **Weapon System Integration**: Fabricate and mount the chosen weapon mechanism. Connect its motor/servo to the microcontroller and power supply. Program the microcontroller to activate the weapon based on RC input. 
8.  **Wiring and Cable Management**: Neatly route and secure all wiring to prevent entanglement and damage during combat. Use connectors where possible for easier assembly/disassembly. 
9.  **Testing and Calibration**: Thoroughly test all functionalities: motor control, steering, weapon activation, and emergency stops. Calibrate motor speeds and weapon timing for optimal performance. 
10. **Armor and Aesthetics**: Add protective armor to vulnerable areas using chosen materials. Consider the bot's center of gravity and overall weight distribution. 

## 4. Software Architecture (C#)

The C# software will run on a companion computer (e.g., Raspberry Pi running Windows IoT Core, or a full Windows machine) that communicates with the microcontroller. This allows for more complex logic, telemetry, and potential future upgrades like autonomous control. The architecture will strictly adhere to OOP principles.

### 4.1. OOP Principles in Practice

*   **Abstraction**: Hiding complex implementation details and showing only essential features. For example, a `Motor` class abstracts the low-level serial commands sent to the ESC, exposing simple methods like `SetSpeed(int speed)` and `Stop()`. 
*   **Encapsulation**: Bundling data (attributes) and methods (functions) that operate on the data within a single unit (class), and restricting direct access to some of the object's components. Private fields with public properties (get/set) are a prime example. The `Battery` class encapsulates its charge level and provides methods to check its status. 
*   **Inheritance**: A mechanism where a new class (derived class) inherits properties and behaviors from an existing class (base class). This promotes code reusability. For instance, `Weapon` could be a base class, with `FlipperWeapon` and `SpinnerWeapon` inheriting from it. 
*   **Polymorphism**: The ability of an object to take on many forms. In C#, this is often achieved through method overriding (runtime polymorphism) or method overloading (compile-time polymorphism), and interfaces. A `BattleBot` class might have a `ActivateWeapon()` method that behaves differently depending on the specific `Weapon` type attached. 

### 4.2. Class Outline (14+ Classes)

Here is a proposed list of classes, grouped by their functional area, demonstrating the required OOP principles:

#### Base Classes and Interfaces (Abstraction & Inheritance)

1.  `IActuator` (Interface): Defines common behavior for all movable parts (e.g., `Move()`, `Stop()`). 
2.  `ActuatorBase` (Abstract Class): Provides common properties (e.g., `ID`, `IsActive`) and implements `IActuator`. 
3.  `ICommunicator` (Interface): Defines methods for sending/receiving data (e.g., `SendMessage(string data)`, `ReceiveMessage()`). 
4.  `CommunicationBase` (Abstract Class): Handles common communication setup (e.g., `PortName`, `BaudRate`) and implements `ICommunicator`. 

#### Hardware Control (Encapsulation & Abstraction)

5.  `Motor` (inherits `ActuatorBase`): Represents a single hoverboard motor. Encapsulates speed, direction, and sends commands via a `MotorController` instance. 
6.  `MotorController` (inherits `CommunicationBase`): Manages communication with a single hoverboard ESC (e.g., via UART). Abstracts the serial protocol details. 
7.  `Battery` (Encapsulation): Manages battery state (charge level, voltage). Provides methods to check status and warnings. 
8.  `Weapon` (inherits `ActuatorBase`): Base class for all weapon types. Defines common weapon actions (e.g., `Activate()`, `Deactivate()`). 

#### Specific Weapon Implementations (Inheritance & Polymorphism)

9.  `FlipperWeapon` (inherits `Weapon`): Implements specific logic for a flipper mechanism. Overrides `Activate()` to perform flipper action. 
10. `SpinnerWeapon` (inherits `Weapon`): Implements specific logic for a spinning weapon. Overrides `Activate()` to control spinner motor. 

#### Bot Structure and Control (Encapsulation & Composition)

11. `DriveSystem` (Encapsulation, Composition): Composes two `Motor` instances (left and right). Provides high-level movement commands (e.g., `MoveForward()`, `TurnLeft()`). 
12. `BattleBot` (Encapsulation, Composition): The main bot class. Composes `DriveSystem`, `Battery`, and a `Weapon` instance. Orchestrates overall bot behavior. 
13. `RemoteControl` (Encapsulation): Interprets signals from the RC receiver. Translates raw RC input into commands for the `BattleBot` (e.g., `Throttle`, `Steering`, `WeaponTrigger`). 

#### Utility/Logging (Encapsulation)

14. `Logger` (Encapsulation): Provides logging functionality for debugging and telemetry. Can log events, errors, and sensor data to a file or console. 

#### Example of Polymorphism

The `BattleBot` class could have a method `PerformAttack()` that calls `_weapon.Activate()`. Due to polymorphism, if `_weapon` is a `FlipperWeapon`, it will execute the flipper's activation logic; if it's a `SpinnerWeapon`, it will execute the spinner's logic, without the `BattleBot` class needing to know the specific weapon type at compile time.

```csharp
// Example of Polymorphism
public class BattleBot
{
    private DriveSystem _driveSystem;
    private Battery _battery;
    private Weapon _weapon; // Can be FlipperWeapon, SpinnerWeapon, etc.

    public BattleBot(DriveSystem driveSystem, Battery battery, Weapon weapon)
    {
        _driveSystem = driveSystem;
        _battery = battery;
        _weapon = weapon;
    }

    public void PerformAttack()
    {
        Console.WriteLine("Initiating attack sequence...");
        _weapon.Activate(); // Polymorphic call
    }

    // Other bot control methods...
}

public abstract class Weapon : ActuatorBase
{
    public abstract void Activate();
    public abstract void Deactivate();
}

public class FlipperWeapon : Weapon
{
    public override void Activate()
    {
        Console.WriteLine("Flipper weapon activated!");
        // Logic to move flipper motor
    }

    public override void Deactivate()
    {
        Console.WriteLine("Flipper weapon deactivated.");
        // Logic to stop flipper motor
    }
}

public class SpinnerWeapon : Weapon
{
    public override void Activate()
    {
        Console.WriteLine("Spinner weapon activated!");
        // Logic to start spinner motor
    }

    public override void Deactivate()
    {
        Console.WriteLine("Spinner weapon deactivated.");
        // Logic to stop spinner motor
    }
}
```

## 5. Additional Information and Tips

*   **Safety First**: Always prioritize safety. Work with batteries carefully, use appropriate protective gear, and ensure all electrical connections are secure. Implement an emergency kill switch for the bot. 
*   **Modular Design**: Design the bot in modules (drive system, weapon system, control system) to facilitate easier assembly, testing, and repairs. 
*   **Weight Distribution**: Pay close attention to the bot's center of gravity. A well-balanced bot is more stable and maneuverable. 
*   **Documentation**: Keep detailed notes on wiring, code, and design decisions. This will be invaluable for debugging and future modifications. 
*   **Community Resources**: Leverage online communities (e.g., Reddit r/battlebots, robotics forums) for advice, troubleshooting, and inspiration. Many hobbyists have successfully repurposed hoverboard components. 
*   **Competition Rules**: If participating in a competition, thoroughly review the rules regarding weight limits, weapon types, and safety requirements. 

## 6. References

[1] Reddit. (2025, March 16). *How to Control Hoverboard Wheels Using Arduino and ...*. [https://www.reddit.com/r/robotics/comments/1jcvkd3/how_to_control_hoverboard_wheels_using_arduino/](https://www.reddit.com/r/robotics/comments/1jcvkd3/how_to_control_hoverboard_wheels_using_arduino/)
[2] Hackaday.io. (2020, April 16). *Project | Hoverboards for Assistive Devices*. [https://hackaday.io/project/170932/logs?sort=oldest](https://hackaday.io/project/170932/logs?sort=oldest)
[3] Medium. (2023, May 13). *Cheaper way to control Hoverboard motors - RoboFoundry*. [https://robofoundry.medium.com/cheaper-way-to_control-hoverboard-motors-79b02dd8a521](https://robofoundry.medium.com/cheaper-way-to-control-hoverboard-motors-79b02dd8a521)
[4] Medium. (2024, September 30). *Mastering the Four Pillars of Object-Oriented Programming in C#*. [https://rafaelaraujolima.medium.com/mastering-the-four-pillars-of-object-oriented-programming-in-c-f5c16e4827ee](https://rafaelaraujolima.medium.com/mastering-the-four-pillars-of-object-oriented-programming-in-c-f5c16e4827ee)
[5] Pluralsight. (2026, January 16). *Guided: Exploring the Pillars of Object-Oriented Programming in C#*. [https://www.pluralsight.com/labs/codeLabs/guided-exploring-the-pillars-of-object-oriented-programming-in-c](https://www.pluralsight.com/labs/codeLabs/guided-exploring-the-pillars-of-object-oriented-programming-in-c)
