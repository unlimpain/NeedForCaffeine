namespace Enemies.StateMachine
{
    public class EnemyState
    {
        protected Enemy _enemy;
        protected EnemyStateMachine _playerStateMachine;

        public EnemyState(Enemy enemy, EnemyStateMachine enemyStateMachine)
        {
            _enemy = enemy;
            _playerStateMachine = enemyStateMachine;
        }
        public virtual void EnterState(){}
        public virtual void ExitState(){}
        public virtual void PhysicsUpdate(){}
        public virtual void FrameUpdate(){}
    }
}
