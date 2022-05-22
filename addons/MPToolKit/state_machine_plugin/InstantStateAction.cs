using Godot;

public abstract class InstantStateAction : StateAction
{
    [Export] private bool _onEnter;
    [Export] private bool _onExit;

    public override bool OnEnter => _onEnter;
    public override bool OnExit => _onExit;
}

