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
        [SerializeField] protected float _attackDamage;
        [SerializeField] protected float _collisionDamage;
        public float CurrentHealth { get; set; }
        public Rigidbody2D Rigidbody2D { get; set; }
        public bool IsFacedRight { get; set; }
        public SpriteRenderer Sprite { get; private set; }

        
        
        #region StateMachineVariables

        public EnemyStateMachine StateMachine { get; private set; }
        public EnemyState RunningState { get; private set; }
        public EnemyState IdleState { get; private set; }
        
        public EnemyState AttackingState { get; private set; }

        #endregion

        protected void Start()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            CurrentHealth = MaxHealth;
            Sprite = GetComponent<SpriteRenderer>();
            
            StateMachine = new EnemyStateMachine();
            RunningState = new EnemyRunningState(this, StateMachine, FindObjectOfType<Player>().transform);
            IdleState = new EnemyIdleState(this, StateMachine);
            AttackingState = new EnemyAttackingState(this, StateMachine, FindObjectOfType<Player>().transform);
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

        public virtual void Attack()
        {
            
        }
        
        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            print("1211");
            if (other.collider.CompareTag("Player"))
                FindObjectOfType<Player>().TakeDamage(_collisionDamage);
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PlayerHit"))
                TakeDamage(Player.PlayerDamage);
        }
    }
}