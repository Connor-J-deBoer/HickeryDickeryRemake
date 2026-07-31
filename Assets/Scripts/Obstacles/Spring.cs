// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

using UnityEngine;

namespace HickeryDickery.Obstacles
{
    public class Spring : MonoBehaviour
    {
        [SerializeField] private int _springCountMax = 5;

        [SerializeField] private float _springForce = 100;
        [HideInInspector] [SerializeField] private Vector3 _center;
        [HideInInspector] [SerializeField]  private Vector3 _halfExtends;
        [HideInInspector] [SerializeField] BoxCollider _collider;
        private Collider[] _cashedSpring;
        private void OnValidate()
        {
            _collider = GetComponent<BoxCollider>();
            CalculateBox();
        }
        private void Awake()
        {
            _cashedSpring = new Collider[_springCountMax];
        }

        private void Update()
        {
            Physics.OverlapBoxNonAlloc(_center, _halfExtends, _cashedSpring, transform.rotation);
            for (int i = 0; i < _cashedSpring.Length; ++i)
            {
                // The overlap box did populate this pool item
                if (_cashedSpring[i] == null)
                    continue;
                // The overlap box didn't find a physics object
                if (!_cashedSpring[i].gameObject.TryGetComponent(out Rigidbody rb))
                    continue;
                // The overlap box isn't over our collider
                if (!Physics.Raycast(rb.position, (transform.position - rb.position).normalized, out RaycastHit hit, 1))
                    continue;
                rb.AddForce(hit.normal * _springForce * Time.deltaTime, ForceMode.Impulse);
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