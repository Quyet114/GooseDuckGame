using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("-------------Audio Source-----------")]
    [SerializeField] AudioSource Music_Source;
    [SerializeField] AudioSource FSX_Source;
    [Header("-------------Audio Clip-------------")]
    public AudioClip background;
    public AudioClip getCoint;
    public AudioClip getChest;
    public AudioClip death;
    public AudioClip hitwall;
    public AudioClip Monster;
    // Start is called before the first frame update
    void Start()
    {
        Music_Source.clip = background;
        Music_Source.Play();
    }
 
    // Update is called once per frame
    public void PlayFSX(AudioClip clip)
    {
        FSX_Source.PlayOneShot(clip);
    }
}
