using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ControlVolumen : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider generalSlider;
    public Slider musicaSlider;

    private void Awake()
    {
        generalSlider.onValueChanged.AddListener(ControlGeneralVolumen);
    }

    void Start()
    {
        Cargar();
    }

    private void ControlGeneralVolumen(float valor)
    {
        mixer.SetFloat("VolumenGeneral", Mathf.Log10(valor) * 20);
        PlayerPrefs.SetFloat("VolumenGeneral", valor);
    }

    private void Cargar()
    {
        float volGeneral = PlayerPrefs.GetFloat("VolumenGeneral", 0.75f);
        float volMusica = PlayerPrefs.GetFloat("VolumenMusica", 0.75f);

        generalSlider.value = volGeneral;

        ControlGeneralVolumen(volGeneral);

        mixer.SetFloat("VolumenMusica", Mathf.Log10(volMusica) * 20);
        PlayerPrefs.SetFloat("VolumenMusica", volMusica);
    }
}