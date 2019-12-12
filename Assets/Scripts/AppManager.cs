using Microsoft.Azure.SpatialAnchors.Unity;
using System;
using TMPro;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    [SerializeField]
    private SpatialAnchorManager spatialAnchorManager;

    [SerializeField]
    private TextMeshProUGUI feedbackText;

    private void Awake()
    {
        Application.logMessageReceivedThreaded += Application_logMessageReceivedThreaded;
    }

    private void OnDestroy()
    {
        Application.logMessageReceivedThreaded -= Application_logMessageReceivedThreaded;
    }

    private void Application_logMessageReceivedThreaded(string condition, string stackTrace, LogType type)
    {
        UnityDispatcher.InvokeOnAppThread(() => feedbackText.text += $"\nLOG: {condition}.");
    }

    public async void CreateSession()
    {
        feedbackText.text += $"\nCreating session.";

        try
        {
            await spatialAnchorManager.CreateSessionAsync();

            feedbackText.text += $"\nSession created.";
        }
        catch (Exception ex)
        {
            feedbackText.text += $"\nError while creating session: {ex.Message}";
        }
    }
}
