using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BaseHealth : MonoBehaviour, IDamageable
{
    [HideInInspector] public int currentHealth;

    [SerializeField] protected int maxHealth;

    public virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void Damage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void RegenHealth(int health)
    {    
        if(currentHealth + health > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += health;
        }
    }

    public virtual void Die() 
    {
        PhotonNetwork.Destroy(gameObject);
    }

}
