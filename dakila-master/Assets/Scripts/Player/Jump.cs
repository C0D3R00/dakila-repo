using UnityEngine;

using TMPro;

using System.Collections;
using System.Collections.Generic;

// minimum jump force = sqrt(2 * Mathf.Abs(gravity) * minJumpHeight)

public class Jump : BehaviourAbstract
{
    private float
        _maxJumpVelocity = 0f;

    private int
        _jumpCount = 1;

    protected override void Start()
    {
        _maxJumpVelocity = Mathf.Abs(_playerAttributes.Gravity) * _playerAttributes.TimeToJump;
    }

    protected override void Update()
    {
        // jump only. fall will be in another script
        if (!_playerState.Jumping)
        {
            if (_collisionState.IsGrounded &&
                _inputState.Jump)
            {
                _jumpCount = 1;

                GoJump();
            }
            else if (!_collisionState.IsGrounded &&
               !_collisionState.IsWallAtBack &&
                _inputState.Jump &&
                _jumpCount < _playerAttributes.MaxJumpCount)
            {
                _jumpCount++;

                GoJump();
            }
        }
        else if (_playerState.Jumping &&
           !_inputState.Jump)
            _playerState.Jumping = false;
    }

    private void GoJump()
    {
        _playerState.Jumping = true;
        _rb2d.velocity = new Vector2(_rb2d.velocity.x, _maxJumpVelocity);

        //Debug.Log("jump count: " + _jumpCount + " maxJumpCount: " + _playerAttributes.MaxJumpCount + " : " + (_jumpCount < _playerAttributes.MaxJumpCount).ToString());
    }

    //// Version 1 - Working
    //protected override void Update()
    //{
    //    // jump
    //    if (!_isJumping &&
    //         _collisionState.IsGrounded &&
    //         _inputState.Jump)
    //    {
    //        _isJumping = true;
    //        _playerState.Jumping = true;
    //        _rb2d.velocity = new Vector2(_rb2d.velocity.x, _maxJumpVelocity);
    //    }

    //    // minimum velocity
    //    if (_isJumping &&
    //       !_inputState.Jump &&
    //        _rb2d.velocity.y > _minJumpVelocity)
    //        _rb2d.velocity = new Vector2(_rb2d.velocity.x, _minJumpVelocity);

    //    // pull down with gravity
    //    if (!_collisionState.IsGrounded)
    //        _rb2d.velocity += new Vector2(0f, _gravity * _gravityMultiplier * Time.deltaTime);

    //    // let go of jump button
    //    if (_isJumping &&
    //       !_inputState.Jump)
    //        _playerState.Jumping = false;
    //    if (_isJumping &&
    //        _collisionState.IsGrounded &&
    //       !_inputState.Jump)
    //        _isJumping = false;
    //}
}