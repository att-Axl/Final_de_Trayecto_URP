using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class CmbScnUno : MonoBehaviour
{
    public string VagonUno;
    public float fadeDuration = 1f; 

    private bool isFading = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFading)
        {
            StartCoroutine(FadeAndLoadScene(VagonUno));
        }
    }

    IEnumerator FadeAndLoadScene(string sceneName)
    {
        isFading = true;

        // Creamos un panel negro 
        GameObject fadePanelGO = new GameObject("FadePanel");
        Canvas canvas = fadePanelGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 100; //Capa
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

        // Destruir el panel después de cargar la escena
        Destroy(fadePanelGO);
    }
}