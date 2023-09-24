﻿using UnityEngine;

namespace PlayerScripts.StateMachine.ConcreteStates
{
    public class RunningState : PlayerState
    {
        public RunningState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
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
            if (Input.GetAxis("Horizontal") == 0f)
                _player.StateMachine.ChangeState(_player.IdleState);
            if (Input.GetButtonDown("Fire1"))
                _player.StateMachine.ChangeState(_player.AttackingState);
            if (Input.GetKey(KeyCode.Z))
                _playerStateMachine.ChangeState(_player.JumpingState);
        }
    }
}