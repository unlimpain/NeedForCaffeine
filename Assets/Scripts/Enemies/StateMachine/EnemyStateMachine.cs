using UnityEngine;

namespace Enemies.StateMachine
{
    public class EnemyStateMachine
    {
        public EnemyState CurrentEnemyState;
        
        public void Initialize(EnemyState startingState)
        {
            CurrentEnemyState = startingState;
            CurrentEnemyState.EnterState();
        }

        public void ChangeState(EnemyState newState)
        {
            CurrentEnemyState.ExitState();
            CurrentEnemyState = newState;
            CurrentEnemyState.EnterState();
        }
    }
}
