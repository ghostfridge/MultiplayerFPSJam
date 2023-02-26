using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEnemy : Actor {
    [SerializeField] private AIBehaviour behaviour;

    public override float TakeDamage(float damage) {
        Debug.Log("I'm an NPC and I'm taking damage! :D");

        return base.TakeDamage(damage);
    }
}
