using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
public class Weapon : ScriptableObject {
    public string id;
    public string displayName;
    public WeaponProperties properties;
}

[System.Serializable]
public struct WeaponProperties {
    public float damage;
    public int ammo;
    public float fireRate;
    public float reloadTime;
    public enum Type {
        Pistol,
        Rifle,
        Shotgun,
        Sniper,
        Melee
    }
    public Type type;
}
