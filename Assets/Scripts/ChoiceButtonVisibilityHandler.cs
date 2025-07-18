using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoiceButtonVisibilityHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InkStoryController storyController;
    [SerializeField] private VNUIManager uIController;
    [SerializeField] private Button buttonComponent;
    [SerializeField] private TextMeshProUGUI buttonLabel;


    private void Awake()
    {
        if (storyController == null)
            storyController = FindObjectOfType<InkStoryController>();

        if (uIController == null)
            uIController = FindObjectOfType<VNUIManager>();

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

        float targetBGAlpha = shouldBeVisible ? uIController.visibleAlpha : uIController.hiddenAlpha;
        float targetTextAlpha = shouldBeVisible ? 1f : uIController.hiddenAlpha;

        // Fade button background
        if (buttonComponent.image != null)
        {
            Color bgColor = buttonComponent.image.color;
            bgColor.a = Mathf.Lerp(bgColor.a, targetBGAlpha, Time.deltaTime * uIController.fadeSpeed);
            buttonComponent.image.color = bgColor;
        }

        // Fade label text (always target full opacity when visible)
        Color textColor = buttonLabel.color;
        textColor.a = Mathf.Lerp(textColor.a, targetTextAlpha, Time.deltaTime * uIController.fadeSpeed);
        buttonLabel.color = textColor;

        // Toggle interactivity
        buttonComponent.interactable = shouldBeVisible;
    }
}
