using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace ConcreteStates
{
    public class IdleState : EnemyState
    {
        public IdleState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
        {
        
        }
        public override void EnterState()
        {
        
        }

        public override void ExitState()
        {
        }

        public override void PhycicsUpdate()
        {
        
        }

        public override void FrameUpdate()
        {
        }
    }
}