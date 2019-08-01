using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class WallJump : BehaviourAbstract
{
    [SerializeField]
    private float
        _jumpDistance = 4f,
        _timeToMoveAwayFromWall = 0f;

    private float
        _velocityX,
        _velocityY;

    protected override void Start()
    {
        _timeToMoveAwayFromWall = _playerAttributes.TimeToJump / 2f;
        _velocityX = _jumpDistance / _timeToMoveAwayFromWall;
        _velocityY = Mathf.Abs(_playerAttributes.Gravity) * _playerAttributes.TimeToJump;
    }

    protected override void Update()
    {
        if (!_playerState.WallJumping &&
             _playerState.WallSlide &&
             _collisionState.IsWallAtBack &&
             _inputState.Jump)
        {
            _playerState.WallJumping = true;
            _rb2d.velocity = new Vector2(_playerState.FacingRight ? _velocityX : -_velocityX, _velocityY);

            //StartCoroutine(WallJumpCo());
        }
        else if (_playerState.WallJumping &&
           !_inputState.Jump)
            _playerState.WallJumping = false;
    }

    private IEnumerator WallJumpCo()
    {
        var timer = 0f;

        _rb2d.velocity = new Vector2(_playerState.FacingRight ? _velocityX : -_velocityX, _velocityY);

        while (_inputState.Jump &&
            _inputState.Walk == 0f &&
            timer < _timeToMoveAwayFromWall)
        {
            timer += Time.deltaTime;

            yield return null;
        }

        _rb2d.velocity = new Vector2(0f, _rb2d.velocity.y);
    }
}
