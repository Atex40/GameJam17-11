using UnityEngine;

[RequireComponent (typeof(PlayerController))]
public class Player : MonoBehaviour
{
    private bool _dead = false;
    public bool IsDead { get { return _dead; } private set { _dead = value; } }

    public float MoveSpeed = 5;
    public float SprintSpeed = 8;

    private Camera _viewCamera;
    private PlayerController _controller;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
    }

	private void Start ()
    {
        _viewCamera = Camera.main;
    }

	private void Update () {
        // Movement input
        bool sprint = Input.GetKey(KeyCode.LeftShift);

        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * (sprint ? SprintSpeed : MoveSpeed);
        _controller.Move(moveVelocity, sprint);

        // Jump
        bool jumpInput = Input.GetKeyDown(KeyCode.Space);
        if (jumpInput)
            _controller.Jump();

        // Look input

	}
}
