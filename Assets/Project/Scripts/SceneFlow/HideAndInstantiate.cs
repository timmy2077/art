using UnityEngine;

public class HideAndInstantiate : MonoBehaviour
{
    public GameObject uiToHide;
    public GameObject prefabToInstantiate;

    public void OnAnimationFrame()
    {
        if (uiToHide != null)
        {
            CanvasGroup canvasGroup = uiToHide.GetComponentInParent<CanvasGroup>(true);
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0f;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }
            else
            {
                Debug.LogWarning("[HideAndInstantiate] 未找到 CanvasGroup，无法隐藏选关界面。", this);
            }
        }

        if (prefabToInstantiate != null)
        {
            Instantiate(prefabToInstantiate, Vector3.zero, Quaternion.identity);
        }

        gameObject.SetActive(false);
    }
}
