using System.Collections.Generic;

public static class InputBindings
{
    public const string MOVE_FORWARD = "move_forward";
    public const string MOVE_BACK = "move_backward";
    public const string MOVE_RIGHT = "move_right";
    public const string MOVE_LEFT = "move_left";
    public const string JUMP = "jump";
    public const string FULLSCREEN = "fullscreen";

    public static Dictionary<Buttons, string> Bindings = new Dictionary<Buttons, string>
    {
        { Buttons.MOVE_FORWARD, MOVE_FORWARD},
        { Buttons.MOVE_BACK, MOVE_BACK},
        { Buttons.MOVE_RIGHT, MOVE_RIGHT},
        { Buttons.MOVE_LEFT, MOVE_LEFT},
        { Buttons.JUMP, JUMP},
    };
}