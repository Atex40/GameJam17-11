using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

    private Vector3 _velocity;
    private Rigidbody _myRigidBody;

    public float MaxVelocity = 5f;

	void Start () {
        _myRigidBody = GetComponent<Rigidbody>();
	}

    public void Move(Vector3 velocity)
    {
        _velocity += velocity;
        _velocity = Vector3.ClampMagnitude(_velocity, 5f);
        _myRigidBody.velocity = _velocity;
    }

    public void Jump()
    {
        _myRigidBody.AddForce(new Vector3(0f, 10f, 0f), ForceMode.Impulse);
    }

    public void LookAt(Vector3 lookPoint)
    {
        Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectedPoint);
    }

    void FixedUpdate()
    {
        var vector = _myRigidBody.position + _velocity * Time.fixedDeltaTime;
        //Debug.Log(_velocity);
        //_myRigidBody.MovePosition(vector);
        LookAt(vector);
    }
}
