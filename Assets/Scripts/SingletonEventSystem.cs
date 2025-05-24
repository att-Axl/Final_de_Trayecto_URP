using UnityEngine;
using UnityEngine.EventSystems;

public class SingletonEventSystem : MonoBehaviour
{
    private static bool eventSystemExists = false;

    void Awake()
    {
    
        if (FindObjectOfType<EventSystem>() != null && 
            FindObjectOfType<EventSystem>().GetInstanceID() != this.GetInstanceID())
        {
            Destroy(gameObject);
            return;
        }

        if (!eventSystemExists)
        {
            eventSystemExists = true;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}