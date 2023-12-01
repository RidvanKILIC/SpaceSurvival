using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float damage;
    public float speed;
    public float range;
    public bool canMove;
    public Rigidbody2D rb;
    public AudioClip shootSFX;
    public AudioClip blastSFX;
    public GameObject shootFx;
    public GameObject hitFX;
    public abstract void movement();
    public abstract void playShootFx(Vector3 pos);
    public abstract void playHitFx(Vector3 pos);
}
