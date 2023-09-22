using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Inspector
    [SerializeField] private Transform _target;
    [SerializeField] private float _followSpeed;
    [SerializeField] private float _xOffset;
    [SerializeField] private float _yOffset;
    [SerializeField] private float _xBorder;
    [SerializeField] private float _yBorder;
    #endregion

    private void FixedUpdate()
    {
        if (Math.Abs(_target.position.x - transform.position.x+_xOffset) > _xBorder
            || Math.Abs(_target.position.y - transform.position.y+_yOffset) > _yBorder)
        {
            Vector3 newPos = _target.position + new Vector3(_xOffset, _yOffset, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, _followSpeed * Time.deltaTime);
        }
    }
}

