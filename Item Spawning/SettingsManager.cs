using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsTab;
    [SerializeField] private Button settingsButton;

    [SerializeField] private Color highlightColor = Color.green; // Color slot for highlight color
    [SerializeField] private Color defaultColor = Color.white;   // Color slot for default color

    private List<GameObject> tabs;
    private List<Button> buttons;
    private GameObject currentOpenTab;
    private Button currentHighlightedButton;

    private void Awake()
    {
        tabs = new List<GameObject> { settingsTab };
        buttons = new List<Button> { settingsButton };

        CloseAllTabs();
    }

    public void CloseAllTabs()
    {
        foreach (GameObject tab in tabs)
        {
            tab.SetActive(false);
        }

        foreach (Button button in buttons)
        {
            ResetButtonHighlight(button);
        }

        currentOpenTab = null;
        currentHighlightedButton = null;
    }

    private void ResetButtonHighlight(Button button)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = defaultColor;
        colors.selectedColor = defaultColor;
        button.colors = colors;
    }

    private void HighlightButton(Button button)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = highlightColor;
        colors.selectedColor = highlightColor;
        button.colors = colors;
    }

    private void ToggleTab(GameObject tab, Button button)
    {
        if (currentOpenTab == tab)
        {
            CloseAllTabs();
        }
        else
        {
            CloseAllTabs();
            tab.SetActive(true);
            HighlightButton(button);
            currentOpenTab = tab;
            currentHighlightedButton = button;
        }
    }

    public void OpenSettings()
    {
        ToggleTab(settingsTab, settingsButton);
    }

    public void Logout()
    {
        // Perform Web3 logout actions here
        Debug.Log("Logout initiated");
        CloseAllTabs();

        // Make cursor visible
        Cursor.visible = true;

        // Optionally, notify PlayerTabManager
        PlayerTabManager.Instance?.CloseAllTabs();
    }
}