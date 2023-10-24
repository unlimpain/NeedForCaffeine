using System;
using Enemies.StateMachine;
using Enemies.StateMachine.ConcreteStates;
using PlayerScripts;
using UnityEngine;


namespace Enemies
{
    public class Enemy : MonoBehaviour, IDamagable, IMovable
    {
        [field: SerializeField] public float MaxHealth { get; set; }
        [field: SerializeField] public float PlayerFindingDistance { get; set; }
        [SerializeField] protected float attackDamage; 
        [SerializeField] protected float collisionDamage;
        [SerializeField] protected int reloadTimeInMilliseconds;
        public float CurrentHealth { get; set; }
        public Action OnDirectionChange { get; set; }
        public Rigidbody2D Rigidbody2D { get; set; }
        public bool IsFacedRight { get; set; }
        public SpriteRenderer Sprite { get; private set; }
        
        
        #region StateMachineVariables
        public EnemyStateMachine StateMachine { get; protected set; }
        public EnemyState RunningState { get; protected set; }
        public EnemyState IdleState { get; protected set; }
        public EnemyState AttackingState { get; protected set; }
        #endregion

        protected void Start()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            CurrentHealth = MaxHealth;
            Sprite = GetComponent<SpriteRenderer>();
            Transform playerTransform = FindObjectOfType<Player>().transform;
            
            StateMachine = new EnemyStateMachine();
            RunningState = new EnemyRunningState(this, StateMachine, playerTransform);
            IdleState = new EnemyIdleState(this, StateMachine);
            AttackingState = new EnemyAttackingState(this, StateMachine, playerTransform, reloadTimeInMilliseconds);
            StateMachine.Initialize(IdleState);
        }

        private void FixedUpdate()
        {
            StateMachine.CurrentEnemyState.PhysicsUpdate();
        }

        private void Update()
        {
            StateMachine.CurrentEnemyState.FrameUpdate();
        }

        public virtual void TakeDamage(float damageAmount)
        {
            CurrentHealth -= damageAmount;
            if (CurrentHealth <= 0f)
            {
                Die();
            }
        }

        public virtual void Die()
        {
            Destroy(gameObject);
        }

        public virtual void Move()
        {
            
        }

        public virtual void Attack(Transform targetTransform)
        {
            
        }
        
        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            print("1211");
            if (other.collider.CompareTag("Player"))
                FindObjectOfType<Player>().TakeDamage(collisionDamage);
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PlayerHit"))
                TakeDamage(Player.PlayerDamage);
        }
    }
}