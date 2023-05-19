using System;
using UnityEngine;

namespace Runner
{
    public class UIController : MonoBehaviour
    {
        private enum PanelType
        {
            Menu,
            Game,
            Win,
            Fail
        }

        private MenuPanel _menuPanel;
        private GamePanel _gamePanel;
        private WinPanel _winPanel;
        private FailPanel _failPanel;
        private GameManager _gameManager;

        public event Action OnStartGame;
        public event Action OnRestartLevel;

        private void Awake()
        {
            _menuPanel = GetComponentInChildren<MenuPanel>(true);
            _winPanel = GetComponentInChildren<WinPanel>(true);
            _gamePanel = GetComponentInChildren<GamePanel>(true);
            _failPanel = GetComponentInChildren<FailPanel>(true);
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void Start()
        {
            SwitchPanel(PanelType.Menu);
            
            _menuPanel.Start += OnStart;
            _winPanel.PlayNextLevel += OnStart;
            _failPanel.Restart += OnRestart;
            
            _gameManager.Win += OnWin;
            _gameManager.Fail += OnFail;
        }
        
        private void OnDestroy()
        {
            _menuPanel.Start -= OnStart;
            _winPanel.PlayNextLevel -= OnStart;
            _failPanel.Restart += OnRestart;
            
            _gameManager.Win -= OnWin;
            _gameManager.Fail -= OnFail;
        
        }

        private void OnRestart()
        {
            SwitchPanel(PanelType.Game);
            OnRestartLevel?.Invoke();
        }

        private void OnFail()
        {
            SwitchPanel(PanelType.Fail);
        }

        private void OnWin()
        {
            SwitchPanel(PanelType.Win);
        }

        private void OnStart()
        {
            SwitchPanel(PanelType.Game);
            OnStartGame?.Invoke();
        }

        private void SwitchPanel(PanelType panel)
        {
            _gamePanel.gameObject.SetActive(panel == PanelType.Game);
            _menuPanel.gameObject.SetActive(panel == PanelType.Menu);
            _winPanel.gameObject.SetActive(panel == PanelType.Win);
            _failPanel.gameObject.SetActive(panel == PanelType.Fail);
        }
    }
}