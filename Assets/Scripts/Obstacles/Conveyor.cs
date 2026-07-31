// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using UnityEngine;

namespace HickeryDickery.Obstacles
{
    public class Conveyor : MonoBehaviour
    {
        [SerializeField] private int _conveyCountMax = 100;
        [SerializeField] private float _speed = 2;
        [SerializeField] private float _acceleration = 10;
        [HideInInspector] [SerializeField] private Vector3 _center;
        [HideInInspector] [SerializeField]  private Vector3 _halfExtends;
        [HideInInspector] [SerializeField] BoxCollider _collider;
        private Collider[] _cashedConvey;
        private void OnValidate()
        {
            _collider = GetComponent<BoxCollider>();
            CalculateBox();
        }
        private void Awake()
        {
            _cashedConvey = new Collider[_conveyCountMax];
        }

        private void FixedUpdate()
        {
            int count = Physics.OverlapBoxNonAlloc(_center, _halfExtends, _cashedConvey, transform.rotation);
            for (int i = 0; i < count; ++i)
            {
                if (_cashedConvey[i] == null)
                    continue;
                if (!_cashedConvey[i].gameObject.TryGetComponent(out Rigidbody rb))
                    continue;
                if (rb.linearVelocity.magnitude > _speed)
                    continue;
                // The overlap box isn't over our collider
                if (!Physics.Raycast(rb.position, Physics.gravity.normalized, out RaycastHit hit))
                    continue;
                    
                Vector3 conveyForward = Vector3.ProjectOnPlane(transform.right, hit.normal);
                Vector3 closestPoint = _cashedConvey[i].ClosestPoint(hit.point);
                rb.AddForceAtPosition(conveyForward * _acceleration, closestPoint);
            }
        }
        private void CalculateBox()
        {
            Vector3 halfExtend = _collider.size * 0.5f;
            halfExtend.y = 0.1f;
            _halfExtends = Vector3.Scale(halfExtend, transform.lossyScale);
            Vector3 center = _collider.center;
            center.y = _collider.size.y / 2;
            _center = transform.position + (transform.rotation * Vector3.Scale(center, transform.lossyScale));
        }
        private void OnDrawGizmosSelected()
        {
            CalculateBox();
            Gizmos.color = Color.green;
            // Cache the standard gizmo matrix
            Matrix4x4 oldMatrix = Gizmos.matrix;
            // Rotate the gizmo drawer to match your transform
            Gizmos.matrix = Matrix4x4.TRS(_center, transform.rotation, Vector3.one);
            // Draw the wire cube at local (0,0,0) relative to the shifted matrix
            Gizmos.DrawWireCube(Vector3.zero, _halfExtends * 2f);
            // Restore the standard matrix
            Gizmos.matrix = oldMatrix;
        }
    }
}