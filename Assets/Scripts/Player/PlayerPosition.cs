// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using System;
using UnityEngine;

namespace HickeryDickery.Player
{
    [System.Serializable]
    public class History
    {
        public Vector3 Position;
        public Vector3 LinearVelocity;
        public Vector3 AngularVelocity;
        public Quaternion Rotation;
        public History Previous;
        public (Vector3, Vector3, Vector3, Quaternion) GetProperties()
        {
            return (Position, LinearVelocity, AngularVelocity, Rotation);
        }

        internal void TravelBackwards(Transform transform, Rigidbody rigidbody)
        {
            transform.position = Position;
            rigidbody.linearVelocity = LinearVelocity;
            rigidbody.angularVelocity = AngularVelocity;
            transform.rotation = Rotation;
        }
    }
}