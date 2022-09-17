using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DayOne
{
    public class Health : MonoBehaviour
    {
        public event Action<float, float, float> Changed = null;

        [SerializeField] private float _maxValue = 100;

        public float Value { get; private set; } = 0;

        private void Awake()
        {
            Value = _maxValue;
        }

        public void Heal(float amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }

            ChangeValue(amount);
        }

        public void Damage(float amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }

            ChangeValue(-amount);
        }

        private void ChangeValue(float amount)
        {
            var lastValue = Value;
            Value = Mathf.Clamp(Value + amount, 0, _maxValue);

            Changed?.Invoke(Value, lastValue, Value - lastValue);
        }
    }
}
