using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFaderClick : MonoBehaviour
{
    public string sceneToLoad = "EstacionBase"; 
    public float fadeDuration = 1f; 

    private Image fadePanel;
    private bool isFading = false;

    void Start()
    {
        // Crear un panel negro 
        GameObject panel = new GameObject("FadePanel");
        fadePanel = panel.AddComponent<Image>();
        fadePanel.rectTransform.SetParent(transform, false);
        fadePanel.rectTransform.anchorMin = Vector2.zero;
        fadePanel.rectTransform.anchorMax = Vector2.one;
        fadePanel.rectTransform.offsetMin = fadePanel.rectTransform.offsetMax = Vector2.zero;
        fadePanel.color = new Color(0, 0, 0, 0); 
        fadePanel.gameObject.SetActive(true);

        var canvas = panel.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        panel.AddComponent<GraphicRaycaster>(); 
    }

    void Update()
    {
        if (!isFading && (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)))
        {
            StartCoroutine(FadeAndLoadScene());
        }
    }

    System.Collections.IEnumerator FadeAndLoadScene()
    {
        isFading = true;

        float timeElapsed = 0f;

        // Fundido negro
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.unscaledDeltaTime;
            float alpha = Mathf.Clamp01(timeElapsed / fadeDuration);
            fadePanel.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // Cargar escena
        SceneManager.LoadScene(sceneToLoad);
    }
}