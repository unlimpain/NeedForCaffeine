using System;
using UnityEngine;

namespace PlayerScripts.StateMachine.ConcreteStates
{
    public class FallingState : PlayerState
    {
        private float _gravityScale;
        public FallingState(Player player, PlayerStateMachine playerStateMachine, float gravityScale) : base(player, playerStateMachine)
        {
            _gravityScale = gravityScale;
        }
        public override void EnterState()
        {
            _player.IsGrounded = false;
        }

        public override void ExitState()
        {
        }

        public override void PhysicsUpdate()
        {
            _player.Run();
            _player._rigidbody2D.velocity += new Vector2(0f ,_gravityScale*Time.fixedDeltaTime);
        }

        public override void FrameUpdate()
        {
            if (_player.IsGrounded && Math.Abs(Input.GetAxis("Horizontal")) > 0f)
                _player.PlayerStateMachine.ChangeState(_player.RunningState);
            if (Input.GetKeyDown(KeyCode.A))
                _player.PlayerStateMachine.ChangeState(_player.AttackingState);
            if (_player.IsGrounded)
                _player.PlayerStateMachine.ChangeState(_player.IdleState);
        }
    }
}