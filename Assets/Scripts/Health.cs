using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;

    private void OnTriggerEnter2D(Collider2D other)
    {//this will check collider we are passing in and see if we can
        //grab component of type
        //if yes, our Damage dealer variable will hold a reference to that component
        //if not it will be null
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        // if its not null we want to take damage
        //and tell damage dealer it hit something
        if (damageDealer != null)
        {//what we will take in as value
            TakeDamage(damageDealer.GetDamage());
            damageDealer.Hit();
        }
    }
    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        { 
            Destroy(gameObject);
        }
    }
}
