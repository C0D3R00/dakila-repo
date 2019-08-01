using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class PlayerAttributes : MonoBehaviour
{
    [SerializeField]
    private float
        _walkSpeed = 5f,
           _timeToJump = .4f,
           _minJumpHeight = 1f,
           _maxJumpHeight = 6f,
           _gravityMultiplier = .9f;

    [SerializeField]
    private int
        _maxJumpCount = 2;

    public float WalkSpeed { get { return _walkSpeed; } }
    public float TimeToJump { get { return _timeToJump; } }
    public float MinJumpHeight { get { return _minJumpHeight; } }
    public float MaxJumpHeight { get { return _maxJumpHeight; } }
    public float GravityMultiplier { get { return _gravityMultiplier; } }
    public float Gravity { get; private set; }

    public int MaxJumpCount { get { return _maxJumpCount; } }

    private void Awake()
    {
        Gravity = -(2f * MaxJumpHeight) / Mathf.Pow(TimeToJump, 2f);
    }

}
