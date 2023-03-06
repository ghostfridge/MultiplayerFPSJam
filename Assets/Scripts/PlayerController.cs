using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FishNet.Object;
using TMPro;

[RequireComponent(typeof(CapsuleCollider), typeof(Rigidbody))]
public class PlayerController : NetworkBehaviour {
    public InputMain controls;
    private CapsuleCollider col;
    private Rigidbody rb;

    [SerializeField] private CanvasReferences canvas;
    [SerializeField] private Transform head;
    [SerializeField] private WeaponController weaponController;
    [SerializeField] private Weapon primaryWeapon;
    [SerializeField] private Weapon secondaryWeapon;

    [Header("Camera Settings")]
    [SerializeField] private float lookSensitivity;
    public Vector2 lookRotation;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float groundDistance;
    [SerializeField] private float groundCheckOffset;

    public override void OnStartClient() {
        base.OnStartClient();

        if (IsOwner) {
            if (controls != null) {
                controls.Enable();
            }

            SetActiveWeapon(primaryWeapon);

            canvas.primaryWeaponUI.GetComponentInChildren<TMP_Text>().text = primaryWeapon.displayName;
            canvas.secondaryWeaponUI.GetComponentInChildren<TMP_Text>().text = secondaryWeapon.displayName;
        } else {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(canvas.gameObject);
        }
    }

    public override void OnStopClient() {
        base.OnStopClient();

        if (IsOwner) {
            if (controls != null) {
                controls.Disable();
            }
        }
    }

    private void Awake() {
        controls = new InputMain();
    }

    private void Start() {
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        if (IsOwner) {
            PerformLooking();

            if (controls.Player.EquipPrimaryWeapon.triggered) {
                SetActiveWeapon(primaryWeapon);
            } else if (controls.Player.EquipSecondWeapon.triggered) {
                SetActiveWeapon(secondaryWeapon);
            }

            if (controls.Player.Shoot.triggered) weaponController.Shoot();
        }
    }

    private void FixedUpdate() {
        if (IsOwner) {
            PerformMovement();
        }
    }

    private void SetActiveWeapon(Weapon weapon) {
        weaponController.weapon = weapon;
        
        if (weapon == primaryWeapon) {
            RectTransform primaryRect = (RectTransform) canvas.primaryWeaponUI;
            primaryRect.sizeDelta = new Vector2(175f, primaryRect.sizeDelta.y);

            RectTransform secondaryRect = (RectTransform) canvas.secondaryWeaponUI;
            secondaryRect.sizeDelta = new Vector2(135f, secondaryRect.sizeDelta.y);
        } else if (weapon == secondaryWeapon) {
            RectTransform primaryRect = (RectTransform) canvas.primaryWeaponUI;
            primaryRect.sizeDelta = new Vector2(135f, primaryRect.sizeDelta.y);

            RectTransform secondaryRect = (RectTransform) canvas.secondaryWeaponUI;
            secondaryRect.sizeDelta = new Vector2(175f, secondaryRect.sizeDelta.y);
        } else {
            RectTransform primaryRect = (RectTransform) canvas.primaryWeaponUI;
            primaryRect.sizeDelta = new Vector2(135f, primaryRect.sizeDelta.y);

            RectTransform secondaryRect = (RectTransform) canvas.secondaryWeaponUI;
            secondaryRect.sizeDelta = new Vector2(135f, secondaryRect.sizeDelta.y);
        }
    }

    private void PerformLooking() {
        Vector2 input = controls.Player.Look.ReadValue<Vector2>();
        lookRotation += input * lookSensitivity;

        lookRotation.x = lookRotation.x % 360f;
        lookRotation.y = Mathf.Clamp(lookRotation.y, -90f, 90f);

        if (IsServer) {
            rb.MoveRotation(Quaternion.Euler(0f, lookRotation.x, 0f));
            head.localRotation = Quaternion.Euler(-lookRotation.y, 0f, 0f);
        } else {
            PerformLookingServerRpc(lookRotation);
        }
    }

    [ServerRpc]
    private void PerformLookingServerRpc(Vector2 lookRotation) {
        rb.MoveRotation(Quaternion.Euler(0f, lookRotation.x, 0f));
        head.localRotation = Quaternion.Euler(-lookRotation.y, 0f, 0f);
    }

    private void PerformMovement() {
        Vector2 input = controls.Player.Move.ReadValue<Vector2>();
        Vector3 velocity = (transform.right * input.x + transform.forward * input.y) * moveSpeed;

        if (rb.SweepTest(velocity.normalized, out RaycastHit sweepHit, velocity.magnitude * Time.deltaTime)) {
            velocity = Vector3.ProjectOnPlane(velocity, sweepHit.normal);
        }

        if (IsServer) {
            if (!IsGrounded()) velocity.y = rb.velocity.y;
            rb.velocity = velocity;
        } else {
            PerformMovementServerRpc(velocity);
        }
    }

    [ServerRpc]
    private void PerformMovementServerRpc(Vector3 velocity) {
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
}
