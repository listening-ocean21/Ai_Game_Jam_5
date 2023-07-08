using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;


#if ENABLE_INPUT_SYSTEM
[RequireComponent(typeof(PlayerInput))]
#endif
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour, IPlayerController {
    // Public for external hooks
    public Vector3 Velocity { get; private set; }
    public FrameInput Input { get; private set; }
    public bool JumpingThisFrame { get;  set; }
    public bool LandingThisFrame { get; private set; }
    public bool FlyingThisFrame { get; private set; }
    public Vector3 RawMovement { get; private set; }
    public bool Grounded => colDown;

    public int FacingRight { get; set; } = 1;
    
    public float CurrentHorizontalSpeed, CurrentVerticalSpeed;

    public Action OnLanded;
    public Action OnFly;


    // This is horrible, but for some reason colliders are not fully established when update starts...
    private bool active;
    private Vector3 lastPosition;
    private BoxCollider2D boxCollider;




    void Awake()
    {
        Invoke(nameof(Activate), 0.5f);
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.size = characterBounds.size;
        boxCollider.offset = characterBounds.center;
        oneWayLayerID = LayerMask.NameToLayer("OneWayPlatform");
    }
    void Activate() =>  active = true;

    public MyInputAction input;


    public bool CanDash = true;
    public bool CanJump = true;

    private void Update() {
        if(!active) return;
        // Calculate velocity
        Velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
        GatherInput();
        ResetAbility();

        CalculateJumpApex(); // Affects fall speed, so calculate before gravity
        CalculateGravity(); // Vertical movement
    }

    void FixedUpdate()
    {
        if(!active) return;
        RunCollisionChecks();

        MoveCharacter();
    }

    void LateUpdate()
    {
        if(!active) return;
        if (tryFall)
        {
            tryFallDuration -= Time.deltaTime;
        }
        if (tryFallDuration < 0)
        {
            tryFall = false;
        }
    }



    #region Gather Input

    private void GatherInput() {
        Input = new FrameInput {
            JumpDown = input.jump,
            JumpUp = !input.jump,
            X = input.move.x,
            Dash = input.dash,
        };
        if (Input.JumpDown) {
            LastJumpPressed = Time.time;
        }
    }

    #endregion


    private float tryFallDuration = 0.2f;

    void ResetAbility()
    {
        if (Input.JumpUp)
        {
            CanJump = true;
        }
        if (!Input.Dash) 
        { 
            CanDash = true;
        }

        if (input.downJump && !tryFall && groundOneWay)
        {
            tryFall = true;
            tryFallDuration = 0.2f;
            transform.position += Vector3.down * 0.001f;
        }
    }

    #region Collisions

    [Header("COLLISION")] 
    [SerializeField] private Bounds characterBounds;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask oneWayLayer;
    [SerializeField] private int detectorCount = 3;
    [SerializeField] private float detectionRayLength = 0.1f;
    [SerializeField][Range(0.1f, 0.3f)] private float rayBuffer = 0.1f; // Prevents side detectors hitting the ground


    public bool ColUp => colUp;
    public bool ColDown => colDown;
    public bool ColLeft => colLeft;
    public bool ColRight => colRight;


    private RayRange raysUp, raysRight, raysDown, raysLeft;
    private bool colUp, colRight, colDown, colLeft;
    private float timeLeftGrounded;
    private bool groundOneWay = false;



    // We use these raycast checks for pre-collision information
    private void RunCollisionChecks() {
        // Generate ray ranges. 
        CalculateRayRanged();

        // Ground
        LandingThisFrame = false;
        FlyingThisFrame = false;
        var groundedCheck = RunDetection(raysDown, groundLayer | wallLayer | oneWayLayer) 
            && CurrentVerticalSpeed <= 0;
        if (groundedCheck )
        {
            groundOneWay = RunDetection(raysDown, oneWayLayer);
        }
        if (colDown && !groundedCheck)
        {
            timeLeftGrounded = Time.time; // Only trigger when first leaving
            FlyingThisFrame = true;
        }
        else if (!colDown && groundedCheck)
        {
            CoyoteUsable = true; // Only trigger when first touching
            LandingThisFrame = true;
        }
        if (FlyingThisFrame)
        {
            OnFly?.Invoke();
        }
        if (LandingThisFrame)
        {
            OnLanded?.Invoke();
        }
        colDown = groundedCheck;

        // The rest
        colUp = RunDetection(raysUp, groundLayer);
        colLeft = RunDetection(raysLeft, wallLayer);
        colRight = RunDetection(raysRight, wallLayer);

        bool RunDetection(RayRange range, LayerMask layer) {
            return EvaluateRayPositions(range).Any(point => Physics2D.Raycast(point, range.Dir, detectionRayLength, layer));
        }
    }


    private void CalculateRayRanged() {
        // This is crying out for some kind of refactor. 
        var b = new Bounds(transform.position + characterBounds.center, characterBounds.size);

        raysDown = new RayRange(b.min.x + rayBuffer, b.min.y, b.max.x - rayBuffer, b.min.y, Vector2.down);
        raysUp = new RayRange(b.min.x + rayBuffer, b.max.y, b.max.x - rayBuffer, b.max.y, Vector2.up);
        raysLeft = new RayRange(b.min.x, b.min.y + rayBuffer, b.min.x, b.max.y - rayBuffer, Vector2.left);
        raysRight = new RayRange(b.max.x, b.min.y + rayBuffer, b.max.x, b.max.y - rayBuffer, Vector2.right);
    }


    private IEnumerable<Vector2> EvaluateRayPositions(RayRange range) {
        for (var i = 0; i < detectorCount; i++) {
            var t = (float)i / (detectorCount - 1);
            yield return Vector2.Lerp(range.Start, range.End, t);
        }
    }

    private void OnDrawGizmos() {
        // Bounds
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + characterBounds.center, characterBounds.size);

        // Rays
        if (!Application.isPlaying)
        {
            CalculateRayRanged();
            Gizmos.color = Color.blue;
            foreach (var range in new List<RayRange> { raysUp, raysRight, raysDown, raysLeft }) {
                foreach (var point in EvaluateRayPositions(range)) {
                    Gizmos.DrawRay(point, range.Dir * detectionRayLength);
                }
            }
        }

        if (!Application.isPlaying) return;

        // Draw the future position. Handy for visualizing gravity
        Gizmos.color = Color.red;
        var move = new Vector3(CurrentHorizontalSpeed, CurrentVerticalSpeed) * Time.deltaTime;
        Gizmos.DrawWireCube(transform.position + characterBounds.center + move, characterBounds.size);
    }

    #endregion

    #region Walk

    [Header("WALKING")] 
    [SerializeField] private float acceleration = 90;
    [SerializeField] private float moveClamp = 13;
    [SerializeField] private float deAcceleration = 60f;
    [SerializeField] private float apexBonus = 2;

    public void PerformWalk()
    {
        CalculateWalk();
    }

    private void CalculateWalk() {
        if (Input.X != 0) {
            // Set horizontal move speed
            CurrentHorizontalSpeed += Input.X * acceleration * Time.deltaTime;

            // clamped by max frame movement
            CurrentHorizontalSpeed = Mathf.Clamp(CurrentHorizontalSpeed, -moveClamp, moveClamp);

            // Apply bonus at the apex of a jump
            var apexBonus = Mathf.Sign(Input.X) * this.apexBonus * ApexPoint;
            CurrentHorizontalSpeed += apexBonus * Time.deltaTime;
        }
        else {
            // No input. Let's slow the character down
            CurrentHorizontalSpeed = Mathf.MoveTowards(CurrentHorizontalSpeed, 0, deAcceleration * Time.deltaTime);
        }

        if (CurrentHorizontalSpeed > 0 && colRight || CurrentHorizontalSpeed < 0 && colLeft) {
            // Don't walk through walls
            CurrentHorizontalSpeed = 0;
        }
    }

    #endregion

    #region Gravity

    [Header("GRAVITY")] 
    [SerializeField] private float fallClamp = -40f;
    [SerializeField] private float minFallSpeed = 80f;
    [SerializeField] private float maxFallSpeed = 120f;
    private float fallSpeed;

    public float WallSlideModifier = 0.3f;
    public bool IsWallSlide = false;
    public bool IgnoreGravity { private get;  set; }


    private void CalculateGravity() {

        if (colDown && !tryFall) {
            // Move out of the ground
            if (CurrentVerticalSpeed < 0) CurrentVerticalSpeed = 0;
        }
        else if (!IgnoreGravity){
            // Add downward force while ascending if we ended the jump early
            var fallSpeed = EndedJumpEarly && CurrentVerticalSpeed > 0 ? this.fallSpeed * jumpEndEarlyGravityModifier : this.fallSpeed;
            //var fallSpeed = _fallSpeed;


            // Fall
            CurrentVerticalSpeed -= fallSpeed * Time.deltaTime;

            if (IsWallSlide)
            {
                if (CurrentVerticalSpeed < -5)
                    CurrentVerticalSpeed = -5;
            }
            // Clamp
            if (CurrentVerticalSpeed < fallClamp) CurrentVerticalSpeed = fallClamp;
        }
    }

    #endregion

    #region Jump

    [Header("JUMPING")] 
    [SerializeField] private float jumpHeight = 30;
    [SerializeField] private float jumpApexThreshold = 10f;
    [SerializeField] private float coyoteTimeThreshold = 0.1f;
    [SerializeField] private float jumpBuffer = 0.1f;
    [SerializeField] private float jumpEndEarlyGravityModifier = 3;
    public bool CoyoteUsable;
    public bool EndedJumpEarly = true;
    public float ApexPoint; // Becomes 1 at the apex of a jump
    public float LastJumpPressed;
    public bool CanUseCoyote => CoyoteUsable && !colDown && timeLeftGrounded + coyoteTimeThreshold > Time.time;
    public bool HasBufferedJump => colDown && LastJumpPressed + jumpBuffer > Time.time;

    private void CalculateJumpApex() {
        if (!colDown) {
            // Gets stronger the closer to the top of the jump
            ApexPoint = Mathf.InverseLerp(jumpApexThreshold, 0, Mathf.Abs(Velocity.y));
            fallSpeed = Mathf.Lerp(minFallSpeed, maxFallSpeed, ApexPoint);
        }
        else {
            ApexPoint = 0;
        }
    }

    public void PerformJump()
    {
        if (!colDown && Input.JumpUp && !EndedJumpEarly && Velocity.y > 0)
        {
            // _currentVerticalSpeed = 0;
            EndedJumpEarly = true;
        }

        if (colUp)
        {
            if (CurrentVerticalSpeed > 0) CurrentVerticalSpeed = 0;
        }
    }

    private void CalculateJump() {
        // Jump if: grounded or within coyote threshold || sufficient jump buffer
        if (Input.JumpDown && CanUseCoyote || HasBufferedJump) {
            CurrentVerticalSpeed = jumpHeight;
            EndedJumpEarly = false;
            CoyoteUsable = false;
            timeLeftGrounded = float.MinValue;
            JumpingThisFrame = true;
        }
        else {
            JumpingThisFrame = false;
        }


        // End the jump early if button released
        if (!colDown && Input.JumpUp && !EndedJumpEarly && Velocity.y > 0) {
            // _currentVerticalSpeed = 0;
            EndedJumpEarly = true;
        }

        if (colUp)
        {
            if (CurrentVerticalSpeed > 0) CurrentVerticalSpeed = 0;
        }
    }

    #endregion

    #region Move


    [Header("MOVE")] 
    [SerializeField, Tooltip("Raising this value increases collision accuracy at the cost of performance.")]
    private int freeColliderIterations = 10;

    private int oneWayLayerID;
    private bool tryFall = false;

    // We cast our bounds before moving to avoid future collisions
    private void MoveCharacter() {
        var pos = transform.position + characterBounds.center;
        RawMovement = new Vector3(CurrentHorizontalSpeed, CurrentVerticalSpeed); // Used externally
        var move = RawMovement * Time.deltaTime;
        var furthestPoint = pos + move;

        // check furthest movement. If nothing hit, move and don't do extra checks
        var hits = Physics2D.OverlapBoxAll(furthestPoint, characterBounds.size, 0, groundLayer | wallLayer | oneWayLayer);
        transform.position += move;
        if (hits.Length == 0) {
            return;
        }
        if (hits.Length > 2) 
        {
            Debug.LogError("�����м�⵽����2����ײ�����ɫ�ཻ");
        }
        foreach ( var hit in hits )
        {
            var distance = boxCollider.Distance(hit);
            if (hit.gameObject.layer == oneWayLayerID && (CurrentVerticalSpeed > 0 || tryFall))
            {
                continue;
            }
            Vector3 exitEdge = distance.normal * distance.distance;
            transform.position += exitEdge;
        }
    }

    #endregion
}
