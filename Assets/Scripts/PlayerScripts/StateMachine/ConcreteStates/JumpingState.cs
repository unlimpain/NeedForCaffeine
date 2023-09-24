using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor.Timeline.Actions;
using UnityEngine;

namespace PlayerScripts.StateMachine.ConcreteStates
{
    public class JumpingState : PlayerState
    {
        private int _jumpDelayMilliseconds = 100;
        public JumpingState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
        
        }
        public override void EnterState()
        {
            _player.StateMachine.ChangeState(_player.FallingState);
        }

        public override void ExitState()
        {
            _player.Jump();
            _player.IsGrounded = false;
        }

        public override void PhysicsUpdate()
        {
        
        }

        public override void FrameUpdate()
        {
            
        }

        // private async Task JumpToFloatingState()
        // {
        //     await Task.Delay(_jumpDelayMilliseconds);
        //     _player.StateMachine.ChangeState(_player.FallingState);
        // }
    }
}