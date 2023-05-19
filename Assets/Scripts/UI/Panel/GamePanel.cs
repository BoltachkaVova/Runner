using TMPro;
using UnityEngine;

namespace Runner
{
    public class GamePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textCoint;
        [SerializeField] private TextMeshProUGUI _textcurrentLevel;

        private GameManager _gameManager;
        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }
        private void OnEnable()
        {
            _gameManager.OnAddCoin += OnAddCoin;
            _gameManager.OnNextLevelIndex += OnLevelIndex;
        }

        private void OnDisable()
        {
            _gameManager.OnAddCoin -= OnAddCoin;
            _gameManager.OnNextLevelIndex -= OnLevelIndex;
        }
        
        private void OnLevelIndex(int index)
        {
            _textcurrentLevel.text = $"Current Level: {index}";
        }
        
        private void OnAddCoin(int coint)
        {
            _textCoint.text = coint.ToString();
        }
        
    }
}