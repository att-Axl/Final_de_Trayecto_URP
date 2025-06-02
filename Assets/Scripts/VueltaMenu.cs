using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VueltaMenu : MonoBehaviour
{
[SerializeField] private float delay = 19f; 

    void Start()
    {
        StartCoroutine(CargarInicioOficial());
    }

    IEnumerator CargarInicioOficial()
    {

        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("InicioOficialFinal");
    }
}