using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CargarCreditos : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "Creditos"; 
    [SerializeField] private float fadeDuration = 2f; 
    
    private bool isFading = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFading)
        {
            StartCoroutine(FadeAndLoadScene());
        }
    }

    IEnumerator FadeAndLoadScene()
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


        fadeImage.color = Color.black;

        // Cargar escena
        SceneManager.LoadScene(sceneToLoad);
    }
}