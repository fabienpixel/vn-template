using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoiceButtonVisibilityHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InkStoryController storyController;
    [SerializeField] private Button buttonComponent;
    [SerializeField] private TextMeshProUGUI buttonLabel;

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
        // Auto-fix missing reference if prefab was cloned
        if (storyController == null)
            storyController = FindObjectOfType<InkStoryController>();

        if (storyController == null || buttonComponent == null || buttonLabel == null)
            return;

        bool isVisible = storyController.isChoicePanelActive;

        // Toggle visibility and interactivity
        buttonComponent.interactable = isVisible;
        buttonComponent.image.enabled = isVisible;
        buttonLabel.enabled = isVisible;
    }
}
