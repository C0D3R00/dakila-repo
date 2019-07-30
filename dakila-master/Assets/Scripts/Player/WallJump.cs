using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class WallJump : BehaviourAbstract
{
    [SerializeField]
    private float 
        _timeToJump = .4f,
        _minJumpHeight = 1f,
        _maxJumpHeight = 6f;

    private bool
        _isWallJumping = false;

    private float
        _wallJumpTimer = 0f,
        _timeToMoveAwayFromWall = 0f;

    protected override void Start()
    {
        _timeToMoveAwayFromWall = _timeToJump / 2f;
    }

    protected override void Update()
    {
        //if(!_playerState.WallJumping &&
        //    _playerState.WallSlide &&
        //    _collisionState.IsWallAtBack)
        //{
        //    _playerState.WallJumping = true;
        //    _wallJumpTimer = 0f;
        //}
    }
}
