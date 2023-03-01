using System;
using UnityEngine;

namespace Code.UserInput
{
    public interface IUserInputButtonProxy
    {
        event Action<bool> OnButtonDown;
        event Action<bool> OnButtonHold;
        event Action<bool> OnButtonUp;
        event Action<Vector3> OnChangeMousePosition;

        void GetButtonDown();
        void GetButtonHold();
        void GetButtonUp();
    }
}
