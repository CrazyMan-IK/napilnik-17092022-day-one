using System;
using DayOne.Scriptables;
using UnityEngine;
using UnityEngine.UI;

namespace DayOne.UI
{
    [RequireComponent(typeof(Button))]
    public class SpellButton : MonoBehaviour
    {
        public event Action<SpellButton> Selected = null;

        [SerializeField] private Image _view = null;
        [SerializeField] private Image _culldownOverlay = null;

        private Button _button = null;
        private SpellInformation _spell = null;
        private Timer _culldownTimer = null;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        public void Initialize(SpellInformation spell)
        {
            if (_spell != null)
            {
                throw new InvalidOperationException("Re-initialization not possible");
            }

            if (spell == null)
            {
                throw new ArgumentNullException(nameof(spell));
            }

            _spell = spell;

            _view.sprite = spell.Icon;

            _culldownTimer = new Timer(spell.Culldown);
            _culldownTimer.Pause();

            _culldownTimer.Ticked += OnCulldownTicked;
        }

        private void OnEnable()
        {
            _button.Clicked += OnButtonClicked;
        }

        private void OnDisable()
        {
            _button.Clicked -= OnButtonClicked;
        }

        private void Update()
        {
            _culldownTimer?.Update(Time.deltaTime);
            _culldownOverlay.fillAmount = 1 - _culldownTimer.TimeLeftPercentage;
        }

        public void Use(Vector2 point, Player player)
        {
            if (_spell.TryUse(point, player, OnSpellFinished))
            {
                _culldownOverlay.enabled = true;
            }
        }

        private void OnButtonClicked()
        {
            Selected?.Invoke(this);
        }

        private void OnCulldownTicked()
        {
            _culldownTimer.Stop();
            _culldownOverlay.enabled = false;
        }

        private void OnSpellFinished()
        {
            _culldownTimer.Start();
        }
    }
}
