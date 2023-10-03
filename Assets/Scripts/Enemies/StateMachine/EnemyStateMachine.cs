using UnityEngine;

namespace Enemies.StateMachine
{
    public class EnemyStateMachine
    {
        public EnemyState CurrentEnemyState { get; private set; }
        private EnemyState _previousEnemyState;

        public void Initialize(EnemyState startingState)
        {
            CurrentEnemyState = startingState;
            CurrentEnemyState.EnterState();
        }

        public void ChangeState(EnemyState newState)
        {
            CurrentEnemyState.ExitState();
            _previousEnemyState = CurrentEnemyState;
            CurrentEnemyState = newState;
            CurrentEnemyState.EnterState();
        }

        public void ReturnToPreviousState()
        {
            EnemyState temp;
            CurrentEnemyState.ExitState();
            temp = CurrentEnemyState;
            CurrentEnemyState = _previousEnemyState;
            _previousEnemyState = temp;
            CurrentEnemyState.EnterState();
        }
    }
}
