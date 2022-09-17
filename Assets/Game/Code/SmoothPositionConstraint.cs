using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DayOne
{
    [ExecuteAlways]
    public class SmoothPositionConstraint : MonoBehaviour
    {
        [SerializeField] private Transform _target = null;
        [SerializeField] private Vector3 _offset = Vector3.zero;
        [SerializeField] private Vector3 _speedMultiplier = Vector3.one * 10;

        private void Update()
        {
            if (_target == null)
            {
                return;
            }

            transform.position = Lerp(transform.position, _target.position + _offset, Time.deltaTime * _speedMultiplier);
        }

        private Vector3 Lerp(Vector3 a, Vector3 b, Vector3 t)
        {
            return new Vector3()
            {
                x = Mathf.Lerp(a.x, b.x, t.x),
                y = Mathf.Lerp(a.y, b.y, t.y),
                z = Mathf.Lerp(a.z, b.z, t.z)
            };
        }
    }
}
