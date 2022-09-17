using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DayOne
{
    [RequireComponent(typeof(Health))]
    public class Enemy : MonoBehaviour
    {
        private Health _health;

        private void Awake()
        {
            _health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            _health.Changed += OnHealthChanged;
        }

        private void OnDisable()
        {
            _health.Changed -= OnHealthChanged;
        }

        private void OnHealthChanged(float newValue, float oldValue, float delta)
        {
            if (newValue <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
