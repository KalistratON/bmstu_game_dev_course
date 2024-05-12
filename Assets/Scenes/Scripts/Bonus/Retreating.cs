using LearnGame.Timer;

using UnityEngine;

using System;

namespace LearnGame.Property
{
    public class Retreating : Property
    {
        public float RetreatingSpeed { get; set; }


        public Retreating (float theSpeed)
        {
            RetreatingSpeed = theSpeed;
        }
    }
}