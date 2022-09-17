using System;
using UnityEngine;
using AYellowpaper;
using DayOne.Extensions;
using DayOne.Interfaces;

namespace DayOne
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed = 10;
        [SerializeField] private float _rotationSpeed = 10;
        [SerializeField] private InterfaceReference<IReadOnlyInput> _input = null;

        private Rigidbody _rigidbody = null;
        private Vector3 _lastDirection = Vector3.forward;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (_input.Value == null)
            {
                return;
            }

            var direction = _input.Value.Direction.AsXZ();
            var mag = direction.magnitude;

            if (mag > 0)
            {
                _rigidbody.MoveRotation(Quaternion.Lerp(_rigidbody.rotation, Quaternion.LookRotation(direction, Vector3.up), _rotationSpeed * Time.deltaTime));
            }

            if (mag <= 0.0001f)
            {
                direction = _lastDirection;
                mag = 0;
            }
            _rigidbody.velocity = _maxSpeed * mag * direction;

            if (mag > 0)
            {
                _lastDirection = direction;
            }
        }
    }
}
