using System;
using UnityEngine;

public static class PauseManager
{
    public static bool IsPaused
    {
        get
        {
            return StartCountDownController.IsCountingDown;
        }
    }
}
