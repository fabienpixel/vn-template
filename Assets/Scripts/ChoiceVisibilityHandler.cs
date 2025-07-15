using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChoiceVisibilityHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InkStoryController storyController;
    [SerializeField] private Image backgroundImage;

    [Header("Fade Settings")]
    [SerializeField] private float fadeSpeed = 5f;
    [SerializeField] private float visibleAlpha = 1f;
    [SerializeField] private float hiddenAlpha = 0f;

    private void Awake()
    {
        if (storyController == null)
            storyController = FindObjectOfType<InkStoryController>();

        if (backgroundImage == null)
            backgroundImage = GetComponent<Image>();
    }

    private void Update()
    {
        if (backgroundImage == null || storyController == null)
            return;

        float targetAlpha = storyController.isChoicePanelActive ? visibleAlpha : hiddenAlpha;

        Color currentColor = backgroundImage.color;
        currentColor.a = Mathf.Lerp(currentColor.a, targetAlpha, Time.deltaTime * fadeSpeed);
        backgroundImage.color = currentColor;
    }
}
