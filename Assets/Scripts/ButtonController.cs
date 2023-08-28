using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button x1Button;
    public Button x10Button;
    public Button x100Button;
    public Button maxButton;
    public Button nextButton;
    
    private Button activeButton;

    public static int buttonValue = 1;

    private void Start()
    {
        // Set the initial state
        SetButtonState(x1Button, true);
        SetButtonState(x10Button, false);
        SetButtonState(x100Button, false);
        SetButtonState(maxButton, false);
        SetButtonState(nextButton, false);
    }

    public void OnButtonClick(Button button)
    {
        if (button == activeButton)
        {
            return;
        }
        // Disable the current active button
        if (activeButton != null)
        {
            SetButtonState(activeButton, false);
        }
        // Enable the clicked button
        SetButtonState(button, true);
    }

    private void SetButtonState(Button button, bool interactable)
    {
        button.interactable = interactable;
        if(interactable)
        {
            activeButton = button;
            // Bring the active button to the front
            activeButton.transform.SetAsLastSibling();
            GetButtonValue(button);
        }
    }

    // TODO: ADD HERE A NEXT BUTTON
    public void GetButtonValue(Button button)
    {
        // Determine the multiplier value based on the button clicked
        if (button == x10Button)
        {
            buttonValue = 10;
        }
        else if (button == x100Button)
        {
            buttonValue = 100;
        }
        else if (button == maxButton)
        {
            buttonValue = -1;
        }
        else if(button == nextButton)
        {
            buttonValue = 0;
        }
        else
        {
            buttonValue = 1; // Default value
        }
    }
}
