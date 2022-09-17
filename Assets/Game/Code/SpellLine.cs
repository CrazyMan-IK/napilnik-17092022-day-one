using System;
using System.Collections.Generic;
using DayOne.Extensions;
using UnityEngine;

namespace DayOne
{
    [RequireComponent(typeof(LineRenderer))]
    public class SpellLine : MonoBehaviour
    {
        public event Action Ticked = null;

        [SerializeField] private float _forceMultiplier = 1;

        private readonly Timer _timer = new Timer(1);
        private LineRenderer _renderer = null;
        private InputsMerge _target = null;
        private bool _isAttractor = false;

        private void Awake()
        {
            _renderer = GetComponent<LineRenderer>();
        }

        public void Initialize(InputsMerge target, bool isAttractor)
        {
            _target = target;
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
