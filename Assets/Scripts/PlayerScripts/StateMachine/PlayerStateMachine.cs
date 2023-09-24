

namespace PlayerScripts.StateMachine
{
    public class PlayerStateMachine
    {
        public PlayerState CurrentEnemyState;
        
        public void Initialize(PlayerState startingState)
        {
            CurrentEnemyState = startingState;
            CurrentEnemyState.EnterState();
        }

        public void ChangeState(PlayerState newState)
        {
            CurrentEnemyState.ExitState();
            CurrentEnemyState = newState;
            CurrentEnemyState.EnterState();
        }
    }
}