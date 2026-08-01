// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using HickeryDickery.Characters.Player;
using UnityEngine;

namespace HickeryDickery.Obstacles
{
    public class LevelEnd : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IPlayer player))
            {
                player.Win();
            }
        }
    }
}