using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _upSpeed = 5f;
    [SerializeField] private LayerMask contactLayerMask;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private bool _isFacingRight = true;
    private bool _isGround = false;
    private float horizontalDirection = 0f;
    private Vector2 _movePlayer;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        float upSpeed = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (_isFacingRight)
                FlipHorizontal();

            horizontalDirection = Input.GetAxis("Horizontal") * _speed;
            _animator.SetFloat("Speed", Mathf.Abs(horizontalDirection));
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!_isFacingRight)
                FlipHorizontal();

            horizontalDirection = Input.GetAxis("Horizontal") * _speed;
            _animator.SetFloat("Speed", horizontalDirection);
        }
        else
        {
            horizontalDirection = Input.GetAxis("Horizontal") * _speed;
            _animator.SetFloat("Speed", horizontalDirection);
        }

        if ((_isGround == true) && (Input.GetButtonDown("Jump")))
        {
            _isGround = false;
            _animator.SetBool("IsGround", _isGround);
            _animator.SetBool("JumpDirectionUp", GetJumpDirectionUp());
            upSpeed = _upSpeed;
        }
        else if (_isGround == false)
        {
            _animator.SetBool("JumpDirectionUp", GetJumpDirectionUp());
        }

        _movePlayer = new Vector2(horizontalDirection, upSpeed);
    }

    private bool GetJumpDirectionUp()
    {
        if (_rigidbody2D.velocity.y >= 0)
            return true;
        else
            return false;
    }

    private void FixedUpdate()
    {
        Move(_movePlayer);
    }

    private void Move(Vector2 move)
    {
        _rigidbody2D.velocity = new Vector2(move.x , _rigidbody2D.velocity.y);

        if (move.y > 0)
        {
            _rigidbody2D.velocity = new Vector2(0, move.y);
        }
    }

    private void FlipHorizontal()
    {
        _isFacingRight = !_isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void OnCollisionEnter2D(Collision2D colliderOther)
    {
        SetGround(colliderOther, true);
    }

    private void OnCollisionStay2D(Collision2D colliderOther)
    {
        SetGround(colliderOther, true);
    }

    private void OnCollisionExit2D(Collision2D colliderOther)
    {
        SetGround(colliderOther, false);
    }

    private void SetGround(Collision2D colliderOther, bool isGround)
    {
        if (!(colliderOther.otherCollider is CircleCollider2D))
            return;

        string calculateLayers = Convert.ToString(contactLayerMask.value, 2);

        if (calculateLayers.Length > colliderOther.gameObject.layer)
        {
            int index = calculateLayers.Length - 1 - colliderOther.gameObject.layer;
            int valueLayerArrayByIndex = Convert.ToInt32(calculateLayers[index].ToString());
            if (valueLayerArrayByIndex == 1)
            {
                _isGround = isGround;
                _animator.SetBool("IsGround", _isGround);
            }
        }
    }
}