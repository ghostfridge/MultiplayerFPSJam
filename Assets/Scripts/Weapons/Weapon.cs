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
    public WeaponType type;
    public float damage;
    public int ammo;
    public float fireRate;
    public float reloadTime;
}
