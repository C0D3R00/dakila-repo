using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public abstract class BehaviourAbstract : MonoBehaviour
{
    protected InputState
        _inputState;

    protected CollisionState
        _collisionState;

    protected PlayerState
        _playerState;

    protected Rigidbody2D
        _rb2d;

    protected Animator
        _animator;

    private void Awake()
    {
        _inputState = GetComponent<InputState>();
        _collisionState = GetComponent<CollisionState>();
        _playerState = GetComponent<PlayerState>();
        _rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    protected virtual void Start() { }
    protected virtual void Update() { }
    protected virtual void FixedUpdate() { }
    protected virtual void OnEnable() { }
    protected virtual void OnDisable() { }
}
