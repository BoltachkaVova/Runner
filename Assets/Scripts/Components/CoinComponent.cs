using DG.Tweening;
using UnityEngine;

namespace Runner
{
    public class CoinComponent : MonoBehaviour
    {
        private Sequence _sequence;

        private void Start()
        {
            _sequence = DOTween.Sequence();

            _sequence.Append(transform.DORotate(new Vector3(90, 360, 0), 3f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear));
            _sequence.SetLoops(-1);
        }

        private void OnDisable()
        {
            _sequence?.Kill(true);
        }
    }
}