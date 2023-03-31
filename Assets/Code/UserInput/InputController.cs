using Code.Interfaces;


namespace Code.UserInput
{
    internal sealed class InputController : IExecute
    {
        private readonly IUserInputProxy _inputHorizontal;
        private readonly IUserInputProxy _inputVertical;
        private readonly IUserInputProxy _inputMouseScrollWheel;
        private readonly IUserInputButtonProxy _inputMouseLeft;
        private readonly IUserInputButtonProxy _inputMouseRight;

        public InputController(InputInitialization input)
        {
            _inputHorizontal = input.InputHorizontal;
            _inputVertical = input.InputVertical;
            _inputMouseLeft = input.InputMouseLeft;
            _inputMouseRight = input.InputMouseRight;
            _inputMouseScrollWheel = input.InputMouseScrollWheel;
        }

        public void Execute()
        {
            _inputHorizontal.GetAxis();
            _inputVertical.GetAxis();
            _inputMouseScrollWheel.GetAxis();
            _inputMouseLeft.GetButtonDown();
            _inputMouseLeft.GetButtonHold();
            _inputMouseLeft.GetButtonUp();
            _inputMouseRight.GetButtonDown();
            _inputMouseRight.GetButtonHold();
            _inputMouseRight.GetButtonUp();
        }
    }
}
