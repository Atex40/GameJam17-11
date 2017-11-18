using UnityEngine;

[RequireComponent (typeof(PlayerController))]
public class Player : MonoBehaviour
{
    private bool _dead = false;
    public bool IsDead { get { return _dead; } private set { _dead = value; } }

    public float MoveSpeed = 5;
<<<<<<< HEAD
    private float _distToGround;
        
=======
    public float SprintSpeed = 8;

>>>>>>> 96f5494b07ecb9059cf20bd9f09ca596baee758d
    private Camera _viewCamera;
    private PlayerController _controller;

    private Collider _collider;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
    }

	private void Start ()
    {
        _viewCamera = Camera.main;
        _collider = GetComponent<Collider>();
        _distToGround = _collider.bounds.extents.y;
    }
	
    private bool OnGround()
    {
        return Physics.Raycast(transform.position, -Vector3.up, _distToGround + 0.01f);
    }

	private void Update () {
        // Movement input
        bool sprint = Input.GetKey(KeyCode.LeftShift);

        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * (sprint ? SprintSpeed : MoveSpeed);
        _controller.Move(moveVelocity, sprint);

        // Jump
        bool jumpInput = Input.GetKeyDown(KeyCode.Space);
        if (jumpInput && OnGround())
            _controller.Jump();

        // Look input

	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ice")
        {
            _collider.material.dynamicFriction = 0;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Ice")
        {
            _collider.material.dynamicFriction = 1;
        }
    }
}
