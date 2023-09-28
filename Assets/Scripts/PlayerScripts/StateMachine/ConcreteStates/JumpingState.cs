using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor.Timeline.Actions;
using UnityEngine;

namespace PlayerScripts.StateMachine.ConcreteStates
{
    public class JumpingState : PlayerState
    {
        public JumpingState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
        
        }
        public override void EnterState()
        {
            _player.PlayerStateMachine.ChangeState(_player.FallingState);
        }

        public override void ExitState()
        {
            _player.Jump();
        }

        public override void PhysicsUpdate()
        {
        
        }

        public override void FrameUpdate()
        {
            
        }
    }
}