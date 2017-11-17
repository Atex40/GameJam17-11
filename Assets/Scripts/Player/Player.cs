using UnityEngine;

[RequireComponent (typeof(PlayerController))]
public class Player : MonoBehaviour
{
    private bool _dead = false;
    public bool IsDead { get { return _dead; } private set { _dead = value; } }

    public float MoveSpeed = 5;

    Camera viewCamera;
    PlayerController controller;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

	private void Start ()
    {
        viewCamera = Camera.main;
	}
	
	private void Update () {
        // Movement input
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * MoveSpeed;
        controller.Move(moveVelocity);

        // Look input

	}
}
