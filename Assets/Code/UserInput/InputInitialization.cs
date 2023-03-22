 using Code.Configs;

 namespace Code.UserInput
{
    public sealed class InputInitialization
    {
        private readonly IUserInputProxy _inputHorizontal;
        private readonly IUserInputProxy _inputVertical;
        private readonly IUserInputProxy _inputMouseScrollWheel;
        private readonly IUserInputButtonProxy _inputMouseLeft;
        private readonly IUserInputButtonProxy _inputMouseRight;


        public InputInitialization(InputConfig config)
        {
            _inputHorizontal = new AxisInput(config.Horizontal);
            _inputVertical = new AxisInput(config.Vertical);
            _inputMouseLeft  = new ButtonInput(config.MouseLeftInput);
            _inputMouseRight  = new ButtonInput(config.MouseRightInput);
            _inputMouseScrollWheel = new AxisInput(config.MouseScrollWheel);
        }

        public IUserInputProxy InputHorizontal => _inputHorizontal;
        public IUserInputProxy InputVertical => _inputVertical;
        public IUserInputButtonProxy InputMouseLeft => _inputMouseLeft;
        public IUserInputButtonProxy InputMouseRight => _inputMouseRight;
        public IUserInputProxy InputMouseScrollWheel => _inputMouseScrollWheel;
    }
}
