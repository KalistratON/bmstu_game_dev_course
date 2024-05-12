using UnityEngine;

using System;

namespace LearnGame.Movement
{
    public interface IMovementDirectionSource
    {
        event Action<float> OnRunning;

        Vector3 MovementDirection { get; }
    }
}
