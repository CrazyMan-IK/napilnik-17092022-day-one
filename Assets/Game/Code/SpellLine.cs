using System;
using System.Collections.Generic;
using DayOne.Extensions;
using DayOne.Scriptables;
using UnityEngine;

namespace DayOne
{
    [RequireComponent(typeof(LineRenderer))]
    public class SpellLine : MonoBehaviour
    {
        public event Action Ticked = null;

        private Timer _timer = null;
        private LineRenderer _renderer = null;
        private InputsMerge _target = null;
        private float _forceMultiplier = 1;
        private bool _isAttractor = false;

        private void Awake()
        {
            _renderer = GetComponent<LineRenderer>();

            _timer = new Timer(SpellInformation.TicksPerSecond);
        }

        public void Initialize(InputsMerge target, float forceMultiplier, bool isAttractor)
        {
            _target = target;
            _forceMultiplier = forceMultiplier;
            _isAttractor = isAttractor;
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
            _renderer.SetPosition(0, transform.position);

            //Line logic
            //Physics.Raycast(transform.position, transform.position - _target.position);

            _renderer.SetPosition(_renderer.positionCount - 1, _target.transform.position);

            var direction = (_target.transform.position - transform.position).GetXZ();
            if (_isAttractor)
            {
                direction = -direction;
            }

            direction.Normalize();
            _target.AddInput(direction * _forceMultiplier);

            _timer.Update(Time.deltaTime);
        }

        private void OnTimerTicked()
        {
            Ticked?.Invoke();
        }
    }
}
