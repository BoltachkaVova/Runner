using Runner.Configs;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runner
{
    public class Level : MonoBehaviour
    {
         [SerializeField] private LevelConfig config;
         
        private PlayerController _player;
        private ParticleSystem _particle;
        
        public PlayerController Player => _player; 
        public ParticleSystem ParticlePrefab => _particle;

        public void GenerateLevel()
        {
            Clear();
            GenerateRoadAndParticle();
            GenegatePlayer();
            GenegateWallsAndCois();
        }
        
        private void Clear()
        {
            for (int i = 0; i < transform.childCount; i++)
                Destroy(transform.GetChild(i).gameObject);
            
            _player = null;
        }

        public void RestartLevel()
        {
            Destroy(_player.gameObject);
            GenegatePlayer();
        }

        private void GenerateRoadAndParticle()
        {
            var roadLocalPosition = Vector3.zero;
            for (int i = 0; i < config.RoadPartCount; i++)
            {
                var road = Instantiate(config.RoadPartPrefab, transform);
                road.transform.localPosition = roadLocalPosition;
                roadLocalPosition.z += config.RoadPartLength;
            }
            Instantiate(config.FinihsPrefab, roadLocalPosition, Quaternion.identity, transform);

          
            var particlePosition = Vector3.zero;
            particlePosition.z = roadLocalPosition.z + config.DistanceBetweenParticleAndFinish;
            particlePosition.y = -20f;
            _particle = Instantiate(config.ParticlePrefab, particlePosition, Quaternion.Euler(-90,0,0), transform);
            _particle.Stop();
          
        }

        private void GenegatePlayer()
        {
            var player = Instantiate(config.PlayerPrefab, transform);
            player.transform.localPosition = new Vector3(0f, 0f, config.RoadPartLength * 0.5f);

            _player = player.GetComponent<PlayerController>();
        }


        private void GenegateWallsAndCois()
        {
            var fullLength = config.RoadPartCount * config.RoadPartLength;
            var currentLength = config.RoadPartLength * 2f;
            var wallOffsetX = config.RoadPartWidth * 0.33333f;
            var startPosX = -config.RoadPartWidth * 0.5f;

            while (currentLength < fullLength)
            {
                
                var zOffset = Random.Range(config.MINWallsOffset, config.MAXWallsOffset); 
                currentLength += zOffset;
                currentLength =
                    Mathf.Clamp(currentLength, 0,
                        fullLength);  

                
                var randomPositionX = Random.Range(0, 3); 
                var wallPositionX = startPosX + wallOffsetX * randomPositionX;
                
                
                var localPositionWall = Vector3.zero;
                localPositionWall.x = wallPositionX;
                localPositionWall.z = currentLength;

                
                var randon = Random.Range(0, 100);
                if (randon <= config.ChanceDropCoin)
                {
                    var coinPositionX = startPosX + wallOffsetX * (randomPositionX != 0 ? randomPositionX != 2  ? 0 : 1 : 2);
                    var localPositionCoin = Vector3.zero;
                    localPositionCoin.z = currentLength;// + 2f;
                    localPositionCoin.x = coinPositionX;
                    Instantiate(config.CoinPrefab, localPositionCoin, Quaternion.identity, transform);
                }
                
                Instantiate(config.WallPrefab, localPositionWall, Quaternion.identity, transform);
                
            }

        }

    }
}