using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class CheckoutNext
{
    public static string nextLevel = "Mission-1";

    [MenuItem("Cody/Next Level")]
    static void LoadNextLevel()
    {
        CheckoutNext.SwitchBranch(nextLevel);
        EditorUtility.DisplayDialog("Switching Branch", "Wait for a while until you see a reload scene popup. Kinda like this one.", "OK");
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
