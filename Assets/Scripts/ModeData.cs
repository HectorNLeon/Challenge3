﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ModeData{

    private static int mode;
    public static int Mode
    {
        get
        {
            return mode;
        }
        set
        {
            mode = value;
        }
    }
}
