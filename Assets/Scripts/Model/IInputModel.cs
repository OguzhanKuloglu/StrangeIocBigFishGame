
using Assets.Scripts.Data.Vo;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public interface IInputModel
    {
        RD_InputData InputData { get; set; }

        void SetInput(Vector2 value);
        void SetInputDegree(float value);
        Vector2 GetInput();
        float GetInputDegree();
        void Reset();

    }   
}
