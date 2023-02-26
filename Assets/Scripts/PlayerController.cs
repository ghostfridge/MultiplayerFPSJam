using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider), typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
    public InputMain controls;
    private CapsuleCollider col;
    private Rigidbody rb;

    [SerializeField] private Transform head;
    [SerializeField] private WeaponController weaponController;
    [SerializeField] private Weapon primaryWeapon;
    [SerializeField] private Weapon secondaryWeapon;

    [Header("Camera Settings")]
    [SerializeField] private float lookSensitivity;
    private Vector2 lookRotation;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float groundDistance;
    [SerializeField] private float groundCheckOffset;

    [Header("Crouch Settings")]
    [SerializeField] private float crouchScale;
    private float defaultScale;
    private bool crouching;

    private void Awake() {
        controls = new InputMain();
    }

    private void Start() {
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();

        weaponController.weapon = primaryWeapon;
        
        Cursor.lockState = CursorLockMode.Locked;
        defaultScale = transform.localScale.y;
    }

    private void Update() {
        PerformLooking();
        PerformCrouch();

        if (controls.Player.EquipPrimaryWeapon.triggered) {
            weaponController.weapon = primaryWeapon;
        } else if (controls.Player.EquipSecondWeapon.triggered) {
            weaponController.weapon = secondaryWeapon;
        }

        if (controls.Player.Shoot.triggered) weaponController.Shoot();
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

    private void PerformCrouch() {
        bool newState = controls.Player.Crouch.ReadValue<float>() != 0f;
        if (!newState) {
            bool obstructed = Physics.SphereCast(transform.position, col.radius, Vector3.up, out RaycastHit hit, col.height - col.radius - 0.01f);
            if (obstructed) return;
        }

        if (crouching != newState) {
            crouching = newState;
            transform.localScale = new Vector3(transform.localScale.x, crouching ? crouchScale : defaultScale, transform.localScale.z);
        }
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

            if (crouching) {
                float crouchLength = (defaultScale - (defaultScale * crouchScale)) * 2.5f;
                Gizmos.DrawLine(transform.position + Vector3.up * crouchScale * 2f, transform.position + Vector3.up * (defaultScale + crouchLength));
            }
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
