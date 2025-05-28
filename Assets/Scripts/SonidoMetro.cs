using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoMetro : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource _audioSource;

    public static AudioManager Instance;AudioSource audioAmbienteMetro;
    AudioSource audioVoces;
    AudioSource audioLuces;
    AudioSource audioSigParada;
    
    void Start()
    {
       audioAmbienteMetro = gameObject.AddComponent<AudioSource>();
        audioAmbienteMetro.clip = AudioManager.Instance.AmbienteMetro;
        audioAmbienteMetro.loop = true;
        audioAmbienteMetro.Play();

        audioVoces = gameObject.AddComponent<AudioSource>();
        audioVoces.clip = AudioManager.Instance.Voces;
        audioVoces.loop = true;
        audioVoces.Play();

        audioLuces = gameObject.AddComponent<AudioSource>();
        audioLuces.clip = AudioManager.Instance.Luces;
        audioLuces.loop = true;
        audioLuces.Play();

        audioSigParada = gameObject.AddComponent<AudioSource>();
        audioSigParada.clip = AudioManager.Instance.SigParada;
        audioSigParada.loop = true;
        audioSigParada.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
