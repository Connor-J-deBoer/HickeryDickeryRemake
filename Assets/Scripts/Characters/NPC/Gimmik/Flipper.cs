// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using UnityEngine;

namespace HickeryDickery.Characters.NPC.Gimmik
{
    public class Flipper : AGimmik
    {
        protected override void Gimmik(Rigidbody rigidbody, Collider collider, RaycastHit hit)
        {
            Physics.gravity = -Physics.gravity;
        }
    }
}