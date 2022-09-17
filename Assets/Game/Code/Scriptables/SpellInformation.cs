using System;
using UnityEngine;

namespace DayOne.Scriptables
{
    public abstract class SpellInformation : ScriptableObject
    {
        [field: SerializeField] public Sprite Icon { get; private set; } = null;
        [field: SerializeField] public float Culldown { get; private set; } = 1;

        public abstract bool TryUse(Vector2 point, Player player, Action finished);
    }
}
