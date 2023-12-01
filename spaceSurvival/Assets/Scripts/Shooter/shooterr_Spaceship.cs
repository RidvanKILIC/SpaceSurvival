using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooterr_Spaceship : MonoBehaviour
{
    Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void shootWeapon(GameObject weapon)
    {
        var shooter = weapon.GetComponent<weaponContainer>();
        if (shooter != null)
            shooter.triggerShooting();
    }

}
