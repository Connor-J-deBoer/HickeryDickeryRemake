// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using UnityEngine;

namespace HickeryDickery.Obstacles
{
    public class Spring : MonoBehaviour
    {
        [SerializeField] private float _springForce = 1000;
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Rigidbody _rb))
            {
                _rb.AddForce(new Vector3(0, _springForce) * Time.deltaTime, ForceMode.Impulse);
            }
        }
    }
}