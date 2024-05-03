
using UnityEngine;

namespace LearnGame.Movement
{
    public interface IMovementDirectionSource
    {
        Vector3 MovementDirection { get; }
        bool IsRunning { get; }
    }
}
