using System.Collections.Generic;
using UnityEngine;

public static class DebugOnce
{
    private static HashSet<string> _loggedMessages = new HashSet<string>();

    public static void LogWarningOnce(string message)
    {
        if (_loggedMessages.Contains(message))
            return;

        Debug.LogWarning(message);
        _loggedMessages.Add(message);
    }

    public static void LogOnce(string message)
    {
        if (_loggedMessages.Contains(message))
            return;

        Debug.Log(message);
        _loggedMessages.Add(message);
    }

    public static void Reset()
    {
        _loggedMessages.Clear();
    }
}
