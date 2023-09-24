using System.Collections;
using PlayerScripts.StateMachine;
using PlayerScripts.StateMachine.ConcreteStates;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpSpeed;
        public bool IsGrounded;
        private Rigidbody2D _rigidbody2D;

        #region AttackingVariables
        [SerializeField] private Object _attackPrefab;
        public static int PlayerDamage = 5;
        public float PlayerAttackTime;
        private Transform _attackColliderTransform;
        #endregion

        #region StateMachineVariables

        public PlayerStateMachine StateMachine;
        public IdleState IdleState;
        public RunningState RunningState;
        public AttackingState AttackingState;
        public FallingState FallingState;
        public JumpingState JumpingState;
        
        #endregion
        
        private IEnumerator _jumpingCoroutine;
        private SpriteRenderer _sprite;
        private bool _isFacedRight = true;
        private void Awake()
        {
            StateMachine = new PlayerStateMachine();
            IdleState = new IdleState(this, StateMachine);
            RunningState = new RunningState(this, StateMachine);
            AttackingState = new AttackingState(this, StateMachine);
            FallingState = new FallingState(this, StateMachine);
            JumpingState = new JumpingState(this, StateMachine);
        }

        private void Start()
        {
            StateMachine.Initialize(IdleState);
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
            StateMachine.CurrentEnemyState.PhysicsUpdate();
        }

        private void Update()
        {
            StateMachine.CurrentEnemyState.FrameUpdate();
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
            // Invoke(nameof(StopJump), 0.2f);
            // _jumpingCoroutine = Jumping();
            // StartCoroutine(_jumpingCoroutine);
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpSpeed);
        }

        public void Attack()
        {
            var playerAttack = Instantiate(_attackPrefab, _attackColliderTransform, false);
            Destroy(playerAttack, PlayerAttackTime);
        }
        
        private IEnumerator Jumping()
        {
            while (!Input.GetKeyUp(KeyCode.Z))
            {
                _rigidbody2D.velocity += Vector2.up * 1f;
                yield return new WaitForFixedUpdate();
            }
        }

        private void StopJump()
        {
            StopCoroutine(_jumpingCoroutine);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            IsGrounded = true;
        }
        
    }
}
