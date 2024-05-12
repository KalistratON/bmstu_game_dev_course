using UnityEngine;

using System;

namespace LearnGame.Movement
{
    public interface IMovementDirectionSource
    {
        Vector3 MovementDirection { get; }
    }
}
