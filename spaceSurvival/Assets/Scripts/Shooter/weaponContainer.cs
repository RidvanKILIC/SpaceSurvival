using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponContainer : MonoBehaviour
{
    [SerializeField] Weapon _weapon;
    bool triggerShoot = false;
    bool coolDownd=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerShoot)
            shootWeapon();
    }
    public void triggerShooting()
    {
        triggerShoot = true;
    }
    public void shootWeapon()
    {
        if (getWeapon() != null && !coolDownd)
        {
            StartCoroutine(shootRountine());
        }
    }
    public Weapon getWeapon()
    {
        if (_weapon != null)
            return _weapon;
        else
            return null;
    }
    IEnumerator shootRountine()
    {
        coolDownd = true;
        yield return new WaitForSeconds(getWeapon().delay);
        _weapon.shoot();
        coolDownd = false;
    }
}
