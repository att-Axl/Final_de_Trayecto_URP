using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    public Animator animator;
    private string sceneToLoad;

    public void FadeToScene(string sceneName)
    {
        sceneToLoad = sceneName;
        animator.SetTrigger("isFadingOut");
    }

    
    public void OnFadeOutComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

        public void OnFadeInComplete()
    {
        animator.SetTrigger("isFadingIn");
    }
}