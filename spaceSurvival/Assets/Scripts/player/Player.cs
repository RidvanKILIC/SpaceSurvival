using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IDamagable
{
    [SerializeField] float health;
    [SerializeField] healthBar _healthBar;
    [SerializeField] GameObject singleShooter;
    [SerializeField] GameObject doubleShooter;
    [SerializeField] GameObject tripleShooter;
    [SerializeField] GameObject rocketShooter;
    GameObject shooterWeapon;
    GameObject rocketWeapon;
    shooterr_Spaceship _shooter;
    public float Health { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        _shooter = GetComponent<shooterr_Spaceship>();
        Health = health;
        _healthBar.setMaxHealth(Health);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void setWeapon(GameObject _weapon)
    {
        if (getShooter(_weapon.GetComponent<weaponContainer>().getWeapon()._shootingType).Equals(Weapon.shootingType.Rocket))
        {
            rocketWeapon = _weapon;
        }
        else
        {
            shooterWeapon = _weapon;
        }
        _weapon.GetComponent<Animator>().enabled = false;
        _weapon.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        _weapon.transform.SetParent(this.gameObject.transform);
        _weapon.transform.position = this.gameObject.transform.position;
        _shooter.shootWeapon(_weapon);
    }
    public GameObject getShooter(Weapon.shootingType _type)
    {
        switch(_type)
        {
            case(Weapon.shootingType.Single):
                return singleShooter;
                break;
            case (Weapon.shootingType.Double):
                return doubleShooter;
                break;
            case (Weapon.shootingType.Tripble):
                return tripleShooter;
            case (Weapon.shootingType.Rocket):
                return rocketShooter;
                break;
            default:
                return singleShooter;
                break;
        } 
    }
    public GameObject getShooterWeapon()
    {
        return shooterWeapon;
    }
    public GameObject getRocketWeapon()
    {
        return rocketWeapon;
    }
    public void Damage(float _damageAmount)
    {
        Health -= _damageAmount;
        _healthBar.decraseHealth(_damageAmount);
        if (Health <= 0)
        {
            Debug.Log("Died");
        }
    }
    public void Heal(float _healthAmount)
    {
        throw new System.NotImplementedException();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            setWeapon(collision.gameObject);
        }
    }
}
