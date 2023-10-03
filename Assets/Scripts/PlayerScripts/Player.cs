using System.Collections;
using PlayerScripts.StateMachine;
using PlayerScripts.StateMachine.ConcreteStates;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpSpeed;
        [SerializeField] private float _addedGravity;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _unhittableTime;
        [SerializeField] private float _damagedDistance;
        
        // TODO: groundCheck ^^
        public bool IsGrounded { get; set; }
        
        public Rigidbody2D _rigidbody2D { get; private set; }

        #region AttackingVariables
        
        [SerializeField] private Object _attackPrefab;
        [SerializeField] private float _reloadTime;
        [field: SerializeField] public float PlayerAttackTime { get; private set; }
        public static int PlayerDamage { get; private set; }
        private Transform _attackColliderTransform;
        private bool _isAttackAllowed = true;
        
        #endregion

        #region StateMachineVariables

        public PlayerStateMachine PlayerStateMachine { get; private set; }
        public IdleState IdleState { get; private set; }
        public RunningState RunningState { get; private set; }
        public AttackingState AttackingState { get; private set; }
        public FallingState FallingState { get; private set; }
        public JumpingState JumpingState { get; private set; }

        #endregion
        
        private float _currentHealth;
        private IEnumerator _jumpingCoroutine;
        private SpriteRenderer _sprite;
        private bool _isFacedRight = true;
        private bool _isHittable = true;
        private void Awake()
        {
            PlayerStateMachine = new PlayerStateMachine();
            IdleState = new IdleState(this, PlayerStateMachine);
            RunningState = new RunningState(this, PlayerStateMachine);
            AttackingState = new AttackingState(this, PlayerStateMachine);
            FallingState = new FallingState(this, PlayerStateMachine, _addedGravity);
            JumpingState = new JumpingState(this, PlayerStateMachine);

            _currentHealth = _maxHealth;
            PlayerDamage = 5;
        }

        private void Start()
        {
            PlayerStateMachine.Initialize(FallingState);
            _attackColliderTransform = GetComponentInChildren<Transform>(); //HitCreator in Player should always be first
            _sprite = GetComponentInChildren<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void LateUpdate()
        {
            _sprite.flipX = !_isFacedRight;
        }

        private void FixedUpdate()
        {   
            PlayerStateMachine.CurrentEnemyState.PhysicsUpdate();
        }

        private void Update()
        {
            PlayerStateMachine.CurrentEnemyState.FrameUpdate();
        }

        public void Run()
        {
            float movementDirection = Input.GetAxis("Horizontal");
            _rigidbody2D.position += new Vector2(movementDirection * _speed * Time.fixedDeltaTime, 0f);
            if (movementDirection != 0)
                _isFacedRight = movementDirection > 0;
        }

        public void Jump()
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpSpeed);
        }

        public void Attack()
        {
            if (_isAttackAllowed)
            {
                var playerAttack = Instantiate(_attackPrefab, _attackColliderTransform.position, Quaternion.identity);
                Destroy(playerAttack, PlayerAttackTime);
                _isAttackAllowed = false;
                Invoke(nameof(ReloadAttack), _reloadTime);
            }
        }
        
        public void TakeDamage(float damageAmount)
        {
            if (!_isHittable) return;
            _isHittable = false;
            
            _currentHealth -= damageAmount;
            if (_currentHealth <= 0f)
            {
                Die();
            }
            
            Invoke(nameof(AllowToHit), _unhittableTime);
        }

        public void Die()
        {
            print("dead");
        }

        private void GetBackFromHit(bool jumpToLeft)
        {
            Vector2 jumpDir = jumpToLeft ? -transform.right + transform.up : transform.right + transform.up;
            _rigidbody2D.AddForce(jumpDir * _damagedDistance, ForceMode2D.Impulse);
            PlayerStateMachine.ChangeState(FallingState); // TODO: remake
        }

        private void ReloadAttack()
        {
            _isAttackAllowed = true;
        }

        private void AllowToHit()
        {
            _isHittable = true;
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("World"))
            {
                IsGrounded = true;
            }
            if (other.gameObject.CompareTag("Enemy"))
            {
                bool jumpToLeft = other.collider.transform.position.x >= transform.position.x;
                GetBackFromHit(jumpToLeft);
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            PlayerStateMachine.ChangeState(FallingState);
        }
    }
}
