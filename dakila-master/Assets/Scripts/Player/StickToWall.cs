using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class StickToWall : BehaviourAbstract
{
    [SerializeField]
    private float
        _velocityY = -10f;

    private float
        _gravity = 0f;

    protected override void Update()
    {
        if (!_playerState.WallSlide &&
            !_collisionState.IsGrounded &&
            !_playerState.WallJumping &&
             _collisionState.IsWallInFront &&
             _inputState.Walk != 0 &&
             _rb2d.velocity.y < 0f)
        {
            _playerState.WallSlide = true;
            _playerState.Jumping = false;
            _playerState.DoubleJumping = false;

            EventManager.Instance.TriggerEvent(ActionNames.WALL_SLIDE.ToString());

            _rb2d.velocity = Vector2.zero;
        }
        else if (_playerState.WallSlide)
        {
            if (_collisionState.IsGrounded ||
               !_collisionState.IsWallAtBack)
                _playerState.WallSlide = false;
            else if(_collisionState.IsWallAtBack)
                _rb2d.velocity = new Vector2(_rb2d.velocity.x, _velocityY);
        }

        //if (_isOnWall &&
        //   !_collisionState.IsGrounded &&
        //    _collisionState.IsWallAtBack)
        //    _rb2d.velocity = new Vector2(0f, _velocityY);
        //else if (_isOnWall &&
        //   !_collisionState.IsGrounded &&
        //   !_collisionState.IsWallAtBack)
        //{
        //    _isOnWall = false;
        //    _playerState.WallSlide = false;
        //}
    }
}
