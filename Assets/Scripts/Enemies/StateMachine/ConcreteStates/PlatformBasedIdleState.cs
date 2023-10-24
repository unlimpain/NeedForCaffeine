using System.Threading.Tasks;

namespace Enemies.StateMachine.ConcreteStates
{
    public class PlatformBasedIdleState : EnemyIdleState
    {
        private int _stayTimeInMilliseconds;
        public PlatformBasedIdleState(Enemy enemy, EnemyStateMachine enemyStateMachine, int stayTimeInMilliseconds) : base(enemy, enemyStateMachine)
        {
            _stayTimeInMilliseconds = stayTimeInMilliseconds;
        }
        public override void EnterState()
        {
            IdleToRunningState();
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
        
        private async Task IdleToRunningState()
        {
            await Task.Delay(_stayTimeInMilliseconds);
            Enemy.StateMachine.ReturnToPreviousState();
        }
    }
}