// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using UnityEngine;

namespace HickeryDickery.Characters.NPC.Gimmik
{
    public class Spring : AGimmik
    {
        [SerializeField] private float _springForce;

        protected override void Gimmik(Rigidbody rigidbody, Collider collider, RaycastHit hit)
        {
            rigidbody.AddForce(hit.normal.normalized * _springForce * Time.deltaTime, ForceMode.Impulse);
        }
    }
}