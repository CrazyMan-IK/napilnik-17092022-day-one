using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DayOne.Interfaces;
using DayOne.Extensions;

namespace DayOne
{
    public class KeyboardInput : MonoBehaviour, IReadOnlyInput
    {
        public Vector2 Direction { get; private set; } = Vector2.zero;
        public bool IsLocked => false;
        
        private void Update()
        {
            var x = Input.GetAxisRaw("Horizontal");
            var y = Input.GetAxisRaw("Vertical");

            var newDirection = new Vector2(x, y);
            newDirection.Normalize();

            var cameraYRotation = Camera.main.transform.eulerAngles.y;

            Direction = newDirection.Rotated(-cameraYRotation);
        }
    }
}
