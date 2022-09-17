using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DayOne
{
    public class MoveDetector : MonoBehaviour
    {
        public event Action Moved = null;

        private Vector3 _lastPosition = Vector3.zero;

        private void Awake()
        {
            _lastPosition = transform.position;
        }

        private void Update()
        {
            var currentPosition = transform.position;

            if (currentPosition != _lastPosition)
            {
                Moved?.Invoke();
            }

            _lastPosition = currentPosition;
        }
    }
}
