using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ControlVolumen : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider generalSlider;
   


    // Start is called before the first frame update

    private void Awake()
    {
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

    private void ControlGeneralVolumen(float valor){
        mixer.SetFloat("VolumenGeneral", Mathf.Log10(valor) * 20);
        PlayerPrefs.SetFloat("VolumenGeneral", generalSlider.value);
    }



        

        private void Cargar(){
            generalSlider.value = PlayerPrefs.GetFloat("VolumenMusica", 0.75f);
        
            ControlGeneralVolumen(generalSlider.value);
           
        }



}
