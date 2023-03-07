namespace NESControllerLibrary
{
    public interface INESController
    {
        event EventHandler<NESControllerEventArgs>? ButtonStateChanged;

        NESControllerState GetCurrentState();
    }
}