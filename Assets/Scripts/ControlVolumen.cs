using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ControlVolumen : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider musicaSlider;
   


    // Start is called before the first frame update

    private void Awake()
    {
      musicaSlider.onValueChanged.AddListener(ControlMusicaVolumen);
      
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



        

        private void Cargar(){
            musicaSlider.value = PlayerPrefs.GetFloat("VolumenMusica", 0.75f);
        
            ControlMusicaVolumen(musicaSlider.value);
           
        }



}
