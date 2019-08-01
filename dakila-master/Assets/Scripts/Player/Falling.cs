using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class Falling : BehaviourAbstract
{
    private float
        _gravity = 0f,
        _minJumpVelocity = 0f;

    protected override void Start()
    {
        _gravity = -(2f * _playerAttributes.MaxJumpHeight) / Mathf.Pow(_playerAttributes.TimeToJump, 2f);
        _minJumpVelocity = Mathf.Sqrt(2f * Mathf.Abs(_playerAttributes.Gravity) * _playerAttributes.MinJumpHeight);
    }

    protected override void Update()
    {
        // stop player from rising by using _minJumpVelocity
        if (!_inputState.Jump &&
             _rb2d.velocity.y > _minJumpVelocity)
             _rb2d.velocity = new Vector2(_rb2d.velocity.x, _minJumpVelocity);

        // pull down with gravity
        if (!_collisionState.IsGrounded)
             _rb2d.velocity += new Vector2(0f, _gravity * _playerAttributes.GravityMultiplier * Time.deltaTime);
    }
}
