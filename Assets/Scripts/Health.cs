using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{//is it player or AI
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 50;
    [SerializeField] int unitScore = 50;
    [SerializeField] ParticleSystem hitEffect;

    //since script is attached to enemies as well 
    //we must set if its suppose to shake or no
    //will setup in prefab itself
    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;

    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    private void Awake()
    {//camera has find object of type built in it
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

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
            PlayHitEffect();
            ShakeCamera();
            damageDealer.Hit();
        }
    }
    void TakeDamage(int damage)
    {
        health -= damage;
        audioPlayer.PlayGetHitClip();
        if (health <= 0)
        {
            Die();
            
        }
    }
    public void Die()
    {
        if (!isPlayer)
        {
            scoreKeeper.ModifyScore(unitScore);

        }
        else 
        {
            levelManager.LoadGameOver();
        }
        Destroy(gameObject);
    }

    void PlayHitEffect()
    { //if our hit effect is not null-if we have attached something for hitEffect
        if(hitEffect != null) 
        {//play - we instantiate hit effect, on place where target was being hit,on some/default rotation
            ParticleSystem instance = Instantiate(hitEffect,transform.position, Quaternion.identity);
            //we will destroy with overloads - instance of gameobject we"just instantiated?"
            //instance is pretty much particle obejct??? ,instance duration it was created with
            //but include their lifetime since it was included in those previous definitions in Unity GUI
            Destroy(instance.gameObject,instance.main.duration+instance.main.startLifetime.constantMax);
        }
    }
    void ShakeCamera()
    {
        if(cameraShake != null&&applyCameraShake) 
        {
            cameraShake.Play();
        }
    }
    public int GetHealth()
    {
        return health;
    }

}
