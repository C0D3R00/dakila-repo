using UnityEngine;

using TMPro;

using System.Collections;
using System.Collections.Generic;

// minimum jump force = sqrt(2 * Mathf.Abs(gravity) * minJumpHeight)

public class Jump : BehaviourAbstract
{
    [SerializeField]
    private float 
        _timeToJump = .4f,
        _minJumpHeight = 1f,
        _maxJumpHeight = 6f,
        _gravityMultiplier = .9f;

    private float
        _gravity = 0f,
        _minJumpVelocity = 0f,
        _maxJumpVelocity = 0f;

    private bool
       _isJumping = false;

    protected override void Start()
    {
        _gravity = -(2f * _maxJumpHeight) / Mathf.Pow(_timeToJump, 2f);
        _maxJumpVelocity = Mathf.Abs(_gravity) * _timeToJump;
        _minJumpVelocity = Mathf.Sqrt(2f * Mathf.Abs(_gravity) * _minJumpHeight);
    }

    protected override void Update()
    {
        // jump
        if (!_isJumping &&
             _collisionState.IsGrounded &&
             _inputState.Jump)
        {
            _isJumping = true;
            _playerState.Jumping = true;
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, _maxJumpVelocity);
        }

        // minimum velocity
        if (_isJumping &&
           !_inputState.Jump &&
            _rb2d.velocity.y > _minJumpVelocity)
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, _minJumpVelocity);

        // pull down with gravity
        if (!_collisionState.IsGrounded)
            _rb2d.velocity += new Vector2(0f, _gravity * _gravityMultiplier * Time.deltaTime);

        // let go of jump button
        if (_isJumping &&
           !_inputState.Jump)
            _playerState.Jumping = false;
        if (_isJumping &&
            _collisionState.IsGrounded &&
           !_inputState.Jump)
            _isJumping = false;
    }
}