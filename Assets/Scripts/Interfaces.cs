using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interfaces : MonoBehaviour
{
    GameObject panelAjustes;
    // Start is called before the first frame update
    void Start()
    {
        panelAjustes = GameObject.Find("PanelAjustes");
        panelAjustes.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartGame(){
        SceneManager.LoadScene("EstacionBase");
    }
    public void ExitGame(){
        Debug.Log("Exit");
        Application.Quit();
    }

    public void MostrarSettings(){
        panelAjustes.SetActive(true);
    }

    public void OcultarSettings(){
        panelAjustes.SetActive(false);
    }

    public void SuenaBoton(){
        AudioManager.Instance.SonarClipUnaVez(AudioManager.Instance.Boton1);
    }

}
