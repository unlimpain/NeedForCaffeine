using System.Threading.Tasks;
using UnityEngine;

namespace Enemies.StateMachine.ConcreteStates
{
    public class EnemyAttackingState : EnemyState
    {
        protected Transform PlayerTransform;
        protected int ReloadTimeInMilliseconds;

        public EnemyAttackingState(Enemy enemy, EnemyStateMachine enemyStateMachine, Transform playerTransform, 
            int reloadTimeInMilliseconds) : base(enemy, enemyStateMachine)
        {
            PlayerTransform = playerTransform;
            ReloadTimeInMilliseconds = reloadTimeInMilliseconds;
        }
        
        public override void EnterState()
        {
            Enemy.Attack(PlayerTransform);
            AttackToPreviousState();
        }

        public override void ExitState()
        {
            
        }

        public override void PhysicsUpdate()
        {
            
        }

        public override void FrameUpdate()
        {
            
        }
        
        private async Task AttackToPreviousState()
        {
            await Task.Delay(ReloadTimeInMilliseconds);
            Enemy.StateMachine.ReturnToPreviousState();
        }
    }
}