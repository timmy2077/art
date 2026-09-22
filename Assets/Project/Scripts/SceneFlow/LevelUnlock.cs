using UnityEngine;

public class LevelUnlock : MonoBehaviour
{
    public int currentLevelId;
    public UIPageSwitch pageSwitch;

    private void Start()
    {
        RefreshLevelState();
    }

    /// <summary>
    /// 当前场景内按钮直接调用。
    /// </summary>
    public void OnUnlockButtonClick()
    {

        int nextLevelId = currentLevelId + 1;

        if (DataSystem.instance != null)
        {
            DataSystem.instance.UnlockLevel(nextLevelId);
        }

        int nextPanelIndex = nextLevelId - 1;
        if (pageSwitch != null && nextPanelIndex >= 0 && nextPanelIndex < pageSwitch.uiPanels.Length)
        {
            pageSwitch.SwitchToPage(nextPanelIndex);

            GameObject nextPanel = pageSwitch.uiPanels[nextPanelIndex];
            if (nextPanel != null)
            {
                PlayUnlockAnimation(nextPanel);
            }
        }
    }

    private void PlayUnlockAnimation(GameObject targetPanel)
    {
        LevelUnlock nextLevelUnlock = targetPanel.GetComponent<LevelUnlock>();
        if (nextLevelUnlock != null)
            nextLevelUnlock.PlayUnlockAnimation();
    }

    public void PlayUnlockAnimation()
    {
        Transform unlockedState = transform.Find("UnLocked");
        Transform lockedState = transform.Find("Locked");
        if (lockedState == null) return;

        if (unlockedState != null)
            unlockedState.gameObject.SetActive(false);
        lockedState.gameObject.SetActive(true);

        Animator animator = lockedState.GetComponent<Animator>();
        if (animator != null)
            animator.SetTrigger("Unlock");
        else
            Debug.LogWarning($"[LevelUnlock] {name} 的 Locked 节点缺少 Animator。", this);
    }

    public void RefreshLevelState()
    {
        bool isUnlocked = currentLevelId == 1;

        if (DataSystem.instance != null)
        {
            isUnlocked = DataSystem.instance.IsLevelUnlocked(currentLevelId);
        }

        Transform unlockedState = transform.Find("UnLocked");
        Transform lockedState = transform.Find("Locked");

        if (unlockedState != null)
        {
            unlockedState.gameObject.SetActive(isUnlocked);
        }
        if (lockedState != null)
        {
            lockedState.gameObject.SetActive(!isUnlocked);
        }
    }
}
