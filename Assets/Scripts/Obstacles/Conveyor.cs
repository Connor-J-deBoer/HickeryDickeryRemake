// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using UnityEngine;

namespace HickeryDickery.Obstacles
{
    public class Conveyor : MonoBehaviour
    {
        [SerializeField] private float _speed = 2;
        
        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Rigidbody _rb))
            {
                _rb.AddForceAtPosition(new Vector3(_speed, 0) * Time.deltaTime, collision.GetContact(0).point);
            }
        }
    }
}