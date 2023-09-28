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
            _player._rigidbody2D.velocity = new Vector2(0f, 0f);
        }

        public override void ExitState()
        {
        }

        public override void PhysicsUpdate()
        {
        
        }

        public override void FrameUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Z))
                _playerStateMachine.ChangeState(_player.JumpingState);
            if (Input.GetAxis("Horizontal") != 0f)
                _player.PlayerStateMachine.ChangeState(_player.RunningState);
            if (Input.GetKeyDown(KeyCode.A))
                _player.PlayerStateMachine.ChangeState(_player.AttackingState);
        }
    }
}