using System.Diagnostics;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CheckoutNext
{
    public static string nextLevel = "Mission-21";
    public static string previousLevel = "Mission-19";

    [MenuItem("Cody's Unity Adventure/Next Level", false, 100)]
    static void LoadNextLevel()
    {
        CheckoutNext.SwitchBranch(nextLevel);
       // EditorUtility.DisplayDialog("Switching Branch", "Wait for a while until you see a reload scene popup. Kinda like this one.", "OK");
        AssetDatabase.Refresh();
    }

    //[MenuItem("Cody's Unity Adventure/Next Level", true)]

    //static bool LoadNextLevelValidation()
    //{
    //    return false;
    //}
    [MenuItem("Cody's Unity Adventure/Reset Level", false, 1)]
    static void ReloadLevel()
    {
        SaveCurrentScene();
        Reset();
        AssetDatabase.Refresh();
    }

    public static void SaveCurrentScene()
    {
        // Save the currently open scene
        bool success = EditorApplication.SaveScene();

        if (success)
        {
            UnityEngine.Debug.Log("Saved scene to reset it completely.");
        }
        else
        {
            UnityEngine.Debug.LogError("Failed to save the scene for a reset.");
        }
    }

    [MenuItem("Cody's Unity Adventure/Previous Level", false, 100)]
    static void LoadPreviousLevel()
    {
        CheckoutNext.SwitchBranch(previousLevel);
        AssetDatabase.Refresh();
    }

    public static void Reset()
    {
        Process process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "git",
                Arguments = $"reset --hard",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        process.Start();
        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();
        process.WaitForExit();

        if (string.IsNullOrEmpty(error))
        {
            UnityEngine.Debug.Log($"Reset success");
        }
        else
        {
            UnityEngine.Debug.LogError($"Error reseting");
        }
    }
    public static void SwitchBranch(string branchName)
    {
        Reset();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Process process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "git",
                Arguments = $"checkout {branchName}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        process.Start();
        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();
        process.WaitForExit();

        if (string.IsNullOrEmpty(error))
        {
            UnityEngine.Debug.Log($"Switched to branch: {branchName}\n{output}");
        }
        else
        {
            UnityEngine.Debug.LogWarning($"{error}");
        }
    }
}
