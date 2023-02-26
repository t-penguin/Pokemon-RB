using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static bool CanMove;

    // 0 - down; 1 - up; 2 - side(left, flip for right)
    // NPCs don't have walking sprites and thus only turn
    public Sprite[] facing = new Sprite[3];
    public Sprite[] walking = new Sprite[3];

    public Direction CurrentlyFacing { get; private set; }

    [field: SerializeField] public Vector3 TargetPosition { get; protected set; }
    public float moveTime = 3.75f;
    public bool isMoving;

    // Access to this object's Sprite Renderer and Box Collider
    protected SpriteRenderer _spriteRenderer;
    protected BoxCollider2D _boxCollider;
    protected Transform _spriteTransform;

    // Used for shared initialization
    protected virtual void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _spriteTransform = _spriteRenderer.gameObject.transform;

        TargetPosition = transform.position;
    }

    // Checks for a collision at some position in the specified direction on the specified layer
    // Outputs a RaycastHit2D for the first collider hit
    protected bool CheckCollision(Vector2 dir, out RaycastHit2D hit, int layer)
    {
        // Left shift to convert the layer into a layerMask
        int layerMask = 1 << layer;

        // The position to check for collision
        Vector2 point = (Vector2)transform.position + dir;

        // Send a linecast in the specified layer
        hit = Physics2D.Linecast(point, point, layerMask);

        // Return false if the linecast didn't hit anything in the specified layer
        if (hit.transform == null)
            return false;

        return true;
    }

    // Changes the direction in which this object's sprite will face
    public virtual void Turn(Direction dir)
    {
        // Get an index depending on which direction to turn
        int index = GetIndexFromDirection(dir);
        if (index == -1)
            return;

        // Flip the sprite renderer if the direction is right
        bool flip = false;
        if (dir == Direction.Right)
            flip = true;

        _spriteRenderer.sprite = facing[index];
        _spriteRenderer.flipX = flip;

        // Set the CurrentlyFacing direction to the one passed
        CurrentlyFacing = dir;
    }

    // Returns an int corresponding to the opposite direction
    // from which this object is facing
    public Direction GetOppositeDirection()
    {
        switch (CurrentlyFacing)
        {
            default:
                return Direction.None;
            case Direction.Down:
                return Direction.Up;
            case Direction.Up:
                return Direction.Down;
            case Direction.Left:
                return Direction.Right;
            case Direction.Right:
                return Direction.Left;
        }
    }

    // Returns an int corresponding to the index of the direction
    // for the facing and walking arrays. Returns -1 if not down, up, left, or right
    protected int GetIndexFromDirection(Direction dir)
    {
        switch (dir)
        {
            default:
                return -1;
            case Direction.Down:
                return 0;
            case Direction.Up:
                return 1;
            case Direction.Left:
                return 2;
            case Direction.Right:
                return 2;
        }
    }
}

public enum Direction
{
    None,
    Down,
    Up,
    Left,
    Right
}