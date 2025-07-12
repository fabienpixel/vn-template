using UnityEngine;
using Ink.Runtime;
using System.Collections.Generic;

public class InkStoryController : MonoBehaviour
{
    [SerializeField] private TextAsset inkAsset;
    private Story _inkStory;

    [SerializeField] private VNUIManager uiManager;

    private Queue<string> textChunks = new Queue<string>();
    private bool waitingForClickToContinue = false;
    private bool isTyping = false;
    private string currentText = "";

    void Start()
    {
        _inkStory = new Story(inkAsset.text);
        BindPlaySoundFunction();
        BindSwapBackgroundFunction();
        RefreshUI();
        uiManager.ShowCursor(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                uiManager.SkipTypewriter(currentText, OnTypewriterFinished);
                isTyping = false;
                waitingForClickToContinue = true;
            }
            else if (waitingForClickToContinue)
            {
                uiManager.ShowCursor(false);
                waitingForClickToContinue = false;
                DisplayNextChunk();
            }
        }
    }

    void RefreshUI()
    {
        string text = _inkStory.ContinueMaximally();
        string speaker = "";

        foreach (string tag in _inkStory.currentTags)
        {
            if (tag.StartsWith("speaker:"))
            {
                speaker = tag.Substring("speaker:".Length).Trim();
                break;
            }
        }

        uiManager.SetSpeaker(speaker);

        textChunks.Clear();
        foreach (string chunk in text.Split('\n'))
        {
            if (!string.IsNullOrWhiteSpace(chunk))
                textChunks.Enqueue(chunk.Trim());
        }
        DisplayNextChunk();
        uiManager.DisplayChoices(_inkStory.currentChoices, ChooseChoice);
    }

    void DisplayNextChunk()
    {
        if (textChunks.Count == 0)
        {
            waitingForClickToContinue = false;
            return;
        }

        currentText = textChunks.Dequeue();
        uiManager.TypeTextSmartWrapped(currentText, OnTypewriterFinished);
        isTyping = true;
    }

    void OnTypewriterFinished()
    {
        isTyping = false;
        waitingForClickToContinue = true;
        uiManager.ShowCursor(true);
    }

    public void ChooseChoice(int index)
    {
        _inkStory.ChooseChoiceIndex(index);
        RefreshUI();
    }

    void BindPlaySoundFunction()
    {
        _inkStory.BindExternalFunction("playSound", (string soundName) => {
            uiManager.PlaySound(soundName);
        });
    }

    void BindSwapBackgroundFunction()
    {
        _inkStory.BindExternalFunction("swapBackground", (string backgroundName) => {
            uiManager.SwapBackground(backgroundName);
        });
    }
}