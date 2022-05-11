using Godot;
using MP.InputWrapper;

namespace XClone
{

    public class XInput : PlayerInput2D
    {
        public bool ShootRequested => Input.IsActionJustPressed(InputBindings.XBUSTER);
        public bool DashRequested => Input.IsActionJustPressed(InputBindings.DASH);
    }
}