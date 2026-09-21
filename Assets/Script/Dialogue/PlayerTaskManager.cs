using System.Collections.Generic;
using UnityEngine;

///=============================================================================
/// 玩家任务管理器（挂在玩家身上）
/// 只负责记录每个任务的完成状态（按任务ID区分）。
/// 任务要显示什么文字由 TaskInfoPanel 自己持有的 TaskDataStorage 配置。
/// 任务完成方式不做限制：触发器（TaskCompleteTrigger）或其他脚本都可以
/// 调用 CompleteTask(taskId) 来完成对应任务。
///=============================================================================
public class PlayerTaskManager : MonoBehaviour
{
    public static PlayerTaskManager Instance { get; private set; }

    /// <summary>已完成任务的ID集合</summary>
    private HashSet<int> completedTaskIds = new HashSet<int>();

    /// <summary>
    /// 任务状态变化事件（任务面板订阅此事件刷新显示）
    /// 参数：刚完成的任务ID；-1 表示接受了新任务、整体重置
    /// </summary>
    public static event System.Action<int> OnTaskStateChanged;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this); return; }
    }

    /// <summary>
    /// 接受任务：清空旧的完成记录，并通知UI刷新
    /// </summary>
    public void AcceptTask()
    {
        completedTaskIds.Clear();
        OnTaskStateChanged?.Invoke(-1);
    }

    /// <summary>
    /// 完成指定ID的任务（后续其他完成方式也调用这个方法）
    /// </summary>
    public void CompleteTask(int taskId)
    {
        if (completedTaskIds.Contains(taskId)) return;

        completedTaskIds.Add(taskId);
        Debug.Log($"[PlayerTaskManager] 任务已完成，taskId = {taskId}");

        OnTaskStateChanged?.Invoke(taskId);
    }

    /// <summary>查询某个任务是否已完成</summary>
    public bool IsTaskCompleted(int taskId)
    {
        return completedTaskIds.Contains(taskId);
    }
}
