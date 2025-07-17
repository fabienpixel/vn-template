using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoiceButtonVisibilityHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InkStoryController storyController;
    [SerializeField] private Button buttonComponent;
    [SerializeField] private TextMeshProUGUI buttonLabel;

    [Header("Fade Settings")]
    [SerializeField] private float fadeSpeed = 5f;
    [SerializeField] private float visibleAlpha = 1f;   // For background only
    [SerializeField] private float hiddenAlpha = 0f;

    private void Start()
    {
        if (storyController == null)
            storyController = FindObjectOfType<InkStoryController>();

        if (buttonComponent == null)
            buttonComponent = GetComponent<Button>();

        if (buttonLabel == null)
            buttonLabel = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (storyController == null)
            storyController = FindObjectOfType<InkStoryController>();

        if (storyController == null || buttonComponent == null || buttonLabel == null)
            return;

        bool shouldBeVisible = storyController.isChoicePanelActive;

        float targetBGAlpha = shouldBeVisible ? visibleAlpha : hiddenAlpha;
        float targetTextAlpha = shouldBeVisible ? 1f : hiddenAlpha;

        // Fade button background
        if (buttonComponent.image != null)
        {
            Color bgColor = buttonComponent.image.color;
            bgColor.a = Mathf.Lerp(bgColor.a, targetBGAlpha, Time.deltaTime * fadeSpeed);
            buttonComponent.image.color = bgColor;
        }

        // Fade label text (always target full opacity when visible)
        Color textColor = buttonLabel.color;
        textColor.a = Mathf.Lerp(textColor.a, targetTextAlpha, Time.deltaTime * fadeSpeed);
        buttonLabel.color = textColor;

        // Toggle interactivity
        buttonComponent.interactable = shouldBeVisible;
    }
}
