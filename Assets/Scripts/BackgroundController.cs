using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    public Image backgroundImage;

    // Function to swap the background image
    public void SwapBackground(string backgroundName)
    {
        // Load the new background image from Resources/Backgrounds
        Sprite newBackground = Resources.Load<Sprite>("Backgrounds/" + backgroundName);

        // If the new background exists, set it as the source image
        if (newBackground != null)
        {
            backgroundImage.sprite = newBackground;
        }
        else
        {
            Debug.LogWarning("Background image not found: " + backgroundName);
        }
    }
}
