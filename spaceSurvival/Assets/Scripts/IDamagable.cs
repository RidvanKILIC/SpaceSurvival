using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    int Health { get; set; }
    void Damage(int _damageAmount);
    void Heal(int _healthAmount);

}