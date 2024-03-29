﻿using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class Walk : BehaviourAbstract
{
    protected override void Update()
    {
        if (!_playerState.RecoilingX &&
            !_playerState.WallSlide &&
            !_playerState.WallJumping &&
            !_collisionState.IsWallInFront)
            _rb2d.velocity = new Vector2(_inputState.Walk * _playerAttributes.WalkSpeed, _rb2d.velocity.y);
        else if (_playerState.WallSlide &&
            (_inputState.Walk > 0 && _playerState.FacingRight) ||
            (_inputState.Walk < 0 && !_playerState.FacingRight))
            _rb2d.velocity = new Vector2(_inputState.Walk * _playerAttributes.WalkSpeed, _rb2d.velocity.y);
    }
}
