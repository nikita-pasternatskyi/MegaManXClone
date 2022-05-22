using System;

namespace MP.Player
{
    [Flags]
    public enum Vector2Axis
    {
        None = 0,
        X = 1,
        Y = 2,
        XY = X | Y
    }
}