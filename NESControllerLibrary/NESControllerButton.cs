namespace NESControllerLibrary;

public enum  NESControllerButton : ulong
{

    //For easy reference
    //I have added comments with the bits in byte 3, 4, 5 and 6 (zero indexed)
    //of the 8 bytes that are sent from the NES controller in each event

    Select, // 0 1 1 1 1 1 1 1   0 1 1 1 1 1 1 1   0 0 0 0 1 1 1 1  0 0 0[1]0 0 0 0  
    Start,  // 0 1 1 1 1 1 1 1   0 1 1 1 1 1 1 1   0 0 0 0 1 1 1 1  0 0[1]0 0 0 0 0  
    B,      // 0 1 1 1 1 1 1 1   0 1 1 1 1 1 1 1   0 0 0[1]1 1 1 1  0 0 0 0 0 0 0 0  
    A,      // 0 1 1 1 1 1 1 1   0 1 1 1 1 1 1 1   0 0[1]0 1 1 1 1  0 0 0 0 0 0 0 0  
    Right,  //[1]1 1 1 1 1 1 1   0 1 1 1 1 1 1 1   0 0 0 0 1 1 1 1  0 0 0 0 0 0 0 0  
    Up,     // 0 1 1 1 1 1 1 1   0[0 0 0 0 0 0[0]  0 0 0 0 1 1 1 1  0 0 0 0 0 0 0 0  
    Down,   // 0 1 1 1 1 1 1 1  [1]1 1 1 1 1 1 1   0 0 0 0 1 1 1 1  0 0 0 0 0 0 0 0  
    Left,   // 0[0 0 0 0 0 0 0]  0 1 1 1 1 1 1 1   0 0 0 0 1 1 1 1  0 0 0 0 0 0 0 0  
};
