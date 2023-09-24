

namespace PlayerScripts.StateMachine
{
    public class PlayerState
    {
        protected Player _player;
        protected PlayerStateMachine _playerStateMachine;

        public PlayerState(Player player, PlayerStateMachine playerStateMachine)
        {
            _player = player;
            _playerStateMachine = playerStateMachine;
        }
        public virtual void EnterState(){}
        public virtual void ExitState(){}
        public virtual void PhysicsUpdate(){}
        public virtual void FrameUpdate(){}
    }
}