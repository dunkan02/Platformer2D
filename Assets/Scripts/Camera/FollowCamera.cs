using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 8f;

    private void Start()
    {
        if (_target == null)
            _target = transform;
    }

    private void Update()
    {
        Vector3 nextStepPosition = Vector3.Lerp(transform.position, _target.position, Time.deltaTime * _speed);
        transform.position = new Vector3(nextStepPosition.x, nextStepPosition.y, transform.position.z);
    }
}
