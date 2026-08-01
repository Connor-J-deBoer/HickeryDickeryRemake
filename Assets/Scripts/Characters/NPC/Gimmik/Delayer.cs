// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using HickeryDickery.Characters.Player;
using UnityEngine;

namespace HickeryDickery.Characters.NPC.Gimmik
{
    public class Delayer : AGimmik
    {
        [SerializeField] private float _delay = 1f;

        protected override void Gimmik(Rigidbody rigidbody, Collider collider, RaycastHit hit)
        {
            GameObject gameObjectToKillLater = collider.gameObject;
            if (!gameObjectToKillLater.TryGetComponent(out IKillable thingToKillLater) || gameObjectToKillLater.TryGetComponent(out TouchOfDeath _))
                return;

            TouchOfDeath touchOfDeath = collider.gameObject.AddComponent<TouchOfDeath>();
            touchOfDeath.Delay = _delay;
            touchOfDeath.ThingToKill = thingToKillLater;
        }
    }
}