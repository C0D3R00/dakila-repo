using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class CollisionState : MonoBehaviour
{
    public bool IsGrounded { get; private set; }
    public bool IsRoofed { get; private set; }
    public bool IsWallInFront { get; private set; }
    public bool IsWallAtBack { get; private set; }

    [SerializeField]
    private Transform
        _l_groundRaycastTransform,
        _r_groundRaycastTransform,
        _l_roofRaycastTransform,
        _r_roofRaycastTransform;

    [SerializeField]
    private int
        _horizontalRayCount = 8,
        _verticalRayCount = 12;

    [SerializeField]
    private float
        _groundRaycastDistance = .15f;

    private CapsuleCollider2D
        _capsuleCollider2d;

    private void Awake()
    {
        _capsuleCollider2d = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        IsGrounded = CheckForGround();
        IsRoofed = CheckForRoof();
        IsWallInFront = CheckForWallInFront();
        IsWallAtBack = CheckForWallAtBack();
        //Debug.Log("is wall at back: " + IsWallAtBack);
    }

    private bool CheckForGround()
    {
        var startX = _l_groundRaycastTransform.position.x;
        var endX = _r_groundRaycastTransform.position.x;
        var incrementX = (endX - startX) / (_horizontalRayCount - 1);
        var groundHit = false;
        var raycastPositionX = 0f;

        for (var i = 0; i < _horizontalRayCount; i++)
        {
            var raycastOrigin = new Vector2(_l_groundRaycastTransform.position.x + raycastPositionX, _l_groundRaycastTransform.position.y);

            groundHit = Physics2D.Raycast(raycastOrigin, Vector2.down, _groundRaycastDistance, 1 << LayerMask.NameToLayer("Ground"));

            if (groundHit)
                break;

            raycastPositionX += incrementX;
        }

        return groundHit;
    }

    private bool CheckForRoof()
    {
        var startX = _l_roofRaycastTransform.position.x;
        var endX = _r_roofRaycastTransform.position.x;
        var incrementX = (endX - startX) / (_horizontalRayCount - 1);
        var roofHit = false;
        var raycastPositionX = 0f;

        for (var i = 0; i < _horizontalRayCount; i++)
        {
            var raycastOrigin = new Vector2(_l_roofRaycastTransform.position.x + raycastPositionX, _l_roofRaycastTransform.position.y);

            roofHit = Physics2D.Raycast(raycastOrigin, Vector2.down, _groundRaycastDistance, 1 << LayerMask.NameToLayer("Ground"));

            if (roofHit)
                break;

            raycastPositionX += incrementX;
        }

        return roofHit;
    }

    private bool CheckForWallInFront()
    {
        var startY = _r_groundRaycastTransform.position.y;
        var endY = _r_roofRaycastTransform.position.y;
        var incrementY = (endY - startY) / (_verticalRayCount - 1);
        var raycastPositionY = 0f;
        var wallHit = false;

        for (var i = 0; i < _verticalRayCount; i++)
        {
            var raycastOrigin = new Vector2(_r_groundRaycastTransform.position.x, _r_groundRaycastTransform.position.y + raycastPositionY);

            wallHit = Physics2D.Raycast(raycastOrigin, transform.localScale.x > 0f ? Vector2.right : Vector2.left, _groundRaycastDistance, 1 << LayerMask.NameToLayer("Wall"));

            if (wallHit)
                break;

            raycastPositionY += incrementY;
        }

        return wallHit;
    }

    private bool CheckForWallAtBack()
    {
        var startY = _r_groundRaycastTransform.position.y;
        var endY = _r_roofRaycastTransform.position.y;
        var incrementY = (endY - startY) / (_verticalRayCount - 1);
        var raycastPositionY = 0f;
        var wallHit = false;

        for (var i = 0; i < _verticalRayCount; i++)
        {
            var raycastOrigin = new Vector2(_l_groundRaycastTransform.position.x, _l_groundRaycastTransform.position.y + raycastPositionY);

            wallHit = Physics2D.Raycast(raycastOrigin, transform.localScale.x > 0f ? Vector2.left : Vector2.right, _groundRaycastDistance, 1 << LayerMask.NameToLayer("Wall"));

            if (wallHit)
                break;

            raycastPositionY += incrementY;
        }

        return wallHit;
    }

    private void OnDrawGizmos()
    {
        // ground and roof
        var startX = _l_groundRaycastTransform.position.x;
        var endX = _r_groundRaycastTransform.position.x;
        var incrementX = (endX - startX) / (_horizontalRayCount - 1);
        var raycastPositionX = 0f; // startX + incrementX;

        for (var i = 0; i < _horizontalRayCount; i++)
        {
            var raycastOriginGround = new Vector2(_l_groundRaycastTransform.position.x + raycastPositionX, _l_groundRaycastTransform.position.y);
            var raycastOriginRoof = new Vector2(_l_roofRaycastTransform.position.x + raycastPositionX, _l_roofRaycastTransform.position.y);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(raycastOriginGround, new Vector3(raycastOriginGround.x, raycastOriginGround.y - _groundRaycastDistance));
            Gizmos.DrawLine(raycastOriginRoof, new Vector3(raycastOriginRoof.x, raycastOriginRoof.y + _groundRaycastDistance));

            raycastPositionX += incrementX;
        }

        // wall
        var startY = _r_groundRaycastTransform.position.y;
        var endY = _r_roofRaycastTransform.position.y;
        var incrementY = (endY - startY) / (_verticalRayCount - 1);
        var raycastPositionY = 0f;

        for (var i = 0; i < _verticalRayCount; i++)
        {
            var raycastOriginWallFront = new Vector2(_r_groundRaycastTransform.position.x, _r_groundRaycastTransform.position.y + raycastPositionY);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(raycastOriginWallFront, new Vector3(raycastOriginWallFront.x + (transform.localScale.x > 0f ? _groundRaycastDistance : -_groundRaycastDistance), raycastOriginWallFront.y));

            var raycastOriginWallBack = new Vector2(_l_groundRaycastTransform.position.x, _l_groundRaycastTransform.position.y + raycastPositionY);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(raycastOriginWallBack, new Vector3(raycastOriginWallBack.x + (transform.localScale.x > 0f ? -_groundRaycastDistance : _groundRaycastDistance), raycastOriginWallBack.y));
            
            raycastPositionY += incrementY;
        }
    }
}
