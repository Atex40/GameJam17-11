using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    private float _drag;
    public float BaseDrag = 5f;
    public float IceDrag = 10f;
    public float Gravity = 9.81f;

    public float JumpSpeed = 5f;
    public float JumpTime = 0.1f;
    private float _curJumpTime = 0f;
    private bool _isJumping = false;

    private Vector3 _velocity = Vector3.zero;
    private Rigidbody _myRigidBody;
    private Collider _collider;
    private Animator _animator;

    private float _distToGround;

    public float MaxVelocity = 5f;
    public float MaxSprintVelocity = 8f;

    private Vector3 _lastPoisition;

    void Start()
    {
        _myRigidBody = GetComponent<Rigidbody>();
        _drag = BaseDrag;
        _collider = GetComponent<Collider>();
        _animator = GetComponent<Animator>();
        _distToGround = _collider.bounds.extents.y;
    }

    private bool OnGround
    {
        get { return Physics.Raycast(transform.position, -Vector3.up, _distToGround + 0.001f); }
    }

    public void Move(Vector3 velocity, bool sprint)
    {
        _velocity += velocity;
        if (velocity != Vector3.zero)
        {
            _animator.SetBool("IsWalking", true);
        }
        else
        {
            _animator.SetBool("IsWalking", false);
        }

        if (!sprint)
        {
            _velocity.x = Mathf.Clamp(_velocity.x, -MaxVelocity, MaxVelocity);
            _velocity.z = Mathf.Clamp(_velocity.z, -MaxVelocity, MaxVelocity);
        }
        else
        {
            _velocity.x = Mathf.Clamp(_velocity.x, -MaxSprintVelocity, MaxSprintVelocity);
            _velocity.z = Mathf.Clamp(_velocity.z, -MaxSprintVelocity, MaxSprintVelocity);
        }
    }

    public void Jump()
    {
        if (OnGround && !_isJumping)
        {
            _animator.SetTrigger("JumpTrigger");
            _isJumping = true;
            _curJumpTime = 0f;
        }
    }

    public void LookAt(Vector3 lookPoint)
    {
        Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectedPoint);
    }

    void FixedUpdate()
    {
        var vector = _myRigidBody.position + _velocity * Time.fixedDeltaTime;
        _myRigidBody.velocity = _velocity;
        _velocity.x *= Mathf.Exp(_drag * -Time.fixedDeltaTime);
        if (_isJumping)
        {
            _curJumpTime += Time.fixedDeltaTime;
            if (_curJumpTime < JumpTime)
                _velocity.y += JumpSpeed * Vector3.up.y;
            else
                _isJumping = false;
        }
        else
        {
            if (!OnGround)
                _velocity.y += Gravity * -Time.fixedDeltaTime;
            else
            {
                _velocity.y = 0f;
                _lastPoisition = transform.position;
            }
        }
        _velocity.z *= Mathf.Exp(_drag * -Time.fixedDeltaTime);
        LookAt(vector);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ice")
        {
            _drag = IceDrag;
        }
        else
        {
            _drag = BaseDrag;
        }
    }
}
