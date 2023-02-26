using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CapsuleCollider), typeof(Rigidbody), typeof(WeaponController))]
public class PlayerController : NetworkBehaviour {
    public InputMain controls;
    private CapsuleCollider col;
    private Rigidbody rb;
    private WeaponController weaponController;
    
    private bool isCursorCaptured;

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

    public override void OnNetworkSpawn() {
        if (IsOwner && controls != null) {
            controls.Enable();
            controls.Player.ToggleCursorCapture.performed += ToggleCursorCapture;
            controls.Player.CaptureCursor.performed += CaptureCursor;
        }
    }

    public override void OnNetworkDespawn() {
        if (IsOwner && controls != null) {
            controls.Disable();
            controls.Player.ToggleCursorCapture.performed -= ToggleCursorCapture;
            controls.Player.CaptureCursor.performed -= CaptureCursor;
        }
    }

    private void Awake() {
        controls = new InputMain();
    }

    private void Start() {
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        weaponController = GetComponent<WeaponController>();

        weaponController.weapon = primaryWeapon;
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        if (isCursorCaptured) {
            PerformLooking();

            if (controls.Player.EquipPrimaryWeapon.triggered) {
                weaponController.weapon = primaryWeapon;
            } else if (controls.Player.EquipSecondWeapon.triggered) {
                weaponController.weapon = secondaryWeapon;
            }
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

    private void ToggleCursorCapture(InputAction.CallbackContext ctx) {
        isCursorCaptured = !isCursorCaptured;
        Cursor.lockState = isCursorCaptured ? CursorLockMode.Locked : CursorLockMode.None;
    }

    private void CaptureCursor(InputAction.CallbackContext ctx) {
        // Only capture if there are no current interactions
        if (GUIUtility.hotControl == 0) {
            isCursorCaptured = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void OnDrawGizmos() {
        if (Application.isPlaying) {
            Gizmos.color = Color.red;

            Vector3 origin = transform.position + Vector3.up * groundCheckOffset;
            Gizmos.DrawLine(origin, origin + Vector3.down * (groundDistance + groundCheckOffset));
        }
    }
}
