using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTabManager : MonoBehaviour
{
    public static PlayerTabManager Instance { get; private set; }

    [SerializeField] private GameObject socialTab;
    [SerializeField] private Button socialButton;

    [SerializeField] private GameObject statsTab;
    [SerializeField] private Button statsButton;

    [SerializeField] private GameObject inventoryTab;
    [SerializeField] private Button inventoryButton;

    [SerializeField] private Color highlightColor = Color.green;
    [SerializeField] private Color defaultColor = Color.white;

    private List<GameObject> tabs;
    private List<Button> buttons;
    private GameObject currentOpenTab;
    private Button currentHighlightedButton;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        tabs = new List<GameObject> { socialTab, statsTab, inventoryTab };
        buttons = new List<Button> { socialButton, statsButton, inventoryButton };

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