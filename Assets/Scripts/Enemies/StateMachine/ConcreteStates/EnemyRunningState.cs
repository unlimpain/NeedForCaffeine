using Unity.VisualScripting;
using UnityEngine;

namespace Enemies.StateMachine.ConcreteStates
{
    public class EnemyRunningState : EnemyState
    {
        private Rigidbody2D _rigidbody2D;
        private Transform _lastFrameTransform;
        
        public EnemyRunningState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
        {
            _enemy = enemy;
            _playerStateMachine = enemyStateMachine;
        }
        
        public override void EnterState()
        {
            base.EnterState();
            _rigidbody2D = _enemy.Rigidbody2D;
            _lastFrameTransform = _enemy.transform;
        }

        public override void ExitState()
        {
            base.ExitState();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            // _enemy.Rigidbody2D.position += new Vector2(_enemy.runningSpeed, 0f);
            _enemy.Move(); 
        }

        public override void FrameUpdate()
        {
            base.FrameUpdate();
            if (_enemy.transform.position.x<_lastFrameTransform.position.x)
                _enemy.IsFacedRight = false;
            else if (_enemy.transform.position.x > _lastFrameTransform.position.x)
                _enemy.IsFacedRight = true;
            _lastFrameTransform = _enemy.transform;
            
            _enemy.Sprite.flipX = _enemy.IsFacedRight;
        }
    }
}
