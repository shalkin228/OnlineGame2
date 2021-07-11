using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : BaseHealth
{
    public override void Damage(int damage)
    {
        base.Damage(damage);
        print(currentHealth);
    }
}
