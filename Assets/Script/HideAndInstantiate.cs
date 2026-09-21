using UnityEngine;

public class HideAndInstantiate : MonoBehaviour
{
    public GameObject uiToHide;
    public GameObject prefabToInstantiate;

    public void OnAnimationFrame()
    {
        if (uiToHide != null)
        {
            uiToHide.SetActive(false);
        }

        if (prefabToInstantiate != null)
        {
            Instantiate(prefabToInstantiate, Vector3.zero, Quaternion.identity);
        }

        gameObject.SetActive(false);
    }
}