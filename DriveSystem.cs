using System;

namespace battlebots
{
    /*--------------------------------------------
    parameeters:
        _leftMotor (Motor): The motor controlling the left-side wheels/tracks
        _rightMotor (Motor): The motor controlling the right-side wheels/tracks
    funtion:
        Manages the movement and navigation of the battlebot
        MoveForward(): Drives the bot forward by activating both motors
        TurnLeft(): Rotates the bot to the left by adjusting motor speeds
        TurnRight(): Rotates the bot to the right by adjusting motor speeds
    outputs:
        MoveForward(): void - no return value
        TurnLeft(): void - no return value
        TurnRight(): void - no return value
    ---------------------------------------------*/
    public class DriveSystem
    {
        private Motor _leftMotor;
        private Motor _rightMotor;

        public void MoveForward() { }
        public void TurnLeft() { }
        public void TurnRight() { }
    }
}