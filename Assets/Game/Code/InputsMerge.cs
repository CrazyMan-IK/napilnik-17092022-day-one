using System.Collections;
using System.Collections.Generic;
using DayOne.Interfaces;
using UnityEngine;

namespace DayOne
{
    public class InputsMerge : MonoBehaviour, IReadOnlyInput
    {
        private readonly List<Vector2> _inputs = new List<Vector2>();

        public Vector2 Direction { get; private set; } = Vector2.zero;
        public bool IsLocked => false;

        private void LateUpdate()
        {
            Direction = Vector2.zero;

            foreach (var direction in _inputs)
            {
                Direction += direction;
            }

            _inputs.Clear();
        }

        public void AddInput(Vector2 direction)
        {
            _inputs.Add(direction);
        }
    }
}
