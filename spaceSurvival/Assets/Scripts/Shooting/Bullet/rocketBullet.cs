using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketBullet : Bullet
{
    public override void movement()
    {
        float RandomRot = Random.Range(-180, 180);
        LeanTween.rotateY(this.gameObject, RandomRot, 1.0f).setOnComplete(()=>
        LeanTween.moveY(this.gameObject, 100f, 2f).setEase(LeanTweenType.easeInCirc).setOnComplete(() => Destroy(this.gameObject)));
        //Debug.Log("Moving");
        
    }

    public override void playHitFx(Vector3 pos)
    {
        throw new System.NotImplementedException();
    }

    public override void playShootFx(Vector3 pos)
    {
        throw new System.NotImplementedException();
    }
}
