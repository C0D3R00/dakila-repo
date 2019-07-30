using UnityEngine;

using System.Collections;
using System.Collections.Generic;

// wall jump must be acquired BEFORE double jump

public class DoubleJump : BehaviourAbstract
{
    [SerializeField]
    private float
        _timeToJump = .65f,
        _minJumpHeight = 1f,
        _maxJumpHeight = 7.5F;

    private float
        _gravity = 0f,
        _minJumpVelocity = 0f,
        _maxJumpVelocity = 0f;

    private bool
       _isDoubleJumping = false;

    protected override void Start()
    {
        _gravity = -(2f * _maxJumpHeight) / Mathf.Pow(_timeToJump, 2f);
        _maxJumpVelocity = Mathf.Abs(_gravity) * _timeToJump;
        _minJumpVelocity = Mathf.Sqrt(2f * Mathf.Abs(_gravity) * _minJumpHeight);
    }

    protected override void Update()
    {
        //if (!_playerState.WallSlide)
        //{
            if (!_isDoubleJumping &&
                !_collisionState.IsGrounded &&
                !_collisionState.IsWallInFront &&
                !_collisionState.IsWallAtBack &&
                !_playerState.Jumping &&
                !_playerState.DoubleJumping &&
                 _inputState.Jump)
            {
                _isDoubleJumping = true;
                _playerState.DoubleJumping = true;

                _rb2d.velocity = new Vector2(_rb2d.velocity.x, 0f);
                _rb2d.velocity = new Vector2(_rb2d.velocity.x, _maxJumpVelocity);
            }

            if (_isDoubleJumping &&
               !_inputState.Jump)
                _playerState.DoubleJumping = false;
            if (_isDoubleJumping &&
                _collisionState.IsGrounded &&
               !_inputState.Jump)
                _isDoubleJumping = false;
        //}
    }

    protected override void OnEnable()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.StartListening(ActionNames.SPIKE_HIT.ToString(), OnSpikeHit);
            EventManager.Instance.StartListening(ActionNames.WALL_SLIDE.ToString(), OnSpikeHit);
        }
    }

    protected override void OnDisable()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.StopListening(ActionNames.SPIKE_HIT.ToString(), OnSpikeHit);
            EventManager.Instance.StopListening(ActionNames.WALL_SLIDE.ToString(), OnSpikeHit);
        }
    }

    private void OnSpikeHit()
    {
        _isDoubleJumping = false;
    }
}
