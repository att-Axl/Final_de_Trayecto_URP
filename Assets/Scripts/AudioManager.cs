using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip BotonUno;
    public AudioClip Llaves;
    public AudioClip Puerta;
    public AudioClip PuertaMetro;
    public AudioClip Papel;
    public AudioClip Voces;
    public AudioClip VocesDist;
    public AudioClip Paloma;
    public AudioClip Luces;
    public AudioClip SigParada;
    public AudioClip SigParadaDist;
    public AudioClip Correr;
    public AudioClip Pasos;
    public AudioClip Alien;
    public AudioClip Ambiente;

    AudioSource _audioSource;

    public static AudioManager Instance;
    void Awke(){

        if(Instance != null && Instance != this){
            Destroy(this);
        }else{
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        
    }




    // Start is called before the first frame update
    void Start()
    {
        _audioSource = this.GetComponent<AudioSource>();
        _audioSource.clip = Ambiente;
        _audioSource.loop = true;
        _audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
