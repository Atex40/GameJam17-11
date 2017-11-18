using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillingPlane : MonoBehaviour
{
    #region Public Var
    public Transform ShadowTransform;
    public Transform PlayerTransform;
    public int MaxTimerValue;
    public float MaxOffset;
    public Direction MovementDirection;
    public DirectionValue Value;
    #endregion

    #region Enums
    public enum Direction
    {
        X,
        Y,
        Z
    };

    public enum DirectionValue
    {
        Positive,
        Negative
    }; 
    #endregion

    #region Private Var
    private float _curOffset;
    private bool _isActive = true;
    #endregion

    void Update()
    {
        if (!_isActive)
            return;
        float posOffset = (Time.deltaTime / MaxTimerValue) * MaxOffset;
        _curOffset += posOffset;
        if (_curOffset > MaxOffset)
            return;

        Vector3 offset = new Vector3();

        switch (MovementDirection)
        {
            case Direction.X:
                offset.x = posOffset;
                break;
            case Direction.Y:
                offset.y = posOffset;
                break;
            case Direction.Z:
                offset.z = posOffset;
                break;
        }

        Vector3 newPos = new Vector3();

        switch (Value)
        {
            case DirectionValue.Positive:
                newPos = ShadowTransform.position + offset;
                break;
            case DirectionValue.Negative:   
                newPos = ShadowTransform.position - offset;
                break;
        }
        ShadowTransform.transform.position = newPos;
    }

    void OnTriggerEnter(Collider other)
    {
        this._isActive = false;
    }


}
