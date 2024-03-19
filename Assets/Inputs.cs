using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    public int? gamepadIndex = null;
    public int zoop;

    public void SetGamepadIndex(int gamepadIndex)
    {
        this.gamepadIndex = gamepadIndex;
        zoop = gamepadIndex;
    }

    public bool HasKeyboard()
    {
        Debug.Assert(gamepadIndex.HasValue);
        return gamepadIndex.Value == 0;
    }

    public int GamepadIndex()
    {
        Debug.Assert(gamepadIndex.HasValue);
        return gamepadIndex.Value;
    }


}
