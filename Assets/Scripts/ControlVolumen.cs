using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ControlVolumen : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider musicaSlider;
    public Slider efectosSlider;
    public Slider vocesSlider;
    public Slider generalSlider;


    // Start is called before the first frame update

    private void Awake()
    {
      musicaSlider.onValueChanged.AddListener(ControlMusicaVolumen);
      efectosSlider.onValueChanged.AddListener(ControlEfectosVolumen);
      vocesSlider.onValueChanged.AddListener(ControlVocesVolumen);
      generalSlider.onValueChanged.AddListener(ControlGeneralVolumen);  
    }

    void Start()
    {
        Cargar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ControlMusicaVolumen(float valor){
        mixer.SetFloat("VolumenMusica", Mathf.Log10(valor) * 20);
        PlayerPrefs.SetFloat("VolumenMusica", musicaSlider.value);
    }


    private void ControlEfectosVolumen(float valor){
        mixer.SetFloat("VolumenEfectos", Mathf.Log10(valor) * 20);
        PlayerPrefs.SetFloat("VolumenEfectos", efectosSlider.value);
    }

        private void ControlVocesVolumen(float valor){
        mixer.SetFloat("VolumenVoces", Mathf.Log10(valor) * 20);
        PlayerPrefs.SetFloat("VolumenVoces", vocesSlider.value);
        }

        private void ControlGeneralVolumen(float valor){
        mixer.SetFloat("VolumenGeneral", Mathf.Log10(valor) * 20);
        PlayerPrefs.SetFloat("VolumenGeneral", generalSlider.value);
        }

        private void Cargar(){
            musicaSlider.value = PlayerPrefs.GetFloat("VolumenMusica", 0.75f);
            efectosSlider.value = PlayerPrefs.GetFloat("VolumenEfectos", 0.75f);
            vocesSlider.value = PlayerPrefs.GetFloat("VolumenVoces", 0.75f);
            generalSlider.value = PlayerPrefs.GetFloat("VolumenGeneral", 0.75f);
            ControlMusicaVolumen(musicaSlider.value);
            ControlEfectosVolumen(musicaSlider.value);
            ControlVocesVolumen(musicaSlider.value);
            ControlGeneralVolumen(musicaSlider.value);
        }



}
