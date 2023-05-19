using UnityEngine;

namespace Runner.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private float forwardSpeed = 5f;
        [SerializeField] private float roadWidth = 5f;
        [SerializeField] private float sideSpeed = 5f;
        [SerializeField] private float turnRotationAngle = 40f;
        [SerializeField] private float lerpSpeed = 4f;

        public float ForwardSpeed => forwardSpeed;
        public float RoadWidth => roadWidth;
        public float SideSpeed => sideSpeed;
        public float TurnRotationAngle => turnRotationAngle;
        public float LerpSpeed => lerpSpeed;
    }
}