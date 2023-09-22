using System;
using System.Collections;
using System.Collections.Generic;
using Enemies.StateMachine;
using Enemies.StateMachine.ConcreteStates;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour, IDamagable, IMovable
    {
        #region StateMachineVariables

        public EnemyStateMachine StateMachine;
        public EnemyState RunningState;
        public EnemyState IdleState;

        #endregion
        
        
        [field: SerializeField] public float MaxHealth { get; set; }
        public float CurrentHealth { get; set; }
        public Rigidbody2D Rigidbody2D { get; set; }
        public SpriteRenderer Sprite { get; private set; }
        public bool IsFacedRight { get; set; }


        private void Awake()
        {
            
        }

        protected void Start()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            CurrentHealth = MaxHealth;
            Sprite = GetComponent<SpriteRenderer>();
            
            StateMachine = new EnemyStateMachine();
            RunningState = new EnemyRunningState(this, StateMachine);
            IdleState = new IdleState(this, StateMachine);
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

        public void TakeDamage(float damageAmount)
        {
            CurrentHealth -= damageAmount;
            if (CurrentHealth <= 0f)
            {
                Die();
            }
        }

        public virtual void Die()
        {
            
        }

        public virtual void Move()
        {
            
        }
    }
}