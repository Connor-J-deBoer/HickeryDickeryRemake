// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using UnityEngine;

namespace HickeryDickery.Characters.NPC.Gimmik
{
    public class Conveyor : AGimmik
    {
        [SerializeField] private float _speed = 2;
        [SerializeField] private float _acceleration = 10;

        protected override void Gimmik(Rigidbody rigidbody, Collider collider, RaycastHit hit)
        {
            if (rigidbody.linearVelocity.magnitude >= _speed)
                return;
            Vector3 conveyForward = Vector3.ProjectOnPlane(transform.right, hit.normal);
            Vector3 closestPoint = collider.ClosestPoint(hit.point);
            rigidbody.AddForceAtPosition(conveyForward * _acceleration, closestPoint);
        }
    }
}