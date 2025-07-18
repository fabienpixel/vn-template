using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoicePanelVisibilityHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InkStoryController storyController;
    [SerializeField] private VNUIManager uIController;
    [SerializeField] private Image backgroundImage;

    private void Awake()
    {
        if (storyController == null)
            storyController = FindObjectOfType<InkStoryController>();

        if (backgroundImage == null)
            backgroundImage = GetComponent<Image>();

        if (uIController == null)
            uIController = FindObjectOfType<VNUIManager>();
    }

    private void Update()
    {
        if (backgroundImage == null || storyController == null || uIController == null)
            return;

        float targetAlpha = storyController.isChoicePanelActive ? uIController.visibleAlpha : uIController.hiddenAlpha;

        Color currentColor = backgroundImage.color;
        currentColor.a = Mathf.Lerp(currentColor.a, targetAlpha, Time.deltaTime * uIController.fadeSpeed);
        backgroundImage.color = currentColor;
    }
}
