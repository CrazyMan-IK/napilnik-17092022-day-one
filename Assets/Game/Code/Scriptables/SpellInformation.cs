using System;
using UnityEngine;

namespace DayOne.Scriptables
{
    public abstract class SpellInformation : ScriptableObject, ISerializationCallbackReceiver
    {
        private static float _globalTicksPerSecond = 4;

        [Header("Global")]
        [SerializeField] private int _ticksPerSecond = 4;

        [field: Space]
        [field: SerializeField] public Sprite Icon { get; private set; } = null;
        [field: SerializeField] public float Culldown { get; private set; } = 1;

        public static float TicksPerSecond => _globalTicksPerSecond;

        public void OnBeforeSerialize()
        {
            _ticksPerSecond = Mathf.RoundToInt(1.0f / _globalTicksPerSecond);
        }

        public void OnAfterDeserialize()
        {
            _globalTicksPerSecond = 1.0f / _ticksPerSecond;
        }

        public abstract bool TryUse(Vector2 point, Player player, Action finished);
    }
}
