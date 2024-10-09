using System.Collections.Generic;
using UnityEngine;

public enum ControlScheme
{
    Keyboard,
    Gamepad
}

public class CustomInputSystem : MonoBehaviour
{
    private static Dictionary<string, KeyCode> _keyMapping = new Dictionary<string, KeyCode>();
    private static Dictionary<string, KeyCode> _keyMappingGamepad = new Dictionary<string, KeyCode>();
    private static Dictionary<string, string> _axisMapping = new Dictionary<string, string>();
    private static Dictionary<string, string> _axisMappingGamepad = new Dictionary<string, string>();

    private void Awake()
    {
        InitializeDefaultKey();
        InitializeDefaultAxis();

        DontDestroyOnLoad(gameObject);
    }

    private void InitializeDefaultKey()
    {
        //Player 1
        _keyMapping["Jump1"] = KeyCode.Space;
        _keyMapping["Fire1"] = KeyCode.Q;
        _keyMappingGamepad["Jump1"] = KeyCode.Joystick1Button0;
        _keyMappingGamepad["Fire1"] = KeyCode.Joystick1Button2;

        //Player 2
        _keyMapping["Jump2"] = KeyCode.UpArrow;
        _keyMapping["Fire2"] = KeyCode.KeypadEnter;
        _keyMappingGamepad["Jump2"] = KeyCode.Joystick2Button0;
        _keyMappingGamepad["Fire2"] = KeyCode.Joystick2Button2;
    }

    private void InitializeDefaultAxis()
    {
        _axisMapping["Horizontal1"] = "Horizontal";
        _axisMappingGamepad["Horizontal1"] = "HorizontalGamepad";

        _axisMapping["Horizontal2"] = "Horizontal2";
        _axisMappingGamepad["Horizontal2"] = "HorizontalGamepad2";
    }

    public static bool GetKeyDown(string action, ControlScheme controlScheme)
    {
        if (controlScheme == ControlScheme.Gamepad)
        {
            if (_keyMappingGamepad.ContainsKey(action))
            {
                return Input.GetKeyDown(_keyMappingGamepad[action]);
            }
        }
        else
        {
            if (_keyMapping.ContainsKey(action))
            {
                return Input.GetKeyDown(_keyMapping[action]);
            }
        }

        return false;
    }

    public static float GetAxis(string action, ControlScheme controlScheme)
    {
        if (controlScheme == ControlScheme.Gamepad)
        {
            if (_axisMappingGamepad.ContainsKey(action))
            {
                return Input.GetAxis(_axisMappingGamepad[action]);
            }
        }
        else
        {
            if (_axisMapping.ContainsKey(action))
            {
                return Input.GetAxis(_axisMapping[action]);
            }
        }

        return 0f;
    }

    public static void RebindKey(string action, KeyCode newKey)
    {
        if (_keyMapping.ContainsKey(action))
        {
            if (!CheckKeyDuplicate(newKey))
                _keyMapping[action] = newKey;
        }
    }

    private static bool CheckKeyDuplicate(KeyCode newKey)
    {
        foreach (KeyValuePair<string, KeyCode> entry in _keyMapping)
        {
            if (entry.Value == newKey)
            {
                return true;
            }
        }

        return false;
    }

    public static KeyCode GetBoundKey(string action)
    {
        if (_keyMapping.ContainsKey(action))
        {
            return _keyMapping[action];
        }

        return KeyCode.None;
    }
}
