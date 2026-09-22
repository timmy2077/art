using UnityEngine;

public class DataSystem : MonoBehaviour
{
    public static DataSystem instance;

    [Header("数据持久化设置")]
    public bool enablePersistence = false;

    private const string KEY_PLAYER_NAME = "PlayerName";
    private const string KEY_LEVEL_UNLOCK_PREFIX = "LevelUnlocked_";
    private const string KEY_SETTINGS_VOLUME = "SettingsVolume";
    private const string KEY_SETTINGS_MUSIC = "SettingsMusic";
    private const string KEY_SETTINGS_SFX = "SettingsSFX";

    private string cachedPlayerName = "Player";
    private bool[] cachedLevelUnlocked = new bool[10];
    private float cachedVolume = 1f;
    private bool cachedMusic = true;
    private bool cachedSFX = true;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadCache();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadCache()
    {
        if (enablePersistence)
        {
            cachedPlayerName = PlayerPrefs.GetString(KEY_PLAYER_NAME, "Player");
            cachedVolume = PlayerPrefs.GetFloat(KEY_SETTINGS_VOLUME, 1f);
            cachedMusic = PlayerPrefs.GetInt(KEY_SETTINGS_MUSIC, 1) == 1;
            cachedSFX = PlayerPrefs.GetInt(KEY_SETTINGS_SFX, 1) == 1;

            for (int i = 0; i < cachedLevelUnlocked.Length; i++)
            {
                cachedLevelUnlocked[i] = PlayerPrefs.GetInt(KEY_LEVEL_UNLOCK_PREFIX + (i + 1), i == 0 ? 1 : 0) == 1;
            }
        }
        else
        {
            cachedLevelUnlocked[0] = true;
        }
    }

    public string GetPlayerName()
    {
        return cachedPlayerName;
    }

    public void SavePlayerName(string name)
    {
        cachedPlayerName = name;
        if (enablePersistence)
        {
            PlayerPrefs.SetString(KEY_PLAYER_NAME, name);
            PlayerPrefs.Save();
        }
    }

    public bool IsLevelUnlocked(int levelId)
    {
        if (levelId == 1)
        {
            return true;
        }
        int index = levelId - 1;
        if (index >= 0 && index < cachedLevelUnlocked.Length)
        {
            return cachedLevelUnlocked[index];
        }
        return enablePersistence ? PlayerPrefs.GetInt(KEY_LEVEL_UNLOCK_PREFIX + levelId, 0) == 1 : false;
    }

    public void UnlockLevel(int levelId)
    {
        int index = levelId - 1;
        if (index >= 0 && index < cachedLevelUnlocked.Length)
        {
            cachedLevelUnlocked[index] = true;
        }
        if (enablePersistence)
        {
            PlayerPrefs.SetInt(KEY_LEVEL_UNLOCK_PREFIX + levelId, 1);
            PlayerPrefs.Save();
        }
    }

    public float GetSettingsVolume()
    {
        return cachedVolume;
    }

    public void SaveSettingsVolume(float volume)
    {
        cachedVolume = Mathf.Clamp01(volume);
        if (GameAudio.Instance != null) GameAudio.Instance.RefreshSettings();
        if (enablePersistence)
        {
            PlayerPrefs.SetFloat(KEY_SETTINGS_VOLUME, cachedVolume);
            PlayerPrefs.Save();
        }
    }

    public bool GetSettingsMusic()
    {
        return cachedMusic;
    }

    public void SaveSettingsMusic(bool enabled)
    {
        cachedMusic = enabled;
        if (GameAudio.Instance != null) GameAudio.Instance.RefreshSettings();
        if (enablePersistence)
        {
            PlayerPrefs.SetInt(KEY_SETTINGS_MUSIC, enabled ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public bool GetSettingsSFX()
    {
        return cachedSFX;
    }

    public void SaveSettingsSFX(bool enabled)
    {
        cachedSFX = enabled;
        if (GameAudio.Instance != null) GameAudio.Instance.RefreshSettings();
        if (enablePersistence)
        {
            PlayerPrefs.SetInt(KEY_SETTINGS_SFX, enabled ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public void ClearAllData()
    {
        cachedPlayerName = "Player";
        cachedVolume = 1f;
        cachedMusic = true;
        cachedSFX = true;
        for (int i = 0; i < cachedLevelUnlocked.Length; i++)
        {
            cachedLevelUnlocked[i] = (i == 0);
        }
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        if (GameAudio.Instance != null) GameAudio.Instance.RefreshSettings();
    }
}


// using UnityEngine;

// public class DataSystem : MonoBehaviour
// {
//     public static DataSystem instance;

//     private const string KEY_PLAYER_NAME = "PlayerName";
//     private const string KEY_LEVEL_UNLOCK_PREFIX = "LevelUnlocked_";
//     private const string KEY_SETTINGS_VOLUME = "SettingsVolume";
//     private const string KEY_SETTINGS_MUSIC = "SettingsMusic";
//     private const string KEY_SETTINGS_SFX = "SettingsSFX";

//     private void Awake()
//     {
//         if (instance == null)
//         {
//             instance = this;
//             DontDestroyOnLoad(gameObject);
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }

//     public string GetPlayerName()
//     {
//         return PlayerPrefs.GetString(KEY_PLAYER_NAME, "Player");
//     }

//     public void SavePlayerName(string name)
//     {
//         PlayerPrefs.SetString(KEY_PLAYER_NAME, name);
//         PlayerPrefs.Save();
//     }

//     public bool IsLevelUnlocked(int levelId)
//     {
//         if (levelId == 1)
//         {
//             return true;
//         }
//         return PlayerPrefs.GetInt(KEY_LEVEL_UNLOCK_PREFIX + levelId, 0) == 1;
//     }

//     public void UnlockLevel(int levelId)
//     {
//         PlayerPrefs.SetInt(KEY_LEVEL_UNLOCK_PREFIX + levelId, 1);
//         PlayerPrefs.Save();
//     }

//     public float GetSettingsVolume()
//     {
//         return PlayerPrefs.GetFloat(KEY_SETTINGS_VOLUME, 1f);
//     }

//     public void SaveSettingsVolume(float volume)
//     {
//         PlayerPrefs.SetFloat(KEY_SETTINGS_VOLUME, Mathf.Clamp01(volume));
//         PlayerPrefs.Save();
//     }

//     public bool GetSettingsMusic()
//     {
//         return PlayerPrefs.GetInt(KEY_SETTINGS_MUSIC, 1) == 1;
//     }

//     public void SaveSettingsMusic(bool enabled)
//     {
//         PlayerPrefs.SetInt(KEY_SETTINGS_MUSIC, enabled ? 1 : 0);
//         PlayerPrefs.Save();
//     }

//     public bool GetSettingsSFX()
//     {
//         return PlayerPrefs.GetInt(KEY_SETTINGS_SFX, 1) == 1;
//     }

//     public void SaveSettingsSFX(bool enabled)
//     {
//         PlayerPrefs.SetInt(KEY_SETTINGS_SFX, enabled ? 1 : 0);
//         PlayerPrefs.Save();
//     }

//     public void ClearAllData()
//     {
//         PlayerPrefs.DeleteAll();
//         PlayerPrefs.Save();
//     }
// }
