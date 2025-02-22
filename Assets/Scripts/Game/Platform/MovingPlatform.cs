using System;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private float horizontalDistance = 1.0f;

    [SerializeField]
    private float verticalDistance = 1.0f;

    [SerializeField]
    private float speed = 1.0f;

    private Vector2 startPosition;
    private Collider2D coll;

    private void Awake()
    {
        startPosition = transform.position;
        coll = GetComponentInChildren<Collider2D>();
    }

    private void FixedUpdate()
    {
        var distanceToTravel = Vector2.Distance(startPosition, new Vector2(startPosition.x + horizontalDistance, startPosition.y + verticalDistance));
        
    }
}
