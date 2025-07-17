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
    public bool isChoicePanelActive = false;


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
        // ⬇️ NEW logic: Left-lick to show choices when text is finished
        if (textChunks.Count == 0 && isTyping == false && _inkStory.currentChoices.Count > 0 && Input.GetMouseButtonDown(0))
        {
            isChoicePanelActive = true;
            uiManager.ShowCursor(false);
            return; // prevent double handling on same frame
        }

        // ⬇️ Existing logic: Left-click for normal interactions
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                uiManager.SkipTypewriter(currentText, OnTypewriterFinished);
                isTyping = false;
                waitingForClickToContinue = true;
                isChoicePanelActive = false;
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

        if (_inkStory.currentChoices.Count > 0)
        {
            isChoicePanelActive = false; // will be activated after final line click
            uiManager.DisplayChoices(_inkStory.currentChoices, ChooseChoice);
        }
        else
        {
            isChoicePanelActive = false;
            uiManager.HideChoices();
        }
    } // ✅ This closes RefreshUI


    void DisplayNextChunk()
    {
        if (textChunks.Count == 0)
        {
            waitingForClickToContinue = false;
            isChoicePanelActive = false;
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
        isChoicePanelActive = false;

        if (textChunks.Count == 0 && _inkStory.currentChoices.Count > 0)
        {
            isChoicePanelActive = false;
        }
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