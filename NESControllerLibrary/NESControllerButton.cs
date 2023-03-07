namespace NESControllerLibrary;

public enum  NESControllerButton : ulong
{

    //For easy reference
    //I have added comments with the bits in the 7th and 8th bytes
    //of the 9 bytes that are sent from the NES controller in each event

    Select = 16,                    //0 0 0 0 0 0 0 0  0 0 0 1 0 0 0 0
    Start = 32,                     //0 0 0 0 0 0 0 0  0 0 1 0 0 0 0 0
    B = 16 * 255,                   //0 0 0 0 0 1 0 0  0 0 0 0 0 0 0 0
    A = 32 * 255,                   //0 0 0 0 1 0 0 0  0 0 0 0 0 0 0 0
    Right = 128 * 255 * 255 * 255,  //0 0 0 0 0 0 0 0  1 0 0 0 0 0 0 0
    Up = 255 * 255,                 //0 0 0 0 0 0 0 0  0 1 0 0 0 0 0 0
    Down = 128 * 255 * 255,         //0 0 0 0 0 0 0 0  0 0 1 0 0 0 0 0
    Left = 255 * 255 * 255,         //0 0 0 0 0 0 0 0  0 0 0 1 0 0 0 0
};
