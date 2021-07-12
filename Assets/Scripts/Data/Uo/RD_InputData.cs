using Assets.Scripts.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "RoofGames/Data/Runtime/InputData",order = 2)]
public class RD_InputData : SerializedScriptableObject
{
    public Vector2 Joystick;
    public float JoystickDegree;
}

