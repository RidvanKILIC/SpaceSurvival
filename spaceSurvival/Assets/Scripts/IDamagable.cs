using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    float Health { get; set; }
    void Damage(float _damageAmount);
    void Heal(float _healthAmount);

}