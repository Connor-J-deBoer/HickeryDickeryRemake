// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using UnityEngine;

namespace HickeryDickery.Characters.NPC.Hostile
{
    public class Spike : MonoBehaviour
    {
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IKillable player))
            {
                player.Die();
            }
        }
    }
}