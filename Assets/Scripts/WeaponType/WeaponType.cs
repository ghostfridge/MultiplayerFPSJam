using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Type", menuName = "Scriptable Objects/Weapon Type")]
public class WeaponType : ScriptableObject {
    public enum FireMode {
        SemiAuto,
        FullAuto,
        Burst,
    }
    public FireMode fireMode;
}
