using System;
using UnityEngine;

public static class InputHandler
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        inputInstance = new Input();
        inputInstance.Enable();
    }

    private static Input inputInstance;
    public static Input.InGameActions InGameActions => inputInstance.InGame;
}
