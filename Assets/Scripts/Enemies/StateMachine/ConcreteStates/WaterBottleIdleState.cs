using System.Threading.Tasks;

namespace Enemies.StateMachine.ConcreteStates
{
    public class WaterBottleIdleState : EnemyIdleState
    {
        private int _stayTimeInMilliseconds;
        public WaterBottleIdleState(Enemy enemy, EnemyStateMachine enemyStateMachine, int stayTimeInMilliseconds) : base(enemy, enemyStateMachine)
        {
            _stayTimeInMilliseconds = stayTimeInMilliseconds;
        }
        public override void EnterState()
        {
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
            await Task.Delay(_stayTimeInMilliseconds);
            Enemy.StateMachine.ReturnToPreviousState();
        }
    }
}