using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBox : MonoBehaviour
{

    #region Private Values
    private Rigidbody _rig;
    private Vector3 _velocity;
    private Collider _collider;
    private float _distToGround;
    private MeshRenderer _renderer;
    #endregion

    #region Public Values
    public float Drag = 100f;
    public float Gravity = 22f;
    public Material OnPressureMaterial;
    public Material OffPressureMaterial;
    public GameObject ToBeDestroyedObject;
    #endregion


    // Use this for initialization
    void Start()
    {
        _rig = gameObject.GetComponent<Rigidbody>();
        _velocity = Vector3.zero;
        _collider = GetComponent<Collider>();
        _distToGround = _collider.bounds.extents.y;
        _renderer = GetComponent<MeshRenderer>();
    }

    private bool OnGround
    {
        get { return Physics.Raycast(transform.position, -Vector3.up, _distToGround + 0.01f); }
    }

    void FixedUpdate()
    {
        _rig.velocity = _velocity;
        _velocity.x *= Mathf.Exp(Drag * -Time.fixedDeltaTime);
        _velocity.z *= Mathf.Exp(Drag * -Time.fixedDeltaTime);

        if (!OnGround)
        {
            _velocity.y += Gravity * -Time.fixedDeltaTime;
            _rig.freezeRotation = false;
        }
        else
        {
            _rig.freezeRotation = true;
            _velocity.y = 0f;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "DetectionGround")
        {
            _renderer.material = OnPressureMaterial;
            Destroy(ToBeDestroyedObject);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "DetectionGround")
        {
            _renderer.material = OffPressureMaterial;
        }
    }
}
