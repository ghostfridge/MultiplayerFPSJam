using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider), typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
    public InputMain controls;

    [SerializeField] private Transform head;

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

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        PerformLooking();
    }

    private void FixedUpdate() {
        PerformMovement();
    }

    private void PerformLooking() {
        Vector2 input = controls.Player.Look.ReadValue<Vector2>();
        lookRotation += input * lookSensitivity;

        lookRotation.x = lookRotation.x % 360f;
        lookRotation.y = Mathf.Clamp(lookRotation.y, -90f, 90f);

        GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(0f, lookRotation.x, 0f));
        head.localRotation = Quaternion.Euler(-lookRotation.y, 0f, 0f);
    }

    private void PerformMovement() {
        Vector2 input = controls.Player.Move.ReadValue<Vector2>();

        Vector3 velocity = (transform.right * input.x + transform.forward * input.y) * moveSpeed;
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb.SweepTest(velocity.normalized, out RaycastHit sweepHit, velocity.magnitude * Time.deltaTime)) {
            velocity = Vector3.ProjectOnPlane(velocity, sweepHit.normal);
        }

        if (!IsGrounded()) velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }

    private bool IsGrounded() {
        CapsuleCollider col = GetComponent<CapsuleCollider>();

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
