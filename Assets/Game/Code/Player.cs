using System;
using UnityEngine;
using DayOne.UI;

namespace DayOne
{
    [RequireComponent(typeof(MoveDetector))]
    public class Player : MonoBehaviour
    {
        public event Action<Player> Moved = null;

        [SerializeField] private MouseInputPanel _inputPanel = null;
        [SerializeField] private SpellsList _spellsList = null;

        private MoveDetector _moveDetector = null;

        private void Awake()
        {
            _moveDetector = GetComponent<MoveDetector>();
        }

        private void OnEnable()
        {
            _inputPanel.Clicked += OnMouseClicked;
            _moveDetector.Moved += OnMoved;
        }

        private void OnDisable()
        {
            _inputPanel.Clicked -= OnMouseClicked;
            _moveDetector.Moved -= OnMoved;
        }

        private void OnMouseClicked(Vector2 point)
        {
            var button = _spellsList.SelectedButton;

            if (button == null)
            {
                return;
            }

            Moved?.Invoke(this);
            button.Use(point, this);
        }

        private void OnMoved()
        {
            Moved?.Invoke(this);
        }
    }
}
