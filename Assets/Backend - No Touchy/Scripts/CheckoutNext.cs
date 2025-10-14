using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class CheckoutNext : EditorWindow
{


    public static void SwitchBranch(string branchName)
    {
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
            UnityEngine.Debug.LogError($"Error switching branch: {error}");
        }
    }
}
