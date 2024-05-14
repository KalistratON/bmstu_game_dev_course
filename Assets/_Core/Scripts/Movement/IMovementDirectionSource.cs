using UnityEngine;

using System;

namespace LearnGame.Movement
{
    public interface IMovementDirectionSource
    {
        Vector3 MovementDirection { get; }
        void UpdateMovementDirection (Vector3 targetPosition, bool isDirect = true);
    }
}
