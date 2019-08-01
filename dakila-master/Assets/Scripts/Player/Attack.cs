using UnityEngine;

using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Attack : BehaviourAbstract
{
    [SerializeField]
    private LayerMask
        _attackableLayersForward,
        _attackableLayersUp,
        _attackableLayersDown;

       [SerializeField]
    private GameObject
        _attackForwardGameObject,
        _attackUpGameObject,
        _attackDownGameObject;

    [SerializeField]
    private Transform
        _attackForwardTransform,
        _attackUpTransform,
        _attackDownTransform;

    [SerializeField]
    private Vector2
        _attackSize;

    [SerializeField]
    private float
        _attackPower = 1f,
        _attackRadius = 2f,
        _attackAnimationLength = .1f,
        _attackInterval = .4f,
        _recoilTimeX = .1f,
        _recoilTimeY = .25f,
        _recoilDistanceX = 1f,
        _recoilDistanceY = 2.5f;

    private bool
        _isAttacking = false;

    private float
        _timer = 0f,
        _recoilVelocityY = 0f;

    //protected override void Start()
    //{
    //    var gravity = -(2f * _recoilDistanceY) / Mathf.Pow(_recoilTimeY, 2f);
        
    //    _recoilVelocityY = Mathf.Abs(gravity) * _recoilTimeY;
    //}

    protected override void Update()
    {
        // grounded
        // not grounded looking down
        // looking up

        if (!_isAttacking &&
            _inputState.Attack)
        {
            _isAttacking = true;
            _timer = 0f;

            switch (_inputState.Look)
            {
                case 0:
                    // forward
                    GoAttack(AttackDirections.FORWARD);
                    StartCoroutine(AttackAnimationCo(AttackDirections.FORWARD));

                    break;
                case 1:
                    // up
                    GoAttack(AttackDirections.UP);
                    StartCoroutine(AttackAnimationCo(AttackDirections.UP));

                    break;
                case -1:
                    // down
                    if (!_collisionState.IsGrounded)
                    {
                        GoAttack(AttackDirections.DOWN);
                        StartCoroutine(AttackAnimationCo(AttackDirections.DOWN));
                    }
                    else
                    {
                        GoAttack(AttackDirections.FORWARD);
                        StartCoroutine(AttackAnimationCo(AttackDirections.FORWARD));
                    }

                    break;
            }
        }
        else if (_isAttacking)
        {
            if (_timer < _attackInterval)
                _timer += Time.deltaTime;
            else
            {
                _timer = 0f;
                _isAttacking = false;
            }
        }
    }

    private void GoAttack(AttackDirections attackDirection)
    {
        var position = Vector2.zero;
        var size = Vector2.zero;

        switch (attackDirection)
        {
            case AttackDirections.FORWARD:
                position = _attackForwardTransform.position;
                size = _attackSize;

                var hitsForward = Physics2D.OverlapBoxAll(position, size, 0f, _attackableLayersForward);

                if (hitsForward != null && hitsForward.Length > 0)
                {
                    _playerState.RecoilingX = true; // set to false by RecoilX Script
                    //RecoilX();

                    foreach (var hit in hitsForward)
                    {
                        // damage enemies
                    }
                }
                else
                    Debug.Log("no hit");

                break;
            case AttackDirections.UP:
                position = _attackUpTransform.position;
                size = new Vector2(_attackSize.y, _attackSize.x);

                var hitsUp = Physics2D.OverlapBoxAll(position, size, 0f, _attackableLayersUp);

                if (hitsUp != null && hitsUp.Length > 0)
                {
                    foreach (var hit in hitsUp)
                    {
                        // damage enemies
                    }
                }
                else
                    Debug.Log("no hit");

                break;
            case AttackDirections.DOWN:
                position = _attackDownTransform.position;
                size = new Vector2(_attackSize.y, _attackSize.x);

                var hitsDown = Physics2D.OverlapBoxAll(position, size, 0f, _attackableLayersDown);

                if (hitsDown != null && hitsDown.Length > 0)
                {
                    // recoil only when spike hit
                    if (hitsDown.Where(h => h.tag == "Spike").Count() > 0)
                        _playerState.RecoilingY = true; // set to false by RecoilY Script
                        //RecoilY();

                    EventManager.Instance.TriggerEvent(ActionNames.SPIKE_HIT.ToString());

                    foreach(var hit in hitsDown)
                    {
                        // damage enemies
                    }
                }
                else
                    Debug.Log("no hit");

                break;
        }        
    }

    //private void RecoilX()
    //{
    //    StartCoroutine(RecoilXCo());
    //}

    //private void RecoilY()
    //{
    //    StartCoroutine(RecoilYCo());
    //}

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(_attackForwardTransform.position, _attackSize);
        Gizmos.DrawWireCube(_attackUpTransform.position, new Vector2(_attackSize.y, _attackSize.x));
        Gizmos.DrawWireCube(_attackDownTransform.position, new Vector2(_attackSize.y, _attackSize.x));

        //Gizmos.DrawWireSphere(_attackForwardTransform.position, _attackRadius);
        //Gizmos.DrawWireSphere(_attackUpTransform.position, _attackRadius);
        //Gizmos.DrawWireSphere(_attackDownTransform.position, _attackRadius);
    }

    private IEnumerator AttackAnimationCo(AttackDirections attackDirection)
    {
        switch (attackDirection)
        {
            case AttackDirections.FORWARD:
                _attackForwardGameObject.SetActive(true);

                break;
            case AttackDirections.UP:
                _attackUpGameObject.SetActive(true);

                break;
            case AttackDirections.DOWN:
                _attackDownGameObject.SetActive(true);

                break;
        }

        var timer = 0f;

        while (timer < _attackAnimationLength)
        {
            timer += Time.deltaTime;

            yield return null;
        }

        switch (attackDirection)
        {
            case AttackDirections.FORWARD:
                _attackForwardGameObject.SetActive(false);

                break;
            case AttackDirections.UP:
                _attackUpGameObject.SetActive(false);

                break;
            case AttackDirections.DOWN:
                _attackDownGameObject.SetActive(false);

                break;
        }
    }

    //private IEnumerator RecoilXCo()
    //{
    //    var timer = 0f;
    //    var velocityX = _recoilDistanceX / _recoilTimeX;

    //    _playerState.RecoilingX = true;
    //    _rb2d.velocity = new Vector2(0f, _rb2d.velocity.y);
    //    _rb2d.velocity = new Vector2(_playerState.FacingRight ? -velocityX : velocityX, _rb2d.velocity.y);

    //    while (timer < _recoilTimeX)
    //    {
    //        timer += Time.deltaTime;

    //        yield return null;
    //    }

    //    _playerState.RecoilingX = false;
    //}

    //private IEnumerator RecoilYCo()
    //{
    //    var timer = 0f;

    //    _playerState.RecoilingY = true;
    //    _rb2d.velocity = new Vector2(_rb2d.velocity.x, 0f);
    //    _rb2d.velocity = new Vector2(_rb2d.velocity.x, _recoilVelocityY);

    //    while(timer < _recoilTimeY)
    //    {
    //        timer += Time.deltaTime;

    //        yield return null;
    //    }

    //    _playerState.RecoilingY = false;
    //}
}
