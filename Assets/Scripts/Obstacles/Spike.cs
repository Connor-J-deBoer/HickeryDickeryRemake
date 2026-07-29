// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using HickeryDickery.Player;
using UnityEngine;

namespace HickeryDickery.Obstacles
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