using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpSpeed;
    private bool _isGrounded = true;
    public Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;
    private bool _isFacedRight;

    private IEnumerator _jumpingCoroutine;
    

    private void Start()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        _sprite.flipX = !_isFacedRight;
    }

    private void FixedUpdate()
    {   
        Run();
        if (_isGrounded && Input.GetKey(KeyCode.Z))
        {
            Jump();
            _isGrounded = false;
        }
    }

    public void Run()
    {
        float movementDirection = Input.GetAxis("Horizontal");
        _rigidbody.position += new Vector2(movementDirection * _speed * Time.fixedDeltaTime, 0f);
        if (Math.Abs(movementDirection) > 0)
            _isFacedRight = movementDirection > 0;
    }

    private void Jump()
    {
        Invoke(nameof(StopJump), 0.2f);
        _jumpingCoroutine = Jumping();
        StartCoroutine(_jumpingCoroutine);
    }

    private IEnumerator Jumping()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpSpeed);
        while (!Input.GetKeyUp(KeyCode.Z))
        {
            _rigidbody.velocity += Vector2.up * 1f;
            yield return new WaitForFixedUpdate();
        }
    }

    private void StopJump()
    {
        StopCoroutine(_jumpingCoroutine);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _isGrounded = true;
    }
}
