namespace Enemies.StateMachine.ConcreteStates
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

        public override void PhysicsUpdate()
        {
            
        }

        public override void FrameUpdate()
        {
        }
    }
}