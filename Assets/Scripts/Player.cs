using UnityEngine;

public class Player : MonoBehaviour
{

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public PlayerInputSet input { get; private set; }

    [Header("Player States")]
    private StateMachine stateMachine;
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }


    [Header("Movement Details")]
    public Vector2 moveInput { get; private set; }
    public float moveSpeed;
    public float jumpForce = 5f;
    private bool facingRight = true;


    [Header("Collision Detection")]
    [SerializeField] private float groundCheckDistance;
    public bool isGrounded;
    [SerializeField] private LayerMask whatIsGround;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>(); // Call it before assigning component inside of the state
        rb = GetComponent<Rigidbody2D>();

        input = new PlayerInputSet();
        stateMachine = new StateMachine();

        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
        jumpState = new Player_JumpState(this, stateMachine, "jumpFall");
        fallState = new Player_FallState(this, stateMachine, "jumpFall");
    }

    void OnEnable()
    {
        input.Enable();

        // input.Player.Movement.started --> Input Just begun
        // input.Player.Movement.performed --> Input is performed 
        // input.Player.Movement.canceled --> Input stops, when you release the key

        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => moveInput = Vector2.zero;

    }

    void OnDisable()
    {
        input.Disable();
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        HandleCollisionDetection();
        stateMachine.UpdateActiveState();
    }

    public void setVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip();
    }

    private void HandleFlip()
    {
        if (facingRight && rb.linearVelocityX < 0)
            Flip();
        else if (!facingRight && rb.linearVelocityX > 0)
            Flip();
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;

    }


    private void HandleCollisionDetection()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
    }


}
