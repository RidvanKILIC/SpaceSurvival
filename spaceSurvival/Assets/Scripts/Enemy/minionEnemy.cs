using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minionEnemy : Enemy,IDamagable
{
    public float Health { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        targetObj = GameObject.FindGameObjectWithTag("Player").transform;
        direction = transform.position - GameObject.FindGameObjectWithTag("Player").transform.position;
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
        Health = health;
        _healthBar.setMaxHealth(Health);
        Destroy(this.gameObject, 20f);
        InvokeRepeating("refreshTarget", 3, 3);
        thisPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //float step = speed * Time.deltaTime;

        direction.Normalize();
        transform.Translate(-direction * speed * Time.fixedDeltaTime, Space.World);
        targetPos = targetObj.position;
        targetPos.x = targetPos.x - thisPos.x;
        targetPos.y = targetPos.y - thisPos.y;
        angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));


    }
    public override void impact(Vector3 pos)
    {
        GameObject fx = Instantiate(impactFx, pos, Quaternion.identity);
        fx.transform.localScale = Vector3.one;
        soundManager.SInstance.playSfx(impactSFX);
        Destroy(fx, 0.5f);
    }
    public void Damage(float DamageTaken)
    {
        if (!DamageCoolDown)
        {
            Debug.Log("Minion damaged");
            Health -= DamageTaken;
            _healthBar.decraseHealth(DamageTaken);
            StartCoroutine(damageCoolDown());
        }
        if (Health <= 0)
        {
            impact(transform.position);
            Destroy(this.gameObject);
        }
    }
    public void refreshTarget()
    {
        targetPos = targetObj.position;
        targetPos.x = targetPos.x - thisPos.x;
        targetPos.y = targetPos.y - thisPos.y;
    }
    IEnumerator damageCoolDown()
    {
        DamageCoolDown = true;
        yield return new WaitForSeconds(0.1f);
        DamageCoolDown = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IDamagable col = collision.gameObject.GetComponent<IDamagable>();
            if (col != null)
            {
                col.Damage(damage);
                impact(this.gameObject.transform.position);
                Destroy(this.gameObject);
            }
        }
   
    }
    public void Heal(float _healthAmount)
    {
        //throw new System.NotImplementedException();
    }
}
