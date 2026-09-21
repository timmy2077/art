using UnityEngine;
using UnityEngine.EventSystems;

public class ShowObjectOnClick : MonoBehaviour
{
    public GameObject targetUI;

    public void OnPointerClick()
    {
        if (targetUI != null)
        {
            targetUI.SetActive(true);
        }
    }
}
