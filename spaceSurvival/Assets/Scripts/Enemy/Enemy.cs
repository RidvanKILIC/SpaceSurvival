using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    public string Tag;
    public float dropCount;
    public float offset = -90f;
    protected Transform targetObj;
    protected Vector3 targetPos;
    protected Vector3 thisPos;
    protected float angle;
    public float health = 50;
    public float damage = 10;
    public bool DamageCoolDown = false;
    public float speed = 0.5f;
    public healthBar _healthBar;
    public Vector2 target, direction;
    protected Rigidbody2D rb;
    protected CapsuleCollider2D col;
    public GameObject portal;
    public GameObject ship;
    public GameObject impactFx;
    public AudioClip movementSFX;
    public AudioClip impactSFX;
    public GameObject dropObject;
    protected bool spawned = false;
    public abstract void impact(Vector3 pos);
}
