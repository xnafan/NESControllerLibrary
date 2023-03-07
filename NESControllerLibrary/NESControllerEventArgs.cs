namespace NESControllerLibrary;

public class NESControllerEventArgs : EventArgs
{
    public NESControllerButton Button { get; set; }
    public NESControllerButtonAction Action { get; set; }

    public NESControllerEventArgs(NESControllerButton button, NESControllerButtonAction action)
    {
        Button = button;
        Action = action;
    }
    public override string ToString()
    {
        return $"{Button} {Action}";
    }
}