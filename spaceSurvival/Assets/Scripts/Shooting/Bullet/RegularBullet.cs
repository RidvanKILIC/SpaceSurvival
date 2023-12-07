using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularBullet : Bullet
{
    private void Update()
    {
        if (this.canMove)
            moveForward();
    }
    public override void movement()
    {
        playShootFx(this.transform.position);
        this.canMove = true;
        //Debug.Log("Moving");
        //LeanTween.move(this.gameObject,transform.forward,1f).setEase(LeanTweenType.linear).setOnComplete(()=>this.canMove=true);
    }
    public override void playHitFx(Vector3 pos)
    {
        GameObject _hitFx = Instantiate(this.hitFX, pos, Quaternion.identity);

        _hitFx.transform.position = pos;
        _hitFx.transform.localScale = Vector3.one;
        Destroy(_hitFx, 0.3f);
    }
    public override void playShootFx(Vector3 pos)
    {
        GameObject _shootFx = Instantiate(this.shootFx, pos, Quaternion.identity);
        _shootFx.transform.position = pos;
        _shootFx.transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
        _shootFx.transform.localScale = Vector3.one;
        soundManager.SInstance.playSfx(shootSFX);
        Destroy(_shootFx, 0.2f);
    }
    void moveForward()
    {
            //Debug.Log("Distance: "+ Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, this.transform.position));
            //Debug.Log("Range: " + this.range);
        if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, this.transform.position) < this.range)
        {
            transform.position += this.transform.up * Time.deltaTime * speed;
        }
        else
        {
            soundManager.SInstance.playSfx(blastSFX);
            playHitFx(this.transform.position);
            Destroy(this.gameObject);
        }
           
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IDamagable col = collision.gameObject.GetComponent<IDamagable>();
            if (col != null)
            {
                col.Damage(damage);
                playHitFx(this.transform.position);
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("Hit null");
            }
        }
    }

}
