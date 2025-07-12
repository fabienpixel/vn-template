using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;


public class Script : MonoBehaviour
{
    public TextAsset inkAsset;
    private Story _inkStory;
    public TextMeshProUGUI textUIElement;
    public TextMeshProUGUI speakerUIElement;
    public Button[] choiceButtons;
    [SerializeField] private Image blinkingCursorImage;
    [SerializeField] private Animator blinkingCursorAnimator;
    [SerializeField] private Sprite cursorSprite;
    [SerializeField] private float typewriterSpeed = 0.02f;
    [SerializeField] private float punctuationPauseShort = 0.1f;  // For commas, semicolons
    [SerializeField] private float punctuationPauseLong = 0.3f;   // For periods, question marks, exclamations
    [SerializeField] private float punctuationPauseEllipsis = 0.6f; // For "…"
    private Coroutine typewriterCoroutine;
    private Queue<string> textChunks = new Queue<string>();
    private bool waitingForClickToContinue = false;
    private bool isTyping = false;
    private string currentText = "";
    public AudioController audioController; // Reference to the AudioController component
    public BackgroundController backgroundController; // Reference to the BackgroundController component

    // Start is called before the first frame update
    void Start()
    {
        _inkStory = new Story(inkAsset.text);
        BindPlaySoundFunction();
        BindSwapBackgroundFunction();
        RefreshUI();

        if (blinkingCursorImage != null && cursorSprite != null)
            blinkingCursorImage.sprite = cursorSprite;

        ShowCursor(false);
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                StopCoroutine(typewriterCoroutine);
                textUIElement.text = currentText;
                isTyping = false;
                waitingForClickToContinue = true;
                ShowCursor(true);
            }
            else if (waitingForClickToContinue)
            {
                ShowCursor(false);
                waitingForClickToContinue = false;
                DisplayNextChunk();
            }
        }
    }


    // Bind external function to play sound
    void BindPlaySoundFunction()
    {
        _inkStory.BindExternalFunction("playSound", (string soundName) => {
            if (!string.IsNullOrEmpty(soundName)) {
                audioController.Play(soundName);
                Debug.Log("Encountered Play Sound function for " + soundName);
            }
        });
    }


    // Bind external function to swap background
    void BindSwapBackgroundFunction()
    {
        _inkStory.BindExternalFunction("swapBackground", (string backgroundName) => {
            if (!string.IsNullOrEmpty(backgroundName))
            {
                backgroundController.SwapBackground(backgroundName);
                Debug.Log("Encountered Swap Background function for " + backgroundName);
            }
        });
    }

    void RefreshUI()
    {
        Debug.Log("Refreshing UI...");

        if (textUIElement == null)
        {
            Debug.LogError("textUIElement is null!");
            return;
        }

        // Continue the Ink story
        string text = _inkStory.ContinueMaximally();
        Debug.Log("Number of choices: " + _inkStory.currentChoices.Count);

        // ✅ SPEAKER TAG PARSING
        string speaker = "";
        foreach (string tag in _inkStory.currentTags)
        {
            if (tag.StartsWith("speaker:"))
            {
                speaker = tag.Substring("speaker:".Length).Trim();
                break;
            }
        }
        Debug.Log("Speaker tag found: [" + speaker + "]");

        if (speakerUIElement != null)
        {
            speakerUIElement.text = speaker;
            speakerUIElement.gameObject.SetActive(true); // optional: keep visible even if empty
        }

        // Set currentText for typewriter
        textChunks.Clear();
        foreach (string chunk in text.Split('\n'))
        {
            if (!string.IsNullOrWhiteSpace(chunk))
                textChunks.Enqueue(chunk.Trim());
        }
        DisplayNextChunk();

        if (typewriterCoroutine != null)
            StopCoroutine(typewriterCoroutine);

        typewriterCoroutine = StartCoroutine(TypeText(currentText));

        // Set up choices
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            int choiceIndex = i;

            if (i < _inkStory.currentChoices.Count)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() => ChooseChoice(choiceIndex));
                choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = _inkStory.currentChoices[i].text;
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }

        Debug.Log("RefreshUI completed.");
    }

    void DisplayNextChunk()
    {
        if (textChunks.Count == 0)
        {
            waitingForClickToContinue = false;
            return;
        }

        string nextChunk = textChunks.Dequeue();
        currentText = nextChunk;

        if (typewriterCoroutine != null)
            StopCoroutine(typewriterCoroutine);

        typewriterCoroutine = StartCoroutine(TypeText(currentText));
    }


    void ShowCursor(bool show)
    {
        if (blinkingCursorAnimator != null)
            blinkingCursorAnimator.SetBool("IsVisible", show);
    }


    public void ChooseChoice(int choiceIndex)
    {
        if (choiceIndex >= 0 && choiceIndex < _inkStory.currentChoices.Count)
        {
            _inkStory.ChooseChoiceIndex(choiceIndex);
            RefreshUI();
        }
        else
        {
            Debug.LogError("Invalid choice index: " + choiceIndex);
        }
    }

    // Typewriter method
    IEnumerator TypeText(string fullText)
    {
        isTyping = true;
        textUIElement.text = "";

        for (int i = 0; i < fullText.Length; i++)
        {
            char c = fullText[i];
            textUIElement.text += c;

            // Check for punctuation pauses
            if (c == '.' || c == '!' || c == '?')
                yield return new WaitForSeconds(punctuationPauseLong);
            else if (c == ',' || c == ';')
                yield return new WaitForSeconds(punctuationPauseShort);
            else if (c == '…' || (c == '.' && i + 2 < fullText.Length && fullText[i + 1] == '.' && fullText[i + 2] == '.'))
                yield return new WaitForSeconds(punctuationPauseEllipsis);
            else
                yield return new WaitForSeconds(typewriterSpeed);
        }

        isTyping = false;
        waitingForClickToContinue = true;
        ShowCursor(true);
    }


    // Run an Ink function to play sound or trigger other actions
    public void PlaySoundFromInk()
    {
        _inkStory.EvaluateFunction("playSound", "ambience-bird-forrest");
    }

    // Run an Ink function to swap the background
    public void SwapBackgroundFromInk()
    {
        _inkStory.EvaluateFunction("swapBackground", "backgroundName");
    }
}
