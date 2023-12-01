using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Regular Weapon", menuName = ("Weaponary/Weapon/Regular Weapon"))]
public class regularWeapon : Weapon
{
    public override void shoot()
    {
        //Debug.Log("Shooting");
        GameObject shootPositons = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Player>().getShooter(this._shootingType);
        if (shootPositons != null)
        {
            foreach (Transform child in shootPositons.transform)
            {
                GameObject bullet = Instantiate(_bullet.gameObject, child.position, Quaternion.identity);
                bullet.transform.localScale = new Vector3(1f,1f,1f);
                bullet.transform.rotation = (GameObject.FindGameObjectWithTag("Player").transform.rotation/*new Quaternion(GameObject.FindGameObjectWithTag("Player").transform.rotation.x, bullet.transform.rotation.y, GameObject.FindGameObjectWithTag("Player").transform.rotation.z, bullet.transform.rotation.w)*/);
                var _BulletScript = bullet.GetComponent<Bullet>();
                //Vector3 forward = child.transform.TransformDirection(child.transform.up) * 10;
                //Debug.DrawRay(child.transform.position, forward, Color.green);
                if (_BulletScript != null)
                    _BulletScript.movement();
                else
                    Debug.Log("Bullet Script Cannot Be Found!");
            }
        }
        else
        {
            Debug.Log("Shoot Positions Is Null");
        }


    }

}
