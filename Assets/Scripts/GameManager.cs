using System;
using System.Collections;
using UnityEngine;

namespace Runner
{
    public class GameManager : MonoBehaviour
    {
        private const string SAVE_LEVEL = "level_index";
        private const string SAVE_COIN = "Coints";
        
        [SerializeField] private Level _levelPrefab;
        [SerializeField] private float _deleyAtFail;
        
        private UIController _uiController;
        private Level _level;
        
        private int _currentlevel;
        private int _coins;

        public event Action<int> OnAddCoin; 
        public event Action Win;
        public event Action Fail;
        public event Action<int> OnNextLevelIndex;

        private int LevelIndex
        {
            get => _currentlevel;
            set
            {
                _currentlevel = value;
                OnNextLevelIndex?.Invoke(LevelIndex);
            }
        }
        
        private void Awake()
        {
            _uiController = FindObjectOfType<UIController>();
        }

        private void Start()
        {
            LoadData();
            
            _uiController.OnStartGame += StartGame;
            _uiController.OnRestartLevel += RestartCurrentLevel;
        }

        private void OnDestroy()
        {
            SaveData(SAVE_COIN, _coins);
            SaveData(SAVE_LEVEL, LevelIndex);
            
            _uiController.OnStartGame -= StartGame;
            _uiController.OnRestartLevel -= RestartCurrentLevel;
        }
        
        private void StartGame()
        {
            
            if (_level != null)
            {
                Destroy(_level.gameObject);
                LevelIndex++;
            }
            else 
            {
                OnAddCoin?.Invoke(_coins);
                OnNextLevelIndex?.Invoke(_currentlevel);
            }
            
            _level = Instantiate(_levelPrefab, transform);
            StartLevel();
            
        }

        private void StartLevel()
        {
            _level.GenerateLevel();
            _level.Player.IsActive = true;

            _level.Player.OnWin += OnWin;
            _level.Player.OnDead += OnDead;
            _level.Player.OnCoin += AddCoin;
        }
        
        private void RestartCurrentLevel()
        {
            _level.RestartLevel();
            _level.Player.IsActive = true;

            _level.Player.OnWin += OnWin;
            _level.Player.OnDead += OnDead;
            _level.Player.OnCoin += AddCoin; 
        }

        private void AddCoin(int coin)
        {
            _coins++;
            OnAddCoin?.Invoke(_coins);
        }

        private void OnDead()
        {
            StartCoroutine(FailWithDelay());  
        }

        private void OnWin()
        {
            _level.ParticlePrefab.Play();
            StartCoroutine(WinWithDelay());
        }

        private IEnumerator WinWithDelay() 
        {
            yield return new WaitWhile(() => _level.ParticlePrefab.isPlaying);
            yield return Clear();
            Win?.Invoke(); 
        }


        private IEnumerator FailWithDelay()
        {
            yield return new WaitForSeconds(_deleyAtFail);
            yield return Clear(); 
            Fail?.Invoke();
        }

        private IEnumerator Clear()
        {
            _level.Player.OnWin -= OnWin;
            _level.Player.OnDead -= OnDead;
            _level.Player.OnCoin -= AddCoin;
            
            yield return null;
        }
        
        private void SaveData(string key, int index)  
        {
            PlayerPrefs.SetInt(key, index);
        }

        private void LoadData() 
        {
            _currentlevel = PlayerPrefs.GetInt(SAVE_LEVEL, 0);
            _coins = PlayerPrefs.GetInt(SAVE_COIN, 0);
        }
    }
}