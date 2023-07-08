using UnityEngine;
using Random = UnityEngine.Random;
using Lightbug.Utilities;

/// <summary>
/// This is a pretty filthy script. I was just arbitrarily adding to it as I went.
/// You won't find any programming prowess here.
/// This is a supplementary script to help with effects and animation. Basically a juice factory.
/// </summary>
public class PlayerAnimator : MonoBehaviour {
    [SerializeField] private Animator _anim;
    [SerializeField] private AudioSource _source;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private ParticleSystem _jumpParticles, _launchParticles;
    [SerializeField] private ParticleSystem _moveParticles, _landParticles;
    [SerializeField] private AudioClip[] _footsteps;
    [SerializeField] private float _maxTilt = .1f;
    [SerializeField] private float _tiltSpeed = 1;
    [SerializeField, Range(1f, 3f)] private float _maxIdleSpeed = 2;
    [SerializeField] private float _maxParticleFallSpeed = -40;

    [SerializeField] private RuntimeAnimatorController _jumpController;

    private IPlayerController _player;
    private bool _playerGrounded;
    private ParticleSystem.MinMaxGradient _currentGradient;
    private Vector2 _movement;
    private StateController StateController;

    void Awake()
    {
        _player = GetComponentInParent<IPlayerController>();
        StateController = this.GetComponentInBranch<PlayerController, StateController>();
    }

    //void Update() {
    //    if (_player == null) return;

    //    // Flip the sprite
    //    if (_player.Input.X != 0)
    //    {
    //        int facingRight = _player.Input.X > 0 ? 1 : -1;
    //    if (StateController.CurrentState.GetType() != typeof(DashState))
    //    {
    //        transform.localScale = new Vector3(facingRight, 1, 1);
    //        _player.FacingRight = facingRight;
    //    }
    //    }
    //    // Lean while running
    //    var targetRotVector = new Vector3(0, 0, Mathf.Lerp(-_maxTilt, _maxTilt, Mathf.InverseLerp(-1, 1, _player.Input.X)));
    //    _anim.transform.rotation = Quaternion.RotateTowards(_anim.transform.rotation, Quaternion.Euler(targetRotVector), _tiltSpeed * Time.deltaTime);



    //    // Splat
    //    if (_player.LandingThisFrame) {
    //        //_anim.SetTrigger(GroundedKey);
    //        //_anim.SetBool(GroundedKey, true);
    //        _source.PlayOneShot(_footsteps[Random.Range(0, _footsteps.Length)]);
    //    }

    //    // Jump effects
    //    if (_player.JumpingThisFrame) {
    //        _anim.runtimeAnimatorController = _jumpController;

    //        _anim.SetBool(GroundedKey, _player.Grounded);
    //        //_anim.ResetTrigger(GroundedKey);
    //        //_anim.SetBool(GroundedKey, false);


    //        // Only play particles when grounded (avoid coyote)
    //        if (_player.Grounded) {
    //            SetColor(_jumpParticles);
    //            SetColor(_launchParticles);
    //            _jumpParticles.Play();
    //        }
    //    }

    //    // Play landing effects and begin ground movement effects
    //    if (!_playerGrounded && _player.Grounded) {
    //        _playerGrounded = true;
    //        _moveParticles.Play();
    //        _landParticles.transform.localScale = Vector3.one * Mathf.InverseLerp(0, _maxParticleFallSpeed, _movement.y);
    //        SetColor(_landParticles);
    //        _landParticles.Play();
    //    }
    //    else if (_playerGrounded && !_player.Grounded) {
    //        _playerGrounded = false;
    //        _moveParticles.Stop();
    //    }

    //    // Detect ground color
    //    var groundHit = Physics2D.Raycast(transform.position, Vector3.down, 2, _groundMask);
    //    if (groundHit && groundHit.transform.TryGetComponent(out SpriteRenderer r)) {
    //        _currentGradient = new ParticleSystem.MinMaxGradient(r.color * 0.9f, r.color * 1.2f);
    //        SetColor(_moveParticles);
    //    }

    //    _movement = _player.RawMovement; // Previous frame movement is more valuable
    //}

    private void OnDisable() {
        _moveParticles.Stop();
    }

    private void OnEnable() {
        _moveParticles.Play();
    }

    void SetColor(ParticleSystem ps) {
        var main = ps.main;
        main.startColor = _currentGradient;
    }

    #region Animation Keys

    public static readonly int GroundedKey = Animator.StringToHash("Grounded");
    public static readonly int IdleSpeedKey = Animator.StringToHash("IdleSpeed");
    public static readonly int JumpKey = Animator.StringToHash("Jump");
    public static readonly int JumpTimesKey = Animator.StringToHash("JumpTimes");
    public static readonly int FacingRightKey = Animator.StringToHash("FacingRight");
    public static readonly int WallJumpKey = Animator.StringToHash("WallJump");

    #endregion
}
