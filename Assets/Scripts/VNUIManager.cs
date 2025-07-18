using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;

public class VNUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI textUIElement;
    [SerializeField] private GameObject choiceButtonPrefab;
    [SerializeField] private Transform choicePanel;
    [SerializeField] public float fadeSpeed = 5f;
    [SerializeField] public float visibleAlpha = 1f;
    [SerializeField] public float hiddenAlpha = 0f;
    [SerializeField] private TextMeshProUGUI speakerUIElement;

    [Header("Typewriter Settings")]
    [SerializeField] private float typewriterSpeed = 0.02f;
    [SerializeField] private float punctuationPauseShort = 0.1f;
    [SerializeField] private float punctuationPauseLong = 0.3f;
    [SerializeField] private float punctuationPauseEllipsis = 0.6f;

    [Header("Cursor")]
    [SerializeField] private Animator blinkingCursorAnimator;

    [Header("Optional External Systems")]
    public AudioController audioController;
    public BackgroundController backgroundController;

    private Coroutine typewriterCoroutine;

    public void SetSpeaker(string name)
    {
        if (speakerUIElement != null)
        {
            speakerUIElement.text = name;
            speakerUIElement.gameObject.SetActive(!string.IsNullOrEmpty(name));
        }
    }

    public void DisplayChoices(List<Choice> choices, System.Action<int> onChoiceSelected)
    {
        // Clear previous buttons
        foreach (Transform child in choicePanel)
            Destroy(child.gameObject);

        // Create a button per Ink choice
        foreach (var choice in choices)
        {
            GameObject buttonGO = Instantiate(choiceButtonPrefab, choicePanel);
            TMP_Text label = buttonGO.GetComponentInChildren<TMP_Text>();
            if (label != null)
                label.text = choice.text;

            Button button = buttonGO.GetComponent<Button>();
            if (button != null)
            {
                int index = choice.index;
                button.onClick.AddListener(() =>
                {
                    onChoiceSelected.Invoke(index);
                });
            }
        }

        choicePanel.gameObject.SetActive(true);
    }

    public void HideChoices()
    {
        choicePanel.gameObject.SetActive(false);
    }

    public void ShowCursor(bool show)
    {
        if (blinkingCursorAnimator != null)
            blinkingCursorAnimator.SetBool("IsVisible", show);
    }

    public void SetTextInstant(string fullText)
    {
        if (textUIElement != null)
            textUIElement.text = fullText;
    }

    public Coroutine TypeText(string fullText, System.Action onComplete)
    {
        if (typewriterCoroutine != null)
            StopCoroutine(typewriterCoroutine);

        typewriterCoroutine = StartCoroutine(TypeTextCoroutine(fullText, onComplete));
        return typewriterCoroutine;
    }

    private IEnumerator TypeTextCoroutine(string fullText, System.Action onComplete)
    {
        textUIElement.text = "";

        for (int i = 0; i < fullText.Length; i++)
        {
            char c = fullText[i];
            textUIElement.text += c;

            if (c == '.' || c == '!' || c == '?')
                yield return new WaitForSeconds(punctuationPauseLong);
            else if (c == ',' || c == ';')
                yield return new WaitForSeconds(punctuationPauseShort);
            else if (c == '…' || (c == '.' && i + 2 < fullText.Length && fullText[i + 1] == '.' && fullText[i + 2] == '.'))
                yield return new WaitForSeconds(punctuationPauseEllipsis);
            else
                yield return new WaitForSeconds(typewriterSpeed);
        }

        typewriterCoroutine = null;
        onComplete?.Invoke();
    }

    public void SkipTypewriter(string fullText, System.Action onComplete)
    {
        if (typewriterCoroutine != null)
        {
            StopCoroutine(typewriterCoroutine);
            textUIElement.text = fullText;
            typewriterCoroutine = null;
            onComplete?.Invoke();
        }
    }


    public void PlaySound(string soundName)
    {
        if (audioController != null)
            audioController.Play(soundName);
    }

    public void SwapBackground(string backgroundName)
    {
        if (backgroundController != null)
            backgroundController.SwapBackground(backgroundName);
    }

    [Header("Advanced Typewriter")]
    [SerializeField] private TextMeshProUGUI previewTextUIElement;

    public Coroutine TypeTextSmartWrapped(string fullText, System.Action onComplete)
    {
        if (typewriterCoroutine != null)
            StopCoroutine(typewriterCoroutine);

        typewriterCoroutine = StartCoroutine(TypeTextSmartWrappedCoroutine(fullText, onComplete));
        return typewriterCoroutine;
    }

    private IEnumerator TypeTextSmartWrappedCoroutine(string fullText, System.Action onComplete)
    {
        textUIElement.text = "";
        previewTextUIElement.text = "";
        previewTextUIElement.color = new Color(1, 1, 1, 0); // invisible, used only for measurement

        string[] words = fullText.Split(' ');
        string displayed = "";
        string lineInProgress = "";
        int currentLineCount = 1;

        foreach (string word in words)
        {
            string trialLine = lineInProgress + word + " ";
            previewTextUIElement.text = displayed + trialLine;
            previewTextUIElement.ForceMeshUpdate();

            int newLineCount = previewTextUIElement.textInfo.lineCount;
            if (newLineCount > currentLineCount)
            {
                displayed += "\n";
                currentLineCount = newLineCount;
                lineInProgress = word + " ";
            }
            else
            {
                lineInProgress = trialLine;
            }

            foreach (char c in word + " ")
            {
                textUIElement.text = displayed + lineInProgress.Substring(0, (word + " ").IndexOf(c) + 1);
                if (c == '.' || c == '!' || c == '?')
                    yield return new WaitForSeconds(punctuationPauseLong);
                else if (c == ',' || c == ';')
                    yield return new WaitForSeconds(punctuationPauseShort);
                else if (c == '…')
                    yield return new WaitForSeconds(punctuationPauseEllipsis);
                else
                    yield return new WaitForSeconds(typewriterSpeed);
            }

            displayed += lineInProgress;
            lineInProgress = "";
        }

        typewriterCoroutine = null;
        onComplete?.Invoke();
    }

}