using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //CLIPS DE SONIDO
    public AudioClip Boton1;
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
    public AudioClip Salto;
    public AudioClip Alien;
    public AudioClip Ambiente;
    public AudioClip AmbienteMetro;
    public AudioClip Susto;
    public AudioClip Gato;
    public AudioClip LlegaMetro;

    //REFERENCIAS
    public GameObject MusicObj;
    public UnityEngine.Audio.AudioMixerGroup efectos; 

    //SOURCES
    private AudioSource audioMusic;
    private AudioSource audioPalomaSource;
    private AudioSource _audioSource;

    //SINGLETON
    public static AudioManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        _audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        _audioSource.outputAudioMixerGroup = efectos;

        audioPalomaSource = gameObject.AddComponent<AudioSource>();
        audioPalomaSource.clip = Paloma;
        audioPalomaSource.loop = false;
        audioPalomaSource.outputAudioMixerGroup = efectos;
    }
    void Start()
    {
        audioMusic = MusicObj.GetComponent<AudioSource>();
        audioMusic.clip = Ambiente;
        audioMusic.loop = true;
        audioMusic.Play();
    }

    //MÉTODOS PÚBLICOS

    public void SonarClipUnaVez(AudioClip ac)
    {
        if (ac != null)
        {
            _audioSource.PlayOneShot(ac);
        }
        else
        {
            Debug.LogWarning("AudioClip nulo");
        }
    }

    public void ReproducirAudioPaloma()
    {
     if (audioPalomaSource.isPlaying)
    {
            audioPalomaSource.Stop();
    }

    audioPalomaSource.Play();
    }

    public void PlayClipByName(string clipName)
    {
        AudioClip clip = GetClipByName(clipName);
        if (clip != null)
        {
            SonarClipUnaVez(clip);
        }
        else
        {
            Debug.LogError($"No se encontró el AudioClip '{clipName}'");
        }
    }


    private AudioClip GetClipByName(string name)
    {
        AudioClip[] allClips = {
            Boton1, Llaves, Puerta, PuertaMetro, Papel,
            Voces, VocesDist, Paloma, Luces, SigParada,
            SigParadaDist, Correr, Pasos, Salto, Alien,
            Ambiente, AmbienteMetro, Susto, Gato, LlegaMetro
        };

        foreach (var clip in allClips)
        {
            if (clip != null && clip.name == name)
                return clip;
        }

        return null;
    }
}