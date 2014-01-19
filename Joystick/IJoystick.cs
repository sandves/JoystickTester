﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoystickInterface
{
    public interface IJoystick
    {
        int Throttle();
        int Pitch();
        int Roll();
        int Yaw();
        byte[] Buttons();
        int PointOfView();
    }
}