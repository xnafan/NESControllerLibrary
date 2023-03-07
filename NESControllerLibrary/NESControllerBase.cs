namespace NESControllerLibrary;
public abstract class NESControllerBase : INESController
{
    #region variables and properties
    protected Dictionary<NESControllerButton, bool> _buttonStates = new();
    public event EventHandler<NESControllerEventArgs>? ButtonStateChanged;
    #endregion

    public NESControllerBase()
    {
        //initialize the button states dictionary with all buttons
        Enum.GetValues<NESControllerButton>().ToList().ForEach(button => _buttonStates.Add(button, false));
    }

    #region Public methods
    public NESControllerState GetCurrentState()
    {
        return new NESControllerState()
        {
            A = _buttonStates[NESControllerButton.A],
            B = _buttonStates[NESControllerButton.B],
            Start = _buttonStates[NESControllerButton.Start],
            Select = _buttonStates[NESControllerButton.Select],
            Up = _buttonStates[NESControllerButton.Up],
            Down = _buttonStates[NESControllerButton.Down],
            Left = _buttonStates[NESControllerButton.Left],
            Right = _buttonStates[NESControllerButton.Right]
        };
    }
    #endregion

    #region Events
    protected void OnButtonStateChanged(NESControllerButton button, NESControllerButtonAction action)
    {
        _buttonStates[button] = action == NESControllerButtonAction.Pressed;
        ButtonStateChanged?.Invoke(this, new NESControllerEventArgs(button, action));
    }
    #endregion
}