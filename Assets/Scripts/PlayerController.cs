using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider), typeof(Rigidbody), typeof(WeaponController))]
public class PlayerController : NetworkBehaviour {
    public InputMain controls;
    private CapsuleCollider col;
    private Rigidbody rb;
    private WeaponController weaponController;

    [SerializeField] private Transform head;
    [SerializeField] private Weapon primaryWeapon;
    [SerializeField] private Weapon secondaryWeapon;

    [Header("Camera Settings")]
    [SerializeField] private float lookSensitivity;
    private Vector2 lookRotation;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float groundDistance;
    [SerializeField] private float groundCheckOffset;

    private void Awake() {
        controls = new InputMain();
    }

    public override void OnNetworkSpawn() {
        if (!IsServer && IsOwner) { //Only send an RPC to the server on the client that owns the NetworkObject that owns this NetworkBehaviour instance
            TestServerRpc(0, NetworkObjectId);
        }
    }

    [ClientRpc]
    void TestClientRpc(int value, ulong sourceNetworkObjectId) {
        Debug.Log($"Client Received the RPC #{value} on NetworkObject #{sourceNetworkObjectId}");
        if (IsOwner) { //Only send an RPC to the server on the client that owns the NetworkObject that owns this NetworkBehaviour instance
            TestServerRpc(value + 1, sourceNetworkObjectId);
        }
    }

    [ServerRpc]
    void TestServerRpc(int value, ulong sourceNetworkObjectId) {
        Debug.Log($"Server Received the RPC #{value} on NetworkObject #{sourceNetworkObjectId}");
        TestClientRpc(value, sourceNetworkObjectId);
    }

    private void Start() {
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        weaponController = GetComponent<WeaponController>();

        weaponController.weapon = primaryWeapon;
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        PerformLooking();

        if (controls.Player.EquipPrimaryWeapon.triggered) {
            weaponController.weapon = primaryWeapon;
        } else if (controls.Player.EquipSecondWeapon.triggered) {
            weaponController.weapon = secondaryWeapon;
        }
    }

    private void FixedUpdate() {
        PerformMovement();
    }

    private void PerformLooking() {
        Vector2 input = controls.Player.Look.ReadValue<Vector2>();
        lookRotation += input * lookSensitivity;

        lookRotation.x = lookRotation.x % 360f;
        lookRotation.y = Mathf.Clamp(lookRotation.y, -90f, 90f);

        rb.MoveRotation(Quaternion.Euler(0f, lookRotation.x, 0f));
        head.localRotation = Quaternion.Euler(-lookRotation.y, 0f, 0f);
    }

    private void PerformMovement() {
        Vector2 input = controls.Player.Move.ReadValue<Vector2>();
        Vector3 velocity = (transform.right * input.x + transform.forward * input.y) * moveSpeed;

        if (rb.SweepTest(velocity.normalized, out RaycastHit sweepHit, velocity.magnitude * Time.deltaTime)) {
            velocity = Vector3.ProjectOnPlane(velocity, sweepHit.normal);
        }

        if (!IsGrounded()) velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }

    private bool IsGrounded() {
        float radius = col.radius;
        Vector3 origin = transform.position + Vector3.up * (groundCheckOffset + radius);
        return Physics.SphereCast(origin, radius, Vector3.down, out RaycastHit groundHit, groundCheckOffset + groundDistance);
    }

    private void OnDrawGizmos() {
        if (Application.isPlaying) {
            Gizmos.color = Color.red;

            Vector3 origin = transform.position + Vector3.up * groundCheckOffset;
            Gizmos.DrawLine(origin, origin + Vector3.down * (groundDistance + groundCheckOffset));
        }
    }

    private void OnEnable() {
        if (controls != null) {
            controls.Enable();
        }
    }

    private void OnDisable() {
        if (controls != null) {
            controls.Disable();
        }
    }
}
