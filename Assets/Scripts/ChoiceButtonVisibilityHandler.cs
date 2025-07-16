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
    [SerializeField] private float visibleAlpha = 1f;
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
        float targetAlpha = shouldBeVisible ? visibleAlpha : hiddenAlpha;

        // Fade button background
        if (buttonComponent.image != null)
        {
            Color bgColor = buttonComponent.image.color;
            bgColor.a = Mathf.Lerp(bgColor.a, targetAlpha, Time.deltaTime * fadeSpeed);
            buttonComponent.image.color = bgColor;
        }

        // Fade label text
        Color textColor = buttonLabel.color;
        textColor.a = Mathf.Lerp(textColor.a, targetAlpha, Time.deltaTime * fadeSpeed);
        buttonLabel.color = textColor;

        // Toggle interactivity
        buttonComponent.interactable = shouldBeVisible;
    }
}
