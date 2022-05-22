using Godot;
using MP.Extensions;
using MP.FiniteStateMachine;

namespace MP.Player
{
    public class ComparePlayer2DVelocityCondition : Condition
    {
        [ExportFlagsEnum(typeof(Vector2Axis))] int _axis;
        [Export] private ComparisonEnum _comparisonType;
        [Export] private float _value;
        private Player2D _player;

        public override void Init(StateMachine baseStateMachine)
        {
            _player = baseStateMachine.GetCachedNode<Player2D>();
        }

        protected override bool ConditionCheck()
        {
            var velocity = _player.Velocity;

            switch (_comparisonType)
            {
                case ComparisonEnum.More:
                    var x = velocity.x > _value;
                    var y = velocity.y > _value;
                    return CheckValue(x, y);
                case ComparisonEnum.Less:
                    x = velocity.x < _value;
                    y = velocity.y < _value;
                    return  CheckValue(x, y);
                case ComparisonEnum.Equals:
                    x = velocity.x == _value;
                    y = velocity.y == _value;
                    return CheckValue(x, y);
                case ComparisonEnum.LessEquals:
                    x = velocity.x <= _value;
                    y = velocity.y <= _value;
                    return CheckValue(x, y);
                case ComparisonEnum.MoreEquals:
                    x = velocity.x >= _value;
                    y = velocity.y >= _value;
                    return CheckValue(x, y);
            }
            return false;
        }

        private bool CheckValue(bool x, bool y)
        {
            Vector2Axis myAxis = (Vector2Axis)_axis;
            switch (myAxis)
            {
                case Vector2Axis.None:
                    GD.PrintErr("Not comparing");
                    break;
                case Vector2Axis.X:
                    return x;
                case Vector2Axis.Y:
                    return y;
                case Vector2Axis.XY:
                    return x == y;
            }
            return false;
        }
    }
}