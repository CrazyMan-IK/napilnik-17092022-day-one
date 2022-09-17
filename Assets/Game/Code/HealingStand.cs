using System;
using System.Collections.Generic;
using UnityEngine;
using DayOne.Extensions;
using DayOne.Scriptables;

namespace DayOne
{
    public class HealingStand : MonoBehaviour
    {
        public event Action Destroyed = null;

        private float _forceMultiplier = 1;
        private float _radius = 5;
        private float _healPerTick = 5;

        private Timer _timer = null;
        private Timer _lifeTimer = null;

        private void Awake()
        {
            _timer = new Timer(SpellInformation.TicksPerSecond);
        }

        public void Initialize(float forceMultiplier, float radius, float healPerTick, float lifeTime)
        {
            _forceMultiplier = forceMultiplier;
            _radius = radius;
            _healPerTick = healPerTick;

            _lifeTimer = new Timer(lifeTime);
            _lifeTimer.Ticked += OnLifeTimerTicked;
        }

        private void OnEnable()
        {
            _timer.Ticked += OnTimerTicked;
        }

        private void OnDisable()
        {
            _timer.Ticked -= OnTimerTicked;
        }

        private void Update()
        {
            foreach (var target in GetNearComponents<InputsMerge>())
            {
                var direction = (transform.position - target.transform.position).GetXZ();
                direction.Normalize();

                target.AddInput(direction * _forceMultiplier);
            }

            _timer.Update(Time.deltaTime);
            _lifeTimer.Update(Time.deltaTime);
        }

        private IEnumerable<T> GetNearComponents<T>()
        {
            var colliders = Physics.OverlapSphere(transform.position, _radius);

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponentInParent<T>(out var target))
                {
                    yield return target;
                }
            }
            
            yield break;
        }

        private void OnTimerTicked()
        {
            foreach (var target in GetNearComponents<Health>())
            {
                target.Heal(_healPerTick);
            }
        }

        private void OnLifeTimerTicked()
        {
            _lifeTimer.Ticked -= OnLifeTimerTicked;

            Destroy(gameObject);

            Destroyed?.Invoke();
        }
    }
}
