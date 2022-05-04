using Godot;
using MP.PlayerKinematicBody;
using MP.StateMachine;

public abstract class MoveAction2D : StateAction
{
    [Export] private float _speed;
    protected PlayerBody2D KinematicBody { get; private set; }

    public override void Init(BaseStateMachine stateMachine)
    {
        KinematicBody = stateMachine.GetNodeOfType<PlayerBody2D>();
        OnInit(stateMachine);
    }

    public sealed override void Act(float delta)
    {
        var velocity = GetInput();
        velocity *= _speed;
        KinematicBody.SetVelocity(velocity);
    }

    protected virtual void OnInit(BaseStateMachine baseStateMachine) { }

    protected abstract Vector2 GetInput();
}

