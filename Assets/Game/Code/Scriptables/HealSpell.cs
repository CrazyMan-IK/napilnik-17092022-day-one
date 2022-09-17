using System;
using UnityEngine;

namespace DayOne.Scriptables
{
    [CreateAssetMenu(fileName = "New HealSpellInformation", menuName = "Day One/Heal Spell", order = 51)]
    public class HealSpell : SpellInformation
    {
        [field: SerializeField] public float HealPerTick { get; private set; } = 5;
        [field: SerializeField] public SpellLine LinePrefab { get; private set; } = null;

        public override bool TryUse(Vector2 point, Player player, Action finished)
        {
            if (!Physics.Raycast(Camera.main.ScreenPointToRay(point), out var result) || !result.transform.TryGetComponent<Health>(out var health) || !result.transform.TryGetComponent<InputsMerge>(out var inputsMerge))
            {
                return false;
            }

            var line = Instantiate(LinePrefab, player.transform);
            line.Initialize(inputsMerge, true);

            player.Moved += onPlayerMoved;
            line.Ticked += onTimerTicked;

            return true;

            void onTimerTicked() => OnTimerTicked(health, player, line, finished, onPlayerMoved, onTimerTicked);
            void onPlayerMoved(Player player) => OnPlayerMoved(player, line, finished, onPlayerMoved, onTimerTicked);
        }

        private void OnPlayerMoved(Player player, SpellLine line, Action finished, Action<Player> onPlayerMoved, Action onTimerTicked)
        {
            player.Moved -= onPlayerMoved;
            line.Ticked -= onTimerTicked;

            Destroy(line.gameObject);

            finished();
        }

        private void OnTimerTicked(Health health, Player player, SpellLine line, Action finished, Action<Player> onPlayerMoved, Action onTimerTicked)
        {
            health.Heal(HealPerTick);

            if (health.Value > 0)
            {
                return;
            }

            OnPlayerMoved(player, line, finished, onPlayerMoved, onTimerTicked);
        }
    }
}
