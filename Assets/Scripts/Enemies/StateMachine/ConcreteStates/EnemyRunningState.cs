using UnityEngine;

namespace Enemies.StateMachine.ConcreteStates
{
    public class EnemyRunningState : EnemyState
    {
        private Rigidbody2D _rigidbody2D;
        private Transform _lastFrameTransform;
        private Transform _playerTransform;
        private float _playerFindingDistance = 7f; // TODO: change this
        
        public EnemyRunningState(Enemy enemy, EnemyStateMachine enemyStateMachine, Transform playerTransform) : base(enemy, enemyStateMachine)
        {
            _playerTransform = playerTransform;
        }
        
        public override void EnterState()
        {
            _rigidbody2D = Enemy.Rigidbody2D;
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
            base.FrameUpdate();
            if (Enemy.transform.position.x<_lastFrameTransform.position.x)
                Enemy.IsFacedRight = false;
            else if (Enemy.transform.position.x > _lastFrameTransform.position.x)
                Enemy.IsFacedRight = true;
            _lastFrameTransform = Enemy.transform;
            
            Enemy.Sprite.flipX = Enemy.IsFacedRight;
            
            
            if (Vector2.Distance(_playerTransform.position, Enemy.transform.position) < _playerFindingDistance)
            {
                EnemyStateMachine.ChangeState(Enemy.AttackingState);
            }
        }
    }
}
