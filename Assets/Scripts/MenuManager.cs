using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject _menuPanel;
    [SerializeField] GameObject _creditsPanel;
    [SerializeField] GameObject _pausePanel;
    [SerializeField] GameObject _gamePanel;
    [SerializeField] GameObject _introPanel;
    [SerializeField] GameObject _defeatPanel;
    [SerializeField] GameObject _victoryPanel;

    GameObject[] _panels;

    private void Awake()
    {
        _panels = new List<GameObject>() { _menuPanel, _creditsPanel, _pausePanel, _gamePanel, _introPanel, _defeatPanel, _victoryPanel}.ToArray();
    }

    public void GamePanel()
    {
        ActivePanel(_gamePanel);
    }
    public void MenuPanel()
    {
        ActivePanel(_menuPanel);
    }

    public void IntroPanel()
    {
        ActivePanel(_introPanel);
    }

    public void DefeatPanel()
    {
        ActivePanel(_defeatPanel);
    }
    public void VictoryPanel()
    {
        ActivePanel(_victoryPanel);
    }

    public void CreditPanel()
    {
        ActivePanel(_creditsPanel);
    }

    private void ActivePanel(GameObject panel)
    {
        foreach (var item in _panels)
        {
            item.SetActive(item.name == panel.name);
        }
    }
}
