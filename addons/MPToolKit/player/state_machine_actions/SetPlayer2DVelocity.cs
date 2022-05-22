using Godot;
using MP.Extensions;
using MP.FiniteStateMachine;
using MP.Player;
using System;

namespace MP.Player
{
    public class SetPlayer2DVelocity : InstantStateAction
    {
        [Export] private Vector2 _targetVelocity;

        [ExportFlagsEnum(typeof(Vector2Axis))] private int _axisToKeep;

        private Player2D _player;
        private Func<Vector2> GetVelocity;

        public override void Init(StateMachine stateMachine)
        {
            _player = stateMachine.GetCachedNode<Player2D>();

            var axis = (Vector2Axis)_axisToKeep;

            switch(axis)
            {
                case Vector2Axis.X:
                    GetVelocity = () => new Vector2(_player.Velocity.x, _targetVelocity.y);
                    break;
                case Vector2Axis.Y:
                    GetVelocity = () => new Vector2(_targetVelocity.x, _player.Velocity.y);
                    break;
                case Vector2Axis.XY:
                    GD.PrintErr("Picked XY to keep axises!");
                    break;
                case Vector2Axis.None:
                    GetVelocity = () => _targetVelocity;
                    break;
            }
        }

        public override void Act(float delta)
        {
            _player.Velocity = GetVelocity();
        }
    }
}