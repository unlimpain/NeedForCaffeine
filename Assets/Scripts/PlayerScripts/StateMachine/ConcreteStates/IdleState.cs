using UnityEngine;

namespace PlayerScripts.StateMachine.ConcreteStates
{
    public class IdleState : PlayerState
    {
        public IdleState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
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
        
        }

        public override void FrameUpdate()
        {
            if (Input.GetKey(KeyCode.Z))
                _playerStateMachine.ChangeState(_player.JumpingState);
            if (Input.GetAxis("Horizontal") != 0f)
                _player.StateMachine.ChangeState(_player.RunningState);
            if (Input.GetButtonDown("Fire1"))
                _player.StateMachine.ChangeState(_player.AttackingState);
        }
    }
}