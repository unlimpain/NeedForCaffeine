using UnityEngine;

namespace Enemies.StateMachine.ConcreteStates
{
    public class EnemyRunningState : EnemyState
    {
        protected Transform _lastFrameTransform;
        protected Transform _playerTransform;

        public EnemyRunningState(Enemy enemy, EnemyStateMachine enemyStateMachine, Transform playerTransform) : base(enemy, enemyStateMachine)
        {
            _playerTransform = playerTransform;
        }
        
        public override void EnterState()
        {
            _lastFrameTransform = Enemy.transform;
        }

        public override void ExitState()
        {
            
        }

        public override void PhysicsUpdate()
        {
            Enemy.Move(); 
        }

        public override void FrameUpdate()
        {
            if (Enemy.transform.position.x<_lastFrameTransform.position.x)
                Enemy.IsFacedRight = false;
            else if (Enemy.transform.position.x > _lastFrameTransform.position.x)
                Enemy.IsFacedRight = true;
            _lastFrameTransform = Enemy.transform;
            
            Enemy.Sprite.flipX = Enemy.IsFacedRight;
            
            //TODO: toIdleStateChange
            
            if (Vector2.Distance(_playerTransform.position, Enemy.transform.position) < Enemy.PlayerFindingDistance)
            {
                EnemyStateMachine.ChangeState(Enemy.AttackingState);
            }
        }
    }
}
