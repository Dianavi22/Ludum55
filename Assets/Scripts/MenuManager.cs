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

    GameObject[] _panels;

    private void Awake()
    {
        _panels = new List<GameObject>() { _menuPanel, _creditsPanel, _pausePanel, _gamePanel, _introPanel}.ToArray();
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
