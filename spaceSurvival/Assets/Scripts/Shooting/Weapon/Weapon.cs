using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    public shootingType _shootingType;
    public GameObject _bullet;
    public float delay;
    public enum shootingType
    {
        Single,
        Double,
        Tripble,
        Rocket
    }
    public abstract void shoot();
}
