using System;
using System.Threading.Tasks;

namespace PlayerScripts.StateMachine.ConcreteStates
{
    public class AttackingState : PlayerState
    {
        public AttackingState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
        {
        
        }
        public override void EnterState()
        {
            AttackToIdleState();
            _player.Attack();
        }

        public override void ExitState()
        {

        }

        public override void PhysicsUpdate()
        {
            _player.Run();
        }

        public override void FrameUpdate()
        {
            
        }
        private async Task AttackToIdleState()
        {
            await Task.Delay(TimeSpan.FromSeconds(_player.PlayerAttackTime));
            _player.PlayerStateMachine.ChangeState(_player.FallingState);
        }
    }
}
