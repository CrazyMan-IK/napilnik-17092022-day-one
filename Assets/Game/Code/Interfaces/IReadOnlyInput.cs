using System;
using UnityEngine;

namespace DayOne.Interfaces
{
    public interface IReadOnlyInput
    {
        Transform transform { get; }
        Vector2 Direction { get; }
        bool IsLocked { get; }
    }
}