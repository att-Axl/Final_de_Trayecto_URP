using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ControlVolumen : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider generalSlider;

    private void Awake()
    {

        generalSlider.onValueChanged.AddListener(ControlAmbosVolúmenes);
    }

    void Start()
    {
        Cargar();
    }

    private void ControlAmbosVolúmenes(float valor)
    {
        float dB = valor <= 0.0001f ? -80f : Mathf.Log10(valor) * 20f;

        mixer.SetFloat("VolumenGeneral", dB);
        mixer.SetFloat("Musica", dB);
        mixer.SetFloat("Efectos", dB);

        PlayerPrefs.SetFloat("VolumenGeneral", valor);
        PlayerPrefs.SetFloat("Musica", valor);
        PlayerPrefs.SetFloat("Efectos", valor);
    }

    private void Cargar()
    {
        float savedVolume = PlayerPrefs.GetFloat("VolumenGeneral", 0.75f);
        generalSlider.value = savedVolume;

        ControlAmbosVolúmenes(savedVolume);
    }
}