using UnityEditor;
using UnityEngine;

public class DeletePrefs : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("AppOnPause/Delete Save", false, -1000)]
    public static void DeleteSave()
    {
        PlayerPrefs.DeleteKey("TotalPlayedTime");
        PlayerPrefs.DeleteKey("TotalSessions");
    }
#endif
}
