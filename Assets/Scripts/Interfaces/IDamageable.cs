using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable {
    /// <returns>
    /// The amount of health.
    /// </returns>
    public float GetHealth();

    /// <returns>
    /// The new amount of health.
    /// </returns>
    public float TakeDamage(float damage);
}
