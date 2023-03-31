using System;
using UnityEngine;


namespace Code.UserInput
{
    internal sealed class ButtonInput : IUserInputButtonProxy
    {
        public event Action<bool> OnButtonDown = delegate(bool b) { };
        public event Action<bool> OnButtonHold = delegate(bool b) { };
        public event Action<bool> OnButtonUp = delegate(bool b) { };
        public event Action<Vector3> OnChangeMousePosition = delegate(Vector3 vector) { };

        private readonly string _button;

        public ButtonInput(string button)
        {
            _button = button;
        }

        public void GetButtonDown()
        {
            OnButtonDown?.Invoke(Input.GetButtonDown(_button));
            OnChangeMousePosition?.Invoke(Input.mousePosition);
        }

        public void GetButtonHold()
        {
            OnButtonHold?.Invoke(Input.GetButton(_button));
            OnChangeMousePosition?.Invoke(Input.mousePosition);
        }

        public void GetButtonUp()
        {
            OnButtonUp?.Invoke(Input.GetButtonUp(_button));
            OnChangeMousePosition?.Invoke(Input.mousePosition);
        }
    }
}
