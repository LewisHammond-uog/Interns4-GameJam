using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Variables.
    public TextMeshProUGUI[] textBoxes;
    public string[] events;

    private int numberOfOptions;
    private int selectedOption;

    private bool down;
    private bool up;
    private bool space;
    
    private bool isInvisible;
    private bool hasChanged;
    private float step = 1f;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        numberOfOptions = textBoxes.Length - 1;
        selectedOption = 0;

        // Set the visual indicator for which option you are on.
        SetTextColors();
    }

    // Update is called once per frame
    void Update()
    {
        up = Input.GetButtonDown("Up");
        down = Input.GetButtonDown("Down");
        space = Input.GetButtonDown("Jump");

        // Input telling it to go up.
        if (down)
        {
            selectedOption += 1;

            // If at end of list go back to top.
            if (selectedOption > numberOfOptions)
            {
                selectedOption = 0;
            }

            SetTextColors();
        }
        // Input telling it to go down.
        else if (up)
        {
            selectedOption -= 1;

            // If at end of list go back to top.
            if (selectedOption < 0)
            {
                selectedOption = numberOfOptions;
            }

            SetTextColors();
        }
        else if (space)
        {
            Invoke("Select", 0.05f);
        }
    }

    // Make sure all others will be black.
    void SetTextColors()
    {
        foreach (TextMeshProUGUI text in textBoxes)
        {
            // Make sure all others will be black.
            text.color = new Color32(0, 0, 0, 255);
        }

        // Set the visual indicator for which option you are on.
        textBoxes[selectedOption].color = new Color32(255, 255, 255, 255);
    }

    void Select()
    {
        switch (selectedOption)
        {
            case 0:
                Invoke(events[selectedOption], Mathf.Epsilon);
                break;
            case 1:
                Invoke(events[selectedOption], Mathf.Epsilon);
                break;
            case 2:
                Invoke(events[selectedOption], Mathf.Epsilon);
                break;
        }
    }

    // Menu actions.
    public void LoadGame()
    {
        SceneManager.LoadScene("MainWorkingScene");
    }

    public void LoadControls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
