using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEnemy : MonoBehaviour, IDamageable {
    private float health = 100f;

    public float GetHealth() {
        return health;
    }

    public float TakeDamage(float damage) {
        health -= damage;
        WelfareCheck();

        return health;
    }

    /// <returns>
    /// The new amount of health of the NPC.
    /// </returns>
    public float SetHealth(float newHealth) {
        health = newHealth;
        WelfareCheck();

        return health;
    }

    /// <returns>
    /// Whether the NPC is alive or not.
    /// </returns>
    private bool WelfareCheck() {
        if (health <= 0f) {
            health = 0f;
            Die();
        }

        return health > 0f;
    }

    /// <returns>
    /// The GameObject that was destroyed.
    /// </returns>
    public GameObject Die() {
        Destroy(gameObject);
        return gameObject;
    }
}
