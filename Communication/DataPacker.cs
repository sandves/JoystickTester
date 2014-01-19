﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JoystickInterface;

namespace Communication
{
    public class DataPacker
    {

        private const byte startByte = 255;
        private const byte stopByte = 251;

        [Flags]
        private enum pointOfView : byte
        {
            Center = 0,     //0000
            Right = 1 << 0, //0001
            Left = 1 << 1,  //0010
            Down = 1 << 2,  //0100
            Up = 1 << 3,    //1000

            UpAndRight = Up | Right,        //1001
            DownAndRight = Down | Right,    //0100
            DownAndLeft = Down | Left,      //0110
            UpAndLeft = Up | Left           //1010
        }

        private IJoystick joystick;

        public DataPacker(IJoystick joystick)
        {
            this.joystick = joystick;
        }

        public byte ButtonsPressed()
        {
            int buttons = 0;
            byte[] buttonsPressed = joystick.Buttons();

            for (int i = 0; i < 7 ; i++)
            {
                if (buttonsPressed[i] != 0)
                {
                    int currentButton = (1 << i);
                    buttons |= currentButton;
                }
            }
            return (byte)buttons;
        }

        public byte HatPov()
        {
            int angle = joystick.PointOfView();
            switch(angle)
            {
                case 0:
                    return (byte)pointOfView.Up;
                case 45:
                    return (byte)pointOfView.UpAndRight;
                case 90:
                    return (byte)pointOfView.Right;
                case 135:
                    return (byte)pointOfView.DownAndRight;
                case 180:
                    return (byte)pointOfView.Down;
                case 225:
                    return (byte)pointOfView.DownAndLeft;
                case 270:
                    return (byte)pointOfView.Left;
                case 315:
                    return (byte)pointOfView.UpAndLeft;
                default:
                    return (byte)pointOfView.Center;
            }
        }

    }
}