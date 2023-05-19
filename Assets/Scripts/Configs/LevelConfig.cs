using UnityEngine;

namespace Runner.Configs
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/Level/LevelConfig", order = 0)]
    public class LevelConfig : ScriptableObject
    {
        [Header("Prefabs")] 
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject finihsPrefab;
        [SerializeField] private GameObject wallPrefab;
        [SerializeField] private GameObject roadPartPrefab;
        [SerializeField] private GameObject coinPrefab;
        [SerializeField] private ParticleSystem particlePrefab;
        
        [Header("Settings Road")] 
        [SerializeField] private int roadPartCount = 10;
        [SerializeField] private float roadPartLength = 6f;
        [SerializeField] private float roadPartWidth = 6f;

        [Header("Settings Walls")] 
        [SerializeField]private float minWallsOffset = 3f;
        [SerializeField] private float maxWallsOffset = 5f;
        
        [Header("Settings Coin")]
        [SerializeField] private int chanceDropCoin = 70; 
        

        [Header(("Particle System"))] 
        [SerializeField] private float distanceBetweenParticleAndFinish = 5f;

        
        public GameObject PlayerPrefab => playerPrefab;
        public GameObject FinihsPrefab => finihsPrefab;
        public GameObject WallPrefab => wallPrefab;
        public GameObject RoadPartPrefab => roadPartPrefab;
        public GameObject CoinPrefab => coinPrefab;
        public ParticleSystem ParticlePrefab => particlePrefab;
        
        
        public int RoadPartCount => roadPartCount;
        public float RoadPartLength => roadPartLength;
        public float RoadPartWidth => roadPartWidth;
        
        public float MINWallsOffset => minWallsOffset;
        public float MAXWallsOffset => maxWallsOffset;
        
        public int ChanceDropCoin => chanceDropCoin;
        
        public float DistanceBetweenParticleAndFinish => distanceBetweenParticleAndFinish;
    }
}