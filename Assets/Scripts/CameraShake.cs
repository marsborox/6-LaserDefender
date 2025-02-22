using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeDuration = 0.5f;
    [SerializeField] float shakeMagnitude = 0.25f;

    Vector3 initialPosition;
    void Start()
    {
        initialPosition= transform.position;
    }

    public void Play()
    {
        StartCoroutine(Shake());
    }
    IEnumerator Shake()
    {
        float elapsedTime = 0;
        while (elapsedTime < shakeDuration)
        {
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            elapsedTime += Time.deltaTime;//so it wont be infinite loop
            yield return new WaitForEndOfFrame();
        }
        transform.position = initialPosition;
    }    
}
