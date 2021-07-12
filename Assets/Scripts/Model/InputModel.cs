using UnityEngine;

namespace Assets.Scripts.Model
{
 
    public class InputModel : IInputModel
    {
        private RD_InputData _inputData;

        public RD_InputData InputData
        {
            get
            {
                if (_inputData == null)
                    OnPostConstruct();
                return _inputData;
            }
            set { }
        }

       

        [PostConstruct]
        public void OnPostConstruct()
        {
            _inputData = Resources.Load<RD_InputData>("Data/InputData");
        }

        #region Func

        public void SetInput(Vector2 value)
        {
            _inputData.Joystick = value;
        }

        public Vector2 GetInput()
        {
            return _inputData.Joystick;
        }

        public void SetInputDegree(float value)
        {
            _inputData.JoystickDegree = value;
        }

        public float GetInputDegree()
        {
            return _inputData.JoystickDegree;
        }

        public void Reset()
        {
            _inputData.Joystick = Vector2.zero;
            _inputData.JoystickDegree = 0;
        }

        #endregion
    }   
}
