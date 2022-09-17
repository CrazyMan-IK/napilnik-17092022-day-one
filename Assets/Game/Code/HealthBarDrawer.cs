using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DayOne
{
    public class HealthBarDrawer : MonoBehaviour
    {
        [SerializeField] private Mesh _mesh = null;
        [SerializeField] private Material _material = null;

        private Matrix4x4[] _transforms = null;
        private float[] _values = null;
        private List<Health> _targets = null;

        private void Start()
        {
            _targets = FindObjectsOfType<Health>().ToList();

            UpdateArrays();
        }

        private void Update()
        {
            var isAnyDied = false;
            for (int i = 0; i < _targets.Count; i++)
            {
                var target = _targets[i];

                if (target == null || target.Value <= 0)
                {
                    isAnyDied = true;

                    _targets.Remove(target);
                    i--;

                    continue;
                }

                _values[i] = target.Value / target.MaxValue;
                _transforms[i] = Matrix4x4.Translate(target.transform.position);
            }

            if (isAnyDied)
            {
                UpdateArrays();
            }

            _material.SetFloatArray("_Values", _values);

            Graphics.DrawMeshInstanced(_mesh, 0, _material, _transforms);
        }

        private void UpdateArrays()
        {
            _values = new float[_targets.Count];
            _transforms = new Matrix4x4[_targets.Count];

            for (int i = 0; i < _targets.Count; i++)
            {
                var target = _targets[i];

                _values[i] = target.Value / target.MaxValue;
                _transforms[i] = Matrix4x4.Translate(target.transform.position);
            }
        }
    }
}
