using Godot;
using System;

namespace MP.Player
{
    public class Player2D : KinematicBody2D
    {
        [Export] public bool UseGravity;
        [Export] public float Gravity;
        [Export] private float _maxSlopeAngle;
        [Export] private float _minimumYVelocity;
        [Export] private Vector2 _groundCheckRayDirection;
        [Export] private bool _facingDirection = true;

        public Vector2 Velocity;
        public bool OnSlope { get; private set; }
        public bool Grounded { get; private set; }
        public Vector2 GroundNormal { get; private set; }

        private bool _requestedJump;

        public bool IsFacingOrigin { get; private set; }
        private Vector2 _snapVector;
        private RayCast2D _rayCast;

        public override void _Ready()
        {
            IsFacingOrigin = true;
            _rayCast = new RayCast2D();
            AddChild(_rayCast);
            _rayCast.Enabled = true;
            _rayCast.CastTo = _groundCheckRayDirection;
            _rayCast.ExcludeParent = true;
        }

        public override void _PhysicsProcess(float delta)
        {
            CollectGroundInfo();
            Grounded = OnFloor();

            Velocity.y += Gravity * delta * Convert.ToByte(UseGravity);

            ClampYVelocity();

            if (Velocity.y < 0 && _requestedJump == true)
            {
                _snapVector = Vector2.Zero;
                _requestedJump = false;
            }
            else
            {
                _snapVector = Vector2.Down * 25;
            }

            Velocity.y = MoveAndSlideWithSnap(Velocity, _snapVector, Vector2.Up, true, 4, Mathf.Deg2Rad(_maxSlopeAngle)).y;
        }

        public float GetDirection()
        {
            if (_facingDirection == false) //if our original direction was left
            {
                if (IsFacingOrigin == true) //if we are already facing it
                    return -1;
            }
            if (IsFacingOrigin == true) //we faced right and now do
                return 1;
            return -1;
        }

        public bool Aligned(float x)
        {
            if (x == 0)
                return true;

            if (x < 0) //face left
            {
                if (_facingDirection == false) //if our original direction was left
                {
                    if (IsFacingOrigin == true) //if we are already facing it
                        return true;
                    return false;
                }

                if (IsFacingOrigin == true)//if our original direction was RIGHT and we are facing RIGHT
                {
                    return false;
                }

                return true;
            }

            else if (x > 0) //right
            {
                if (_facingDirection == false) //if our original direction was left
                {
                    if (IsFacingOrigin == true) //if we are already facing it
                        return false;
                    return true; ;
                }

                if (IsFacingOrigin == false)//if our original direction was RIGHT and we are facing LEFT
                {
                    return false;
                }
                return true;
            }

            return false;
        }

        public void FaceDirectionX(float x)
        {
            if (Aligned(x) == false)
                Flip();
        }

        public void Flip()
        {
            IsFacingOrigin = !IsFacingOrigin;
            var newScale = Scale;
            newScale.x *= -1;
            Scale = newScale;
        }

        public void RequestJump()
        {
            _requestedJump = true;
        }

        private void CollectGroundInfo()
        {
            if (_rayCast.IsColliding() == true)
            {
                GroundNormal = _rayCast.GetCollisionNormal();
                OnSlope = GroundNormal.x != 0;
                return;
            }
            GroundNormal = Vector2.Up;
            OnSlope = false;
        }

        private bool OnFloor()
        {
            return IsOnFloor();
            var v = Vector2.Zero;
            v.y = 5f;
            return TestMove(Transform, v, false);
        }

        public void AddForce(Vector2 force)
        {
            if (force.y < 0)
                RequestJump();
            Velocity += force;
        }

        public void AddForceX(Vector2 force)
        {
            Velocity.x += force.x;
        }

        public void AddForceY(Vector2 force)
        {
            if (force.y < 0)
                RequestJump();
            Velocity.y += force.y;
        }

        public void AddForceX(float x)
        {
            Velocity.x += x;
        }

        public void AddForceY(float y)
        {
            if (y < 0)
                RequestJump();
            Velocity.y += y;
        }

        private void ClampYVelocity()
        {
            Velocity.y = Mathf.Clamp(Velocity.y, float.MinValue, _minimumYVelocity);
        }
    }
}