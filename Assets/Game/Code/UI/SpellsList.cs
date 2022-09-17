using System.Collections.Generic;
using UnityEngine;
using DayOne.Scriptables;

namespace DayOne.UI
{
    public class SpellsList : MonoBehaviour
    {
        [SerializeField] private SpellButton _spellButtonPrefab = null;
        [SerializeField] private List<SpellInformation> _spells = new List<SpellInformation>();

        private readonly List<SpellButton> _buttons = new List<SpellButton>();

        public SpellButton SelectedButton { get; private set; } = null;

        private void Awake()
        {
            foreach (var spell in _spells)
            {
                var button = Instantiate(_spellButtonPrefab, transform);

                button.Initialize(spell);

                _buttons.Add(button);
            }
        }

        private void OnEnable()
        {
            foreach (var button in _buttons)
            {
                button.Selected += OnSpellSelected;
            }
        }

        private void OnDisable()
        {
            foreach (var button in _buttons)
            {
                button.Selected -= OnSpellSelected;
            }
        }

        private void OnSpellSelected(SpellButton button)
        {
            SelectedButton = button;
        }
    }
}
