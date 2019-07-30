using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class Flip : BehaviourAbstract
{
    private bool
        _isFacingRight = false;

    protected override void Update()
    {
        if (!_playerState.WallSlide)
        {
            if (_inputState.Walk > 0)
                _isFacingRight = true;
            else if (_inputState.Walk < 0)
                _isFacingRight = false;

            _playerState.FacingRight = _isFacingRight;

            if (_inputState.Walk != 0)
                transform.localScale = new Vector2(_inputState.Walk, 1f);
        }
        else if (_playerState.WallSlide &&
            _collisionState.IsWallInFront)
        {
            _isFacingRight = !_isFacingRight;
            _playerState.FacingRight = _isFacingRight;

            transform.localScale = new Vector2(_isFacingRight ? 1f : -1f, 1f);
        }
    }
}
