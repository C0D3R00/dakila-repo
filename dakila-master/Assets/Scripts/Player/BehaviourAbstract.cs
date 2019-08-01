using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public abstract class BehaviourAbstract : MonoBehaviour
{
    protected PlayerState
        _playerState;

    protected PlayerAttributes
        _playerAttributes;

    protected InputState
        _inputState;

    protected CollisionState
        _collisionState;

    protected Rigidbody2D
        _rb2d;

    protected Animator
        _animator;

    private void Awake()
    {
        _playerState = GetComponent<PlayerState>();
        _playerAttributes = GetComponent<PlayerAttributes>();
        _inputState = GetComponent<InputState>();
        _collisionState = GetComponent<CollisionState>();
        _animator = GetComponent<Animator>();
        _rb2d = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start() { }
    protected virtual void Update() { }
    protected virtual void FixedUpdate() { }
    protected virtual void OnEnable() { }
    protected virtual void OnDisable() { }
}
