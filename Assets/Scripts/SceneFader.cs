using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UnityEngine.UI.Image))] // Necesitas UI Image
public class SceneFader : MonoBehaviour
{
    public static SceneFader instance; // Singleton

    [SerializeField] private float fadeDuration = 1f; // Duración del fade
    private UnityEngine.UI.Image fadeImage; // Imagen negra
    private bool isFading = false;

    void Awake()
    {
        // Aseguramos que haya solo una instancia
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        fadeImage = GetComponent<UnityEngine.UI.Image>();
        fadeImage.enabled = true;
    }

    void Start()
    {
        // Empezamos con pantalla negra y hacemos fade-in
        StartCoroutine(FadeIn());
    }

    // Método para hacer fade-out y luego cargar escena
    public void FadeAndLoadScene(string sceneName)
    {
        if (!isFading)
        {
            StartCoroutine(FadeOut(sceneName));
        }
    }

    // FADE OUT - De transparente a negro
    IEnumerator FadeOut(string sceneName)
    {
        isFading = true;
        float timeElapsed = 0f;
        Color startColor = fadeImage.color;
        Color targetColor = new Color(0, 0, 0, 1);

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.unscaledDeltaTime;
            fadeImage.color = Color.Lerp(startColor, targetColor, timeElapsed / fadeDuration);
            yield return null;
        }

        SceneManager.LoadScene(sceneName);

        yield return new WaitForSeconds(0.2f); // Pequeña pausa antes de fade in

        StartCoroutine(FadeIn());
        isFading = false;
    }

    // FADE IN - De negro a transparente
    IEnumerator FadeIn()
    {
        float timeElapsed = 0f;
        Color startColor = fadeImage.color;
        Color targetColor = new Color(0, 0, 0, 0);

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.unscaledDeltaTime;
            fadeImage.color = Color.Lerp(startColor, targetColor, timeElapsed / fadeDuration);
            yield return null;
        }

        fadeImage.enabled = false; // Desactiva la imagen cuando es transparente
    }
}