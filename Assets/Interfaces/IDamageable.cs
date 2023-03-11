﻿using UnityEngine;

namespace Assets.Interfaces
{
    public interface IDamageable
    {

        public float Health { get; set; }
        public bool Targetable { get; set; }
        public bool Invincible { get; set; }
        public void OnHit(float damage, Vector2 knockback);
        public void OnHit(float damage);
        public void OnObjectDestroyed();

    }
}
