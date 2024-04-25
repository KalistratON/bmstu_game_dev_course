﻿using UnityEngine;
using System.Collections;
using System;

namespace LearnGame.PickUp {
    public abstract class PickUpItem : MonoBehaviour
    {
        public event Action<PickUpItem> OnPickedUp;
        
        public virtual void PickUp(BaseCharacter character)
        {
            OnPickedUp.Invoke(this);
        }
    }
}
