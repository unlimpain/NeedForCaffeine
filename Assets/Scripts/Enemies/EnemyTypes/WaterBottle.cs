using PlayerScripts;
using UnityEngine;
using Utility;

namespace Enemies.EnemyTypes
{
    public class WaterBottle : Enemy
    {
        [SerializeField] private float _movementRange;
        [SerializeField] private float _runningSpeed;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _bulletCreationTransform;
        [SerializeField] private float _bulletSpeed;
        private Transform _playerTransform;
        private Vector3 _startingPos;
        private bool _isMovingRight;
        
        private new void Start()
        {
            base.Start();
            StateMachine.ChangeState(RunningState);
            _startingPos = transform.position;
            _playerTransform = FindObjectOfType<Player>().transform;
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

        public override void Attack()
        {
            base.Attack();
            
            var bullet = Instantiate(_bulletPrefab, _bulletCreationTransform.position, Quaternion.identity);
            var bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            Launch(_playerTransform, bulletRigidbody);
            
            Destroy(bullet, 3f);
        }

        private void Launch(Transform target, Rigidbody2D bulletRigidbody)
        {
            bulletRigidbody.SetTrajectory(target.position, _bulletSpeed);
        }
        
        
    }
}