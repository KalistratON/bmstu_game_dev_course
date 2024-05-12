﻿using UnityEngine;

using System;

namespace LearnGame.Movement
{

    public class DummyDirectionController : MonoBehaviour, IMovementDirectionSource
    {
        public event Action<float> OnRunning;


        public Vector3 MovementDirection { get; private set; }

        protected void Awake()
        {
            MovementDirection = Vector3.zero;
        }
    }
}