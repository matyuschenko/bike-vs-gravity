using UnityEngine;

// Show this game object on mobile devices and hide otherwise
public class MobileUIHandler : MonoBehaviour
{

    void Start()
    {
        gameObject.SetActive(Application.isMobilePlatform);
    }
}