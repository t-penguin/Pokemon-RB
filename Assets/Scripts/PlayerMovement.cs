using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : Movement
{
    public static bool Teleporting;

    [SerializeField] CameraFollow _cameraFollow;
    [SerializeField] GameObject _shadow;
    [SerializeField] float _walkDelay = 0.1f;
    float _delay;
    bool _flipAnim = false;
    public Direction toMove; // Remove?
    public bool checkForWildBattle; // Remove?

    Direction _newDirection;
    bool _turn;
    bool _walk;
    Vector3 _rawPosition;

    bool _jumpLedge;
    bool _jumpUp;
    float _jumpDelay;
    Vector3 _rawJumpPosition;
    Vector3 _spriteOffset = new Vector3(0, 0.25f, 0);
    Vector3 _jumpOffset = new Vector3(0, 1, 0);

    bool _teleport;
    Vector2 _teleportTargetPos;
    GameMap _teleportTargetMap;
    Direction _teleportStepOff;
    Direction _teleportTrigger;

    #region Monobehaviour Callbacks

    public void OnEnable()
    {
        TestInputManager.MoveAction.performed += OnMovePlayer;
        TestInputManager.MoveAction.canceled += OnMovePlayer;
        MainMenu.Opened += ClearMovement;
    }

    private void OnDisable()
    {
        TestInputManager.MoveAction.performed -= OnMovePlayer;
        TestInputManager.MoveAction.canceled -= OnMovePlayer;
        MainMenu.Opened -= ClearMovement;
    }

    protected override void Start()
    {
        base.Start();
        Turn(Direction.Down);
        _rawPosition = transform.position;
        _rawJumpPosition = _spriteTransform.localPosition;
    }

    private void Update()
    { 
        HandleMovement();
        HandleJump();
    }

    #endregion

    private void ClearMovement()
    {
        _walk = false;
        _turn = false;
    }

    private void HandleMovement()
    {
        if (!CanMove)
            return;

        // Move towards the destination position if not already there
        if (transform.position != TargetPosition)
        {
            MainMenu.CanOpen = false;
            transform.position = SmoothPixelMovement(_rawPosition, TargetPosition, Time.deltaTime * moveTime, out _rawPosition);
            _cameraFollow.UpdateCameraPosition();

            if (transform.position == TargetPosition)
            {
                if (_teleport)
                {
                    StartCoroutine(Teleport());
                    return;
                }
                
                AttemptWildBattle();
                return;
            }
        }

        if (isMoving)
            return;

        // Decrease the delay timer if it is great than 0
        if (_delay > 0)
        {
            _delay -= Time.deltaTime;
            return;
        }

        // Turn if attempting to move in a direction we are not facing
        if (_turn)
        {
            MainMenu.CanOpen = false;
            Turn(_newDirection);
            _turn = false;
            _delay = _walkDelay;
            AttemptWildBattle();
            return;
        }
        
        // Move in the direction we are facing if there's input to move
        if (_walk && !Teleporting && !_jumpLedge)
            StartCoroutine(Walk(CurrentlyFacing));
    }

    private void HandleJump()
    {
        if (!_jumpLedge)
            return;

        // MenuTest.MainMenu.CanOpen = false;

        if (_jumpDelay > 0)
        {
            _jumpDelay -= Time.deltaTime;
            return;
        }

        // First half of the jump
        if (_jumpUp)
        {
            _spriteTransform.localPosition = SmoothPixelMovement(_rawJumpPosition, _jumpOffset, Time.deltaTime * moveTime, out _rawJumpPosition);
            if (_spriteTransform.localPosition == _jumpOffset)
            {
                _jumpUp = false;
                _jumpDelay = 6 / 60f;
            }
            return;
        }
        
        // Second half of the jump
        _spriteTransform.localPosition = SmoothPixelMovement(_rawJumpPosition, _spriteOffset, Time.deltaTime * moveTime, out _rawJumpPosition);
        if (_spriteTransform.localPosition == _spriteOffset)
        {
            _jumpDelay = 6 / 60f;

            if (transform.position == TargetPosition)
            {
                _jumpLedge = false;
                _shadow.SetActive(false);
                // MenuTest.MainMenu.CanOpen = true;
            }
        }

    }

    private Vector3 SmoothPixelMovement(Vector3 start, Vector3 end, float maxDistanceDelta, out Vector3 rawPosition)
    {
        rawPosition = Vector3.MoveTowards(start, end, maxDistanceDelta);
        float rawX = rawPosition.x;
        float rawY = rawPosition.y;

        // Round to the nearest 1/16
        float pixelX = Mathf.Round(rawX * 16) / 16f;
        float pixelY = Mathf.Round(rawY * 16) / 16f;

        return new Vector3(pixelX, pixelY, rawPosition.z);
    }

    public void OnMovePlayer(InputAction.CallbackContext context)
    {
        if (!CanMove)
            return;

        _turn = false;
        _walk = false;

        // Read and set the new direction to attempt to move in
        Vector2 dir = context.ReadValue<Vector2>();
        if (dir == Vector2.zero) _newDirection = Direction.None;
        else if (dir.y < 0) _newDirection = Direction.Down;
        else if (dir.y > 0) _newDirection = Direction.Up;
        else if (dir.x < 0) _newDirection = Direction.Left;
        else _newDirection = Direction.Right;

        if (_newDirection == Direction.None)
            return;

        if (CurrentlyFacing != _newDirection && !_jumpLedge) 
            _turn = true;
        
        _walk = true;
    }

    // Attempts an interaction in the direction the player is facing
    public void AttemptInteraction()
    {
        if(CurrentlyFacing == Direction.None)
            Debug.LogError($"PlayerMovement.AttemptInteraction: CurrentlyFacing variable set to unexpected value {CurrentlyFacing}.");

        // Set target direction to the currently facing direction
        Vector2 direction = CurrentlyFacing.ToVector2();
        RaycastHit2D hit;

        if (CheckCollision(direction, out hit, LayerMask.NameToLayer("Interactables")))
        {
            EventManager.current.OnInteractionStart(hit.collider.GetComponent<Interaction>());
        }
    }

    // Updates the position Vector if there isn't a collision in the specified direction
    // Plays the walking animation regardless of collision
    public IEnumerator Walk(Direction dir)
    {
        if (dir == Direction.None)
            yield break;

        MainMenu.CanOpen = false;

        // Set the target direction to the given direction
        Vector2 direction = dir.ToVector2();
        RaycastHit2D hit;

        // Teleport if attempting to move in the direction of a teleport trigger
        if(dir == _teleportTrigger && _teleportTrigger != Direction.None)
        {
            yield return StartCoroutine(Teleport());
            MainMenu.CanOpen = true;
            yield break;
        }

        isMoving = true;

        if(CheckCollision(direction, out hit, LayerMask.NameToLayer("Blocking")))
        {
            yield return StartCoroutine(WalkAnimation(dir));
            MainMenu.CanOpen = true;
            yield break;
        }

        if (CheckCollision(direction, out hit, LayerMask.NameToLayer("Ledges")))
        {
            if (dir == hit.collider.GetComponent<Ledge>().JumpDirection)
            {
                yield return new WaitForSeconds(4 / 60f);
                TargetPosition += (Vector3)direction * 2;
                _jumpLedge = true;
                _jumpUp = true;
                _shadow.SetActive(true);
                yield return StartCoroutine(WalkAnimation(dir));
            }
        }
        else
            TargetPosition += (Vector3)direction;

        // Clear the teleport fields before moving onto a new square
        _teleport = false;
        _teleportStepOff = Direction.None;
        _teleportTrigger = Direction.None;

        yield return StartCoroutine(WalkAnimation(dir));
        MainMenu.CanOpen = true;
        /*// Update the position vector if there is nothing obstructing movement
        if (!CheckCollision(direction, out hit, LayerMask.NameToLayer("Blocking")))
        {
            // Check for a ledge
            if (CheckCollision(direction, out hit, LayerMask.NameToLayer("Ledges")))
            {
                if (dir == hit.collider.GetComponent<Ledge>().JumpDirection)
                {
                    yield return new WaitForSeconds(5 / 60f);
                    pos += (Vector3)direction * 2;
                    _jumpLedge = true;
                    _jumpUp = true;
                    _shadow.SetActive(true);
                    yield return StartCoroutine(WalkAnimation(dir));
                }
            }
            else
                pos += (Vector3)direction;

            // Clear the teleport fields before moving onto a new square
            _teleport = false;
            _teleportStepOff = Direction.None;
            _teleportTrigger = Direction.None;
        }

        yield return StartCoroutine(WalkAnimation(dir));*/
    }

    // Handles the walking animation using a coroutine
    // If this animation is playing, the player is considered to be moving
    public IEnumerator WalkAnimation(Direction dir)
    {
        float animDelay = 0.125f;

        // Walking right should flip the sprites along the x
        _spriteRenderer.flipX = dir == Direction.Right;

        // Get the index for the corresponding sprites
        int index = GetIndexFromDirection(dir);
        if (index == -1)
            yield break;

        if (index == 0 || index == 1)
            _spriteRenderer.flipX = _flipAnim;

        yield return new WaitForSeconds(animDelay);
        _spriteRenderer.sprite = walking[index];
        yield return new WaitForSeconds(animDelay);
        _spriteRenderer.sprite = facing[index];

        // Flips the animation every step when going up or down to switch feet
        if (index == 0 || index == 1)
            _flipAnim = !_flipAnim;

        isMoving = false;
    }

    public bool AttemptWildBattle()
    {
        if (CheckCollision(Vector2.zero, out RaycastHit2D hit, LayerMask.NameToLayer("Wild Battles")))
        {
            Debug.Log("Attempting wild battle");
            // Get wild area's density
            WildArea currentArea = hit.transform.GetComponent<WildArea>();
            int density = currentArea.GetEncounterRate();

            // Check density against random number between 0 and 256
            int densityCheck = Random.Range(0, 256);
            if (densityCheck < density)
            {
                Debug.Log("Wild battle successful");
                MainMenu.CanOpen = false;
                GameStateManager.currentGameState.ExitState();
                GameStateManager.SwitchState(GameStateManager.BattleStateManager);
                BattleStateManager.StartWildBattle(currentArea);
                ClearMovement();
                CanMove = false;
                return true;
            }
        }

        MainMenu.CanOpen = true;
        return false;
    }

    public void SetTeleport(Vector2 targetPosition, GameMap targetMap, Direction stepOff, 
        bool teleportOnWalkOn = true, Direction trigger = Direction.None)
    {
        _teleport = teleportOnWalkOn;
        _teleportTargetPos = targetPosition;
        _teleportTargetMap = targetMap;
        _teleportStepOff = stepOff;
        _teleportTrigger = trigger;

        Debug.Log($"Set teleport to {_teleportTargetPos} on map {_teleportTargetMap.name}");
    }

    private IEnumerator Teleport()
    {
        // Get current step off value (Can change before stepping off when loading and unloading maps)
        Direction stepOff = _teleportStepOff;
        _turn = false;
        _teleport = false;
        _teleportTrigger = Direction.None;
        Teleporting = true;
        isMoving = true;


        // Fade out
        yield return new WaitForSeconds(18 / 60f);

        // Change position to teleport target position
        TargetPosition = _teleportTargetPos;
        transform.position = TargetPosition;
        _rawPosition = TargetPosition;
        _cameraFollow.UpdateCameraPosition();

        // Load new map(s)
        GameMapManager.LoadMap(_teleportTargetMap);

        // Fade in
        yield return new WaitForSeconds(18 / 60f);

        if (stepOff != Direction.None)
            yield return StartCoroutine(Walk(stepOff));

        Teleporting = false;
    }
}