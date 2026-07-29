// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using UnityEngine;

namespace HickeryDickery.Obstacles
{
    public class Flipper : MonoBehaviour
    {
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Rigidbody _rb))
            {
                Physics.gravity = -Physics.gravity;
            }
        }
    }
}