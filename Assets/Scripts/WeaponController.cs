using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {
    [HideInInspector] public Transform cam;
    [HideInInspector] public Weapon weapon;

    private WeaponHit weaponHit;
    private Queue<WeaponHit> weaponHitHistory = new Queue<WeaponHit>();

    private void Update() {
        Debug.DrawRay(cam.position, cam.forward * 100f, Color.red);
    }

    public WeaponHit? Shoot() {
        if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, 100f)) {
            weaponHit = new WeaponHit(hit.collider.gameObject, hit.point, hit.normal, hit.distance);

            weaponHitHistory.Enqueue(weaponHit);
            if (weaponHitHistory.Count > 10) weaponHitHistory.Dequeue();

            if (hit.collider.gameObject.TryGetComponent<Actor>(out Actor actor)) {
                float oldHealth = actor.GetHealth();
                float newHealth = actor.TakeDamage(weapon.properties.damage);
                Debug.Log($"{oldHealth} => {newHealth}");
            }

            return weaponHit;
        }

        return null;
    }

    private void OnDrawGizmos() {
        int hitI = 0;
        foreach (WeaponHit hit in weaponHitHistory) {
            Color color;
            float radius;
            if (hitI < weaponHitHistory.Count - 1) {
                color = Color.yellow;
                radius = 0.05f;
            } else {
                color = Color.red;
                radius = 0.1f;
            }

            Gizmos.color = color;
            Gizmos.DrawSphere(hit.point, radius);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(hit.point, hit.normal * 0.5f);

            hitI++;
        }
    }
}

public struct WeaponHit {
    public GameObject gameObject;
    public Vector3 point;
    public Vector3 normal;
    public float distance;

    public WeaponHit(GameObject gameObject, Vector3 point, Vector3 normal, float distance) {
        this.gameObject = gameObject;
        this.point = point;
        this.normal = normal;
        this.distance = distance;
    }
}