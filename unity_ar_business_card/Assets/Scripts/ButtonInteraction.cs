using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonInteraction : MonoBehaviour, IPointerClickHandler
{
    public string url; // The URL or mailto link
    private Button button; // Reference to the button component
    private Color originalColor; // To store the original button color
    public Color pressedColor = Color.gray; // Color to change to when pressed

    void Start()
    {
        button = GetComponent<Button>();
        originalColor = button.image.color;
    }

    // This method is called when the button is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        // Change the button color temporarily
        StartCoroutine(ChangeColorTemporarily());

        // Open the associated link
        if (!string.IsNullOrEmpty(url))
        {
            Application.OpenURL(url);
        }
    }

    private IEnumerator ChangeColorTemporarily()
    {
        button.image.color = pressedColor;
        yield return new WaitForSeconds(0.2f); // Wait for 0.2 seconds
        button.image.color = originalColor;
    }
}
