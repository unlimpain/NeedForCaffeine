using System.Threading.Tasks;
using UnityEngine;

namespace Enemies.StateMachine.ConcreteStates
{
    public class EnemyAttackingState : EnemyState
    {
        private Transform _playerTransform;
        private int _reloadTimeInMilliseconds = 1000;
        private float _playerFindingDistance = 5f;
        public EnemyAttackingState(Enemy enemy, EnemyStateMachine enemyStateMachine, Transform playerTransform) : base(enemy, enemyStateMachine)
        {
            _playerTransform = playerTransform;
        }
        
        public override void EnterState()
        {
            Enemy.Attack();
            JumpToFloatingState();
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
        
        private async Task JumpToFloatingState()
        {
            await Task.Delay(_reloadTimeInMilliseconds);
            Enemy.StateMachine.ChangeState(Enemy.RunningState);
        }
    }
}