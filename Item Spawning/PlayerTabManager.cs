using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTabManager : MonoBehaviour
{

    [SerializeField] private GameObject socialTab;
    [SerializeField] private Button socialButton;

    [SerializeField] private GameObject statsTab;
    [SerializeField] private Button statsButton;

    [SerializeField] private GameObject inventoryTab;
    [SerializeField] private Button inventoryButton;

    [SerializeField] private Color highlightColor = Color.green; // Color slot for highlight color
    [SerializeField] private Color defaultColor = Color.white;   // Color slot for default color

    private List<GameObject> tabs;
    private List<Button> buttons;
    private GameObject currentOpenTab;
    private Button currentHighlightedButton;

    private void Awake()
    {
        // Initialize lists with all tab panels and buttons
        tabs = new List<GameObject> { socialTab, statsTab, inventoryTab };
        buttons = new List<Button> { socialButton, statsButton, inventoryButton };

        // Ensure all tabs are closed and no buttons are highlighted at the start
        CloseAllTabs();
    }

    // Closes all tabs and removes highlight from all buttons
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

    // Resets button highlight to the default color
    private void ResetButtonHighlight(Button button)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = defaultColor;
        colors.selectedColor = defaultColor;
        button.colors = colors;
    }

    // Highlights the specified button with the highlight color
    private void HighlightButton(Button button)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = highlightColor;
        colors.selectedColor = highlightColor;
        button.colors = colors;
    }

    // Toggles the specified tab: opens it if closed, closes it if already open
    private void ToggleTab(GameObject tab, Button button)
    {
        if (currentOpenTab == tab) // If the tab is already open, close it
        {
            CloseAllTabs();
        }
        else // Otherwise, close other tabs, open this one, and highlight its button
        {
            CloseAllTabs();
            tab.SetActive(true);
            HighlightButton(button);
            currentOpenTab = tab;
            currentHighlightedButton = button;
        }
    }

    // Public methods to open each tab via ToggleTab

    public void OpenSocial()
    {
        ToggleTab(socialTab, socialButton);
    }

    public void OpenStats()
    {
        ToggleTab(statsTab, statsButton);
    }

    public void OpenInventory()
    {
        ToggleTab(inventoryTab, inventoryButton);
    }
}