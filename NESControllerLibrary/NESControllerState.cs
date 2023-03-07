namespace NESControllerLibrary;
public struct NESControllerState
{
    #region Properties
    public bool Start { get; set; }
    public bool Select { get; set; }
    public bool A { get; set; }
    public bool B { get; set; }
    public bool Up { get; set; }
    public bool Left { get; set; }
    public bool Down { get; set; }
    public bool Right { get; set; }

    public bool NothingPressed { get { return !(Start || Select || A|| B || Up || Left || Down || Right); } }
    #endregion

    public override string ToString()
    {
        return $"DanceMatState: [Up:{Up},Down:{Down},Left:{Left},Right:{Right},A:{A},B:{B},Select:{Select},Start:{Start}]";
    }
}