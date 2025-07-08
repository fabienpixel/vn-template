using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using TMPro;
using System.Collections;


public class Script : MonoBehaviour
{
    public TextAsset inkAsset;
    private Story _inkStory;
    public TextMeshProUGUI textUIElement;
    public Button[] choiceButtons;
    [SerializeField] private float typewriterSpeed = 0.02f;
    private Coroutine typewriterCoroutine;
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
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click
        {
            if (isTyping)
            {
                StopCoroutine(typewriterCoroutine);
                textUIElement.text = currentText;
                isTyping = false;
            }
            else
            {
                // Could call Continue if no choices, or wait for click if next line
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
    else
    {
        Debug.Log("textUIElement text: " + textUIElement.text);
    }

    // Continue with text and choices
    string text = _inkStory.ContinueMaximally(); // Continue without parsing tags/functions
    Debug.Log("Number of choices: " + _inkStory.currentChoices.Count);

    currentText = text;

     if (typewriterCoroutine != null)
            StopCoroutine(typewriterCoroutine);

     typewriterCoroutine = StartCoroutine(TypeText(currentText));

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
        foreach (char c in fullText)
        {
            textUIElement.text += c;
            yield return new WaitForSeconds(typewriterSpeed);
        }
        isTyping = false;
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
