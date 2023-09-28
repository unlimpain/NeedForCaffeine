namespace Enemies.StateMachine
{
    public class EnemyState
    {
        protected Enemy Enemy;
        protected EnemyStateMachine EnemyStateMachine;

        protected EnemyState(Enemy enemy, EnemyStateMachine enemyStateMachine)
        {
            Enemy = enemy;
            EnemyStateMachine = enemyStateMachine;
        }
        public virtual void EnterState(){}
        public virtual void ExitState(){}
        public virtual void PhysicsUpdate(){}
        public virtual void FrameUpdate(){}
    }
}
