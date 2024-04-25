﻿using LearnGame.Shooting;
using UnityEngine;
using System.Collections;
using LearnGame.Bonus;

namespace LearnGame.PickUp
{
    public class PickUpAcceleration : PickUpItem
    {
        [SerializeField]
        Acceleration acceleration;

        public override void PickUp(BaseCharacter character)
        {
            base.PickUp(character);
            character.SetBonusAcceleration(acceleration.Scale, acceleration.Seconds);
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}