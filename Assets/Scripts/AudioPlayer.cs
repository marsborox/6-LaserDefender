using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    //this will kinda constrain value within range 0 and 1 - 
    //its probablyn othing and full
    [SerializeField][Range(0f, 1f)] float shootingVolume = 1f;

    [Header("GetHit")]
    [SerializeField] AudioClip getHitClip;
    [SerializeField] [Range(0f,1f)] float getHitVolume = 1f;

    static AudioPlayer instance;

    /*public AudioPlayer GetInstance()
    { 
        return instance;
    }*/


    private void Awake()
    {
        ManageSingleton();
    }
    void ManageSingleton()
    {//it will track of how many instances of audio player exist

        //int instanceCount = FindObjectsOfType(GetType()).Length;
        //if (instanceCount > 1)
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayShootingClip()
    {
        PlayClip(shootingClip,shootingVolume);
    }

    public void PlayGetHitClip()
    {
        PlayClip(getHitClip, getHitVolume);
    }

    //into method we pass which clip and which volume
    void PlayClip(AudioClip clip, float volume)
    { 
        if (clip != null) 
        {//camera, main camera, at position where it is
            Vector3 cameraPos = Camera.main.transform.position;
            //play clip (which clip, where, how loud)
            AudioSource.PlayClipAtPoint(clip,cameraPos,volume);
        }
    }
}
