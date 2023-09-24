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
        public float CurrentHealth { get; set; }
        public Rigidbody2D Rigidbody2D { get; set; }
        public SpriteRenderer Sprite { get; private set; }
        public bool IsFacedRight { get; set; }
        
        #region StateMachineVariables

        public EnemyStateMachine StateMachine;
        public EnemyState RunningState;
        public EnemyState IdleState;

        #endregion

        protected void Start()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            CurrentHealth = MaxHealth;
            Sprite = GetComponent<SpriteRenderer>();
            
            StateMachine = new EnemyStateMachine();
            RunningState = new EnemyRunningState(this, StateMachine);
            IdleState = new EnemyIdleState(this, StateMachine);
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
            Destroy(this.gameObject);
        }

        public virtual void Move()
        {
            
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            print("1211");
            if (other.collider.CompareTag("PlayerHit"))
                TakeDamage(Player.PlayerDamage);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("PlayerHit"))
                TakeDamage(Player.PlayerDamage);
        }
    }
}