﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{


    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game,
        Start,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public enum UIEvent
    {
        Click,
        Drag,
        Enter,
        Exit
    }
}
