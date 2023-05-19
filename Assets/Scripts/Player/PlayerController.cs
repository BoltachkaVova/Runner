using System;
using Runner.Configs;
using UnityEngine;

namespace Runner
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private Transform model; 
        
        private Rigidbody _rigidbody;
        private Animator _animator;
        private InputHandler _inputHandler;
        private bool _isActive; 
        private int _countCoin;
        
        private static readonly int Run = Animator.StringToHash("Run");
        private static readonly int Fall = Animator.StringToHash("Fall");
        private static readonly int Dance = Animator.StringToHash("Dance");
        
        public event Action OnWin;
        public event Action OnDead;
        public event Action<int> OnCoin;
        
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                if (_isActive)
                {
                    _animator.SetTrigger(Run);
                }
            }
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _inputHandler = GetComponent<InputHandler>();
            _animator = GetComponentInChildren<Animator>();
        }
        
        private void Start()
        {
            IsActive = true;
        }

        private void FixedUpdate()
        {
            if(!IsActive)
                return;
            
            Move(); 
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<WallComponent>())
                Died();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<FinishComponent>())
                Finish();

            if (other.gameObject.TryGetComponent(out CoinComponent coinComponent))
            {
                AddCoin();
                coinComponent.gameObject.SetActive(false);
            }
        }
        
        private void Move()
        {
            var xOffset = - _inputHandler.HorizontalAxis * playerConfig.SideSpeed; 
           
            var rotation = model.localRotation.eulerAngles; 
            rotation.y = Mathf.LerpAngle(rotation.y, _inputHandler.IsHold  ?  Mathf.Sign(xOffset) * playerConfig.TurnRotationAngle : 0, 
                playerConfig.LerpSpeed * Time.deltaTime); 
            
            model.localRotation = Quaternion.Euler(rotation);  
             
            var position = _rigidbody.position; 
            position.x += xOffset; 
            position.x = Mathf.Clamp(position.x, -playerConfig.RoadWidth * 0.5f, playerConfig.RoadWidth * 0.5f); 
            _rigidbody.MovePosition(position + transform.forward * (playerConfig.ForwardSpeed * Time.deltaTime)); 
        }
        
        private void AddCoin()
        {
            _countCoin++;
            OnCoin?.Invoke(_countCoin);
        }
        
        private void Died()
        {
            IsActive = false;
            _animator.SetTrigger(Fall);
            OnDead?.Invoke();
        }
        
        private void Finish()
        {
            IsActive = false;
            _animator.SetTrigger(Dance);
            OnWin?.Invoke();
        }
    }
}