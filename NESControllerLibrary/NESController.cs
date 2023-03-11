using HIDInterface;

namespace NESControllerLibrary;

public class NESController : NESControllerBase, IDisposable
{

    private byte[] _lastReadData = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
    HIDDevice _device;

    //You must change these (VID/PID) to your type of Dance Mat,
    //in case you can't connect using this. Then your dance mat may be another brand.
    //Look in Device manager, find your "HID-Compliant game controller" and look in
    //properties > details > property: hardware ID
    private const int DEVICE_VENDOR_ID = 0x0810;
    private const int DEVICE_PRODUCT_ID = 0xE501;

    private static readonly Dictionary<NESControllerButton, Func<byte[], bool>> ButtonActivationRules = new() {
        {NESControllerButton.Select, (byte[] input) => (input[3] & 16) > 0 },
        {NESControllerButton.Start, (byte[] input) => (input[3] & 32) > 0 },
        {NESControllerButton.A, (byte[] input) => (input[2] & 32) > 0 },
        {NESControllerButton.B, (byte[] input) => (input[2] & 16) > 0 },
        {NESControllerButton.Up, (byte[] input) => (input[1] & 127) == 0 },
        {NESControllerButton.Down, (byte[] input) => (input[1] & 128) > 0 },
        {NESControllerButton.Left, (byte[] input) => (input[0] &127) == 0 },
        {NESControllerButton.Right, (byte[] input) => (input[0] & 128) > 0 },
    };

    public NESController()
    {


        HIDDevice.interfaceDetails[] devices = HIDDevice.getConnectedDevices();

        //Select a device from the available devices (uses the Vendor ID and Product ID of the Dance Mat controller).
        var dev = devices.Where(dev => dev.VID == DEVICE_VENDOR_ID
            && dev.PID == DEVICE_PRODUCT_ID).FirstOrDefault();

        if (dev.VID == 0) { throw new Exception("No Dance Mat controller detected. Do you need to change the driver to 'HID-Compliant game controller' in Device Manager?"); }

        //register device, and set it up for publishing events when new data comes in
        _device = new HIDDevice(dev.devicePath, true);

        //subscribe to data received event
        _device.dataReceived += _device_dataReceived;
    }

    private void _device_dataReceived(byte[] messageInput)
    {
        //Console.Write(message.Length + " : ");
        //foreach (var byteData in message)
        //{
        //    Console.Write(byteData + ",");
        //}
        //Console.WriteLine();
        byte[] message = (byte[])messageInput.Clone();
        try
        {
            //Only look for the "button action" messages (status of controller buttons),
            // which are 8 bytes in length
            if (message.Length != 8) { return; }

            var lastBitArray = new byte[] { _lastReadData[3], _lastReadData[4], _lastReadData[5], _lastReadData[6] };
            var currentBitArray = new byte[] { message[3], message[4], message[5], message[6] };


            foreach (NESControllerButton button in ButtonActivationRules.Keys)
            {
                var action = GetActionFromBitChange(
                    ButtonActivationRules[button](lastBitArray),
                    ButtonActivationRules[button](currentBitArray));
                
                //Console.WriteLine(Convert.ToString((long)BitConverter.ToUInt32(currentBitArray), 2).PadLeft(32, '0'));
                //Console.WriteLine($"Previous:{Convert.ToString(lastBitArray[0], 2).PadLeft(8, '0')} {Convert.ToString(lastBitArray[1], 2).PadLeft(8, '0')} {Convert.ToString(lastBitArray[2], 2).PadLeft(8, '0')} {Convert.ToString(lastBitArray[3], 2).PadLeft(8, '0')}");
                //Console.WriteLine($"Current :{Convert.ToString(currentBitArray[0], 2).PadLeft(8, '0')} {Convert.ToString(currentBitArray[1], 2).PadLeft(8, '0')} {Convert.ToString(currentBitArray[2], 2).PadLeft(8, '0')} {Convert.ToString(currentBitArray[3], 2).PadLeft(8, '0')}");
                //Console.WriteLine("Action: " + action);
                //Console.WriteLine();
                if (action != NESControllerButtonAction.Unchanged)
                {
                   
                    OnButtonStateChanged(button, action);
                }

            }
            _lastReadData = message.ToArray();

        }
        catch (Exception ex) { throw new Exception($"Error while receiving data from controller. Maybe it was disconnected?. Error was: '{ex.Message}'", ex); }

    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) => _device.close();

    private NESControllerButtonAction GetActionFromBitChange(bool previous, bool current)
    {
        if (previous == current) { return NESControllerButtonAction.Unchanged; }
        if (previous) { return NESControllerButtonAction.Released; }
        else { return NESControllerButtonAction.Pressed; }
    }
}
