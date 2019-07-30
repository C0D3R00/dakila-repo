using UnityEngine;
using UnityEngine.InputSystem;

using System.Collections;
using System.Collections.Generic;

public class InputState : MonoBehaviour
{
    public int Walk { get; private set; }
    public int Look { get; private set; }
    public bool Jump { get; private set; }
    public bool Attack { get; private set; }

    public float JumpHoldTime { get; private set; }

    [SerializeField]
    private float
        _walkThreshold = .25f,
        _lookThreshold = .25f;

    [SerializeField]
    private InputAction
        _walk,
        _look,
        _jump,
        _attack;

    //private int
    //    _test = 0;

    private void Awake()
    {
        _walk.performed += OnWalk;
        _walk.canceled += context => Walk = 0;

        _look.performed += OnLook;
        _look.canceled += context => Look = 0;

        //_jump.started += context => _test = 1;
        //_jump.performed += context => _test = 2;
        //_jump.canceled += context => _test = 0;

        _jump.performed += context => Jump = true;
        _jump.canceled += context => Jump = false;

        _attack.performed += context => Attack = true;
        _attack.canceled += context => Attack = false;
    }

    //private void Update()
    //{
    //    Debug.Log(_test);
    //}

    private void OnEnable()
    {
        _walk.Enable();
        _look.Enable();
        _jump.Enable();
        _attack.Enable();
    }

    private void OnDisable()
    {
        _walk.Disable();
        _look.Disable();
        _jump.Disable();
        _attack.Disable();
    }

    private void OnWalk(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<float>();

        if (value > _walkThreshold)
            Walk = 1;
        else if (value < -_walkThreshold)
            Walk = -1;
        else
            Walk = 0;
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<float>();

        if (value > _lookThreshold)
            Look = 1;
        else if (value < -_lookThreshold)
            Look = -1;
        else
            Look = 0;
    }
}
