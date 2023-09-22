using System;
using System.Collections;
using Enemies.StateMachine.ConcreteStates;
using UnityEngine;

namespace Enemies.EnemyTypes
{
    public class WaterBottle : Enemy
    {
        [SerializeField] private float _movementRange;
        [SerializeField] private float _runningSpeed;
        private Vector3 _startingPos;
        private bool _isMovingRight;
        
        private void Start()
        {
            base.Start();
            StateMachine.ChangeState(RunningState);
            _startingPos = transform.position;
        }
        
        public override void Die()
        {
            base.Die();
        }

        public override void Move()
        {
            
            if (_isMovingRight)
                Rigidbody2D.position += new Vector2(_runningSpeed * Time.fixedDeltaTime, 0f);
            else
                Rigidbody2D.position -= new Vector2(_runningSpeed * Time.fixedDeltaTime, 0f);
            

            if (transform.position.x > _startingPos.x + _movementRange)
                _isMovingRight = false;
            
            else if (transform.position.x < _startingPos.x - _movementRange)
                _isMovingRight = true;
            
        }


    }
}