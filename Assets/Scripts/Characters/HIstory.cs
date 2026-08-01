// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using UnityEngine;

namespace HickeryDickery.Characters
{
    [System.Serializable]
    public class History
    {
        public Vector3 Position;
        public Vector3 LinearVelocity;
        public Vector3 AngularVelocity;
        public Quaternion Rotation;
        public History Previous;
        public void TravelBackwards(Transform transform, Rigidbody rigidbody)
        {
            transform.position = Position;
            rigidbody.linearVelocity = LinearVelocity;
            rigidbody.angularVelocity = AngularVelocity;
            transform.rotation = Rotation;
        }
    }
}