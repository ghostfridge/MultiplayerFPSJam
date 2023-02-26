using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour {
    [SerializeField] private float health = 100f;

    /// <returns>
    /// The amount of health the Actor has.
    /// </returns>
    public float GetHealth() {
        return health;
    }

    /// <returns>
    /// The new amount of health the Actor has.
    /// </returns>
    public virtual float TakeDamage(float damage) {
        health -= damage;
        WelfareCheck();

        return health;
    }

    /// <returns>
    /// The new amount of health the Actor has.
    /// </returns>
    public float SetHealth(float newHealth) {
        health = newHealth;
        WelfareCheck();

        return health;
    }

    /// <returns>
    /// Whether the Actor is alive or not.
    /// </returns>
    private bool WelfareCheck() {
        if (health <= 0f) {
            health = 0f;
            Die();
        }

        return health > 0f;
    }

    /// <returns>
    /// The Actor's GameObject that was destroyed.
    /// </returns>
    public GameObject Die() {
        Destroy(gameObject);
        return gameObject;
    }
}
