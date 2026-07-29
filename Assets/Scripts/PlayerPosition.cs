// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using UnityEngine;

namespace HickeryDickery
{
    [System.Serializable]
    public class PlayerPosition
    {
        public Vector3 Position;
        public Vector3 LinearVelocity;
        public Vector3 AngularVelocity;
        public Quaternion Rotation;
        public PlayerPosition Previous;
        public (Vector3, Vector3, Vector3, Quaternion) GetProperties()
        {
            return (Position, LinearVelocity, AngularVelocity, Rotation);
        }
    }
}