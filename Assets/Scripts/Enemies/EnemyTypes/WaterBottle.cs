using System;
using System.Collections;
using Enemies.StateMachine.ConcreteStates;
using PlayerScripts;
using UnityEngine;
using UnityEngine.UIElements;

namespace Enemies.EnemyTypes
{
    public class WaterBottle : Enemy
    {
        [SerializeField] private float _movementRange;
        [SerializeField] private float _runningSpeed;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _bulletCreationTransform;
        [SerializeField] private float _bulletSpeed;
        private Vector3 _startingPos;
        private bool _isMovingRight;
        
        private new void Start()
        {
            base.Start();
            StateMachine.ChangeState(RunningState);
            _startingPos = transform.position;
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

        // TODO: NOT WORKING FORMULA 
        #region BulletLaunching
            
        public override void Attack()
        {
            base.Attack();
            Transform playerTransform = FindObjectOfType<Player>().transform;
            var bullet = Instantiate(_bulletPrefab, _bulletCreationTransform);
            var bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            Launch(playerTransform, bulletRigidbody);
            Destroy(bullet, 3f);
        }

        private void Launch(Transform target, Rigidbody2D bulletRigidbody)
        {
            float angle = GetAngle(transform.position, target.position, _bulletSpeed, Physics2D.gravity.y);
            var forceToAdd = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * _bulletSpeed;
            bulletRigidbody.AddForce(forceToAdd, ForceMode2D.Impulse);
        }

        private float GetAngle(Vector2 origin, Vector2 destination, float speed, float gravity)
        {
            float angle = 0.0f;
            
            float x = Mathf.Abs(destination.x - origin.x);
            float y = Mathf.Abs(destination.y - origin.y);
            float v = speed;
            float g = gravity;

            
            float valueToBeSquareRooted = Mathf.Pow(v, 4) - g * (g * Mathf.Pow(x, 2) + 2 * y * Mathf.Pow(v, 2));
            if (valueToBeSquareRooted >= 0)
            {
                angle = Mathf.Atan((Mathf.Pow(v, 2) + Mathf.Sqrt(valueToBeSquareRooted)) / g * x);
            }
            
            return angle;
        }

        #endregion
        
    }
}