using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;

    [SerializeField] float baseFiringRate = 0.1f;

    [Header("AI")]
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;
    //it will be true or false and based on that player or 
    //AI is liek applied on prefab
    [SerializeField] bool useAI;

    [HideInInspector]public bool isFiring;

    Coroutine firingCoroutine;

    AudioPlayer audioPlayer;
    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        if (useAI)
        { 
            isFiring = true;
            
        }
    }
    void Update()
    {
        Fire();
    }
    void Fire()
    {//from our player script check if we are fireing
     //we want to start/stop coroutine
     //we dont want constalntly to start shooting
        if (isFiring && firingCoroutine == null)
        {//defined Coroutine - of type coroutine

            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if(!isFiring && firingCoroutine != null)
        { // if button is unpressed coroutine stops
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }
    IEnumerator FireContinuously()
    { 
        while (true) 
        {//temp variable, seems transform.position is where we are know
            GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null)
            { //transform.up will make stuff go up / towards green arrow in unity
                //by default its speed 1 so why we multiply by projectile speed
                rb.velocity = transform.up * projectileSpeed;
            }
            //destroy what this instance when after that lifetime expires
            Destroy(instance,projectileLifetime);

            //random range and clamp so it wont go into negative value
            float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);

            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }
}
