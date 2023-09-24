using System;
using UnityEngine;

namespace PlayerScripts.StateMachine.ConcreteStates
{
    public class FallingState : PlayerState
    {
        public FallingState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
        
        }
        public override void EnterState()
        {
        
        }

        public override void ExitState()
        {
        }

        public override void PhysicsUpdate()
        {
            _player.Run();
        }

        public override void FrameUpdate()
        {
            if (_player.IsGrounded && Math.Abs(Input.GetAxis("Horizontal")) > 0f)
                _player.StateMachine.ChangeState(_player.RunningState);
            if (Input.GetButtonDown("Fire1"))
                _player.StateMachine.ChangeState(_player.AttackingState);
            if (_player.IsGrounded)
                _player.StateMachine.ChangeState(_player.IdleState);
            }
    }
}