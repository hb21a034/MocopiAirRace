using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

[StructLayout(LayoutKind.Explicit, Size = 32)]
struct LeftJoyconInputReport : IInputStateTypeInfo
{
    // Because all HID input reports are tagged with the 'HID ' FourCC,
    // this is the format we need to use for this state struct.
    public FourCC format => new FourCC('H', 'I', 'D');

    [InputControl(name = "buttonSouth", displayName = "Down", bit = 1)]
    [InputControl(name = "buttonWest", displayName = "Left", bit = 0)]
    [InputControl(name = "buttonEast", displayName = "Right", bit = 3)]
    [InputControl(name = "buttonNorth", displayName = "Up", bit = 2)]
    [InputControl(name = "leftStickPress", displayName = "SL", bit = 4)]
    [InputControl(name = "rightStickPress", displayName = "SR", bit = 5)]
    [FieldOffset(1)] public byte buttons1;

    //Stickpress is not assigned
    //    [InputControl(name = "leftStickPress", displayName = "StickPress", bit = 2)]
    [InputControl(name = "start", displayName = "Minus", bit = 0)]
    [InputControl(name = "select", displayName = "Cam", bit = 5)]
    [InputControl(name = "leftTrigger", displayName = "Trigger", format = "BIT", bit = 7)]
    [InputControl(name = "leftShoulder", displayName = "Shoulder", bit = 6)]
    [FieldOffset(2)] public byte buttons2;

    [InputControl(name = "dpad", format = "BIT", layout = "Dpad", sizeInBits = 4, defaultState = 8)]
    [InputControl(name = "dpad/up", format = "BIT", layout = "DiscreteButton", parameters = "minValue=5,maxValue=7", bit = 0, sizeInBits = 4)]
    [InputControl(name = "dpad/right", format = "BIT", layout = "DiscreteButton", parameters = "minValue=7,maxValue=1,nullValue=8,wrapAtValue=7", bit = 0, sizeInBits = 4)]
    [InputControl(name = "dpad/down", format = "BIT", layout = "DiscreteButton", parameters = "minValue=1,maxValue=3", bit = 0, sizeInBits = 4)]
    [InputControl(name = "dpad/left", format = "BIT", layout = "DiscreteButton", parameters = "minValue=3, maxValue=5", bit = 0, sizeInBits = 4)]
    [FieldOffset(3)] public byte leftStickX;
}

[StructLayout(LayoutKind.Explicit, Size = 32)]
struct RightJoyconInputReport : IInputStateTypeInfo
{
    // Because all HID input reports are tagged with the 'HID ' FourCC,
    // this is the format we need to use for this state struct.
    public FourCC format => new FourCC('H', 'I', 'D');

    [InputControl(name = "buttonSouth", displayName = "Down", bit = 2)]
    [InputControl(name = "buttonWest", displayName = "Left", bit = 3)]
    [InputControl(name = "buttonEast", displayName = "Right", bit = 0)]
    [InputControl(name = "buttonNorth", displayName = "Up", bit = 1)]
    [InputControl(name = "leftStickPress", displayName = "SL", bit = 4)]
    [InputControl(name = "rightStickPress", displayName = "SR", bit = 5)]
    [FieldOffset(1)] public byte buttons1;

    //Stickpress is not assigned

    //   [InputControl(name = "rightStickPress", displayName = "StickPress", bit = 3)]
    [InputControl(name = "start", displayName = "Plus", bit = 1)]
    [InputControl(name = "select", displayName = "Home", bit = 4)]
    [InputControl(name = "rightTrigger", displayName = "Trigger", bit = 7, format = "BIT")]
    [InputControl(name = "rightShoulder", displayName = "Shoulder", bit = 6)]
    [FieldOffset(2)] public byte buttons2;

    [InputControl(name = "dpad", format = "BIT", layout = "Dpad", sizeInBits = 4, defaultState = 8)]
    [InputControl(name = "dpad/up", format = "BIT", layout = "DiscreteButton", parameters = "minValue=1,maxValue=3", bit = 0, sizeInBits = 4)]
    [InputControl(name = "dpad/right", format = "BIT", layout = "DiscreteButton", parameters = "minValue=3,maxValue=5", bit = 0, sizeInBits = 4)]
    [InputControl(name = "dpad/down", format = "BIT", layout = "DiscreteButton", parameters = "minValue=5, maxValue=7", bit = 0, sizeInBits = 4)]
    [InputControl(name = "dpad/left", format = "BIT", layout = "DiscreteButton", parameters = "minValue=7,maxValue=1,nullValue=8,wrapAtValue=7", bit = 0, sizeInBits = 4)]
    [FieldOffset(3)] public byte rightStickX;
}