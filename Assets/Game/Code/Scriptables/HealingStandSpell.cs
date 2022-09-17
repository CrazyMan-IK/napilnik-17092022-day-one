using System;
using UnityEngine;

namespace DayOne.Scriptables
{
    [CreateAssetMenu(fileName = "New HealingStandSpell", menuName = "Day One/Healing Stand Spell", order = 52)]
    public class HealingStandSpell : SpellInformation
    {
        [SerializeField] private float _healPerTick = 5;
        [SerializeField] private float _lifeTime = 15;
        [SerializeField] private float _forceMultiplier = 3;
        [SerializeField] private float _radius = 3;
        [SerializeField] private HealingStand _standPrefab = null;
        [SerializeField] private LayerMask _groundLayer = default;

        public override bool TryUse(Vector2 point, Player player, Action finished)
        {
            if (!Physics.Raycast(Camera.main.ScreenPointToRay(point), out var result, _groundLayer))
            {
                return false;
            }

            var stand = Instantiate(_standPrefab, result.point, Quaternion.identity);
            stand.Initialize(_forceMultiplier, _radius, _healPerTick, _lifeTime);

            return true;
        }
    }
}
