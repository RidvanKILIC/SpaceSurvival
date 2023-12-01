using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    public float Damage { get; set; }
    public void movement(GameObject bullet,Transform direction);
}
