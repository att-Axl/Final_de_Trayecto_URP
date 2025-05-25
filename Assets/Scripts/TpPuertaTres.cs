using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TpPuertaTres : MonoBehaviour
{
    public string nomEscena = "VagonCuatro";
    public float distanciaMaxima = 2f;
    public TextMeshProUGUI textoE;

    [SerializeField] private float fadeDuration = 1f; 

    private GameObject puertaActual = null;
    private bool isFading = false;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        Debug.DrawRay(ray.origin, ray.direction * distanciaMaxima, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, distanciaMaxima))
        {
            if (hit.collider.CompareTag("Puerta"))
            {
                puertaActual = hit.collider.gameObject;

                if (textoE != null)
                    textoE.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E) && !isFading)
                {
                    StartCoroutine(FadeAndLoadScene(nomEscena));
                }
            }
            else
            {
                DesactivarTexto();
            }
        }
        else
        {
            DesactivarTexto();
        }
    }

    private void DesactivarTexto()
    {
        if (textoE != null)
            textoE.gameObject.SetActive(false);
        puertaActual = null;
    }

    IEnumerator FadeAndLoadScene(string sceneName)
    {
        isFading = true;

        // Crear panel 
        GameObject fadePanelGO = new GameObject("FadePanel");
        Canvas canvas = fadePanelGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 100;
        fadePanelGO.AddComponent<GraphicRaycaster>();

        Image fadeImage = fadePanelGO.AddComponent<Image>();
        fadeImage.color = new Color(0, 0, 0, 0);

        RectTransform rect = fadeImage.rectTransform;
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = rect.offsetMax = Vector2.zero;

        float timeElapsed = 0f;

        // Fundido negro
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(timeElapsed / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // Cargar escena
        SceneManager.LoadScene(sceneName);
        Destroy(fadePanelGO);
    }
}