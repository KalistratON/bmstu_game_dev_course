﻿using UnityEngine;
using System.Collections;

namespace LearnGame
{
    public static class LayerUtils
    {
        public const string BulletLayerName = "Bullet";
        public const string EnemyLayerName = "Enemy";
        public const string PickUpLayerName = "PickUp";

        public static readonly int BulletLayer = LayerMask.NameToLayer(BulletLayerName);
        public static readonly int PickUpLayer = LayerMask.NameToLayer(PickUpLayerName);

        public static readonly int EnemyMask = LayerMask.GetMask(EnemyLayerName);

        public static bool IsBullet(GameObject other) => other.layer == BulletLayer;
        public static bool IsPickUp(GameObject other) => other.layer == PickUpLayer;
    }
}