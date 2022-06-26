using DG.Tweening;
using UnityEngine;

namespace VisualNovel.UI
{
    /// <summary>
    /// A class that represents moving arrow in dialogue panel
    /// </summary>
    public class SkipArrow : MonoBehaviour
    {
        private RectTransform _rect;
        private Sequence _sequence;
        private const float _interval = 0.04f;
        private float _initialPosX;
        private int i;
        private bool _moveRight = true;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            _initialPosX = _rect.anchoredPosition.x;
        }

        private void OnEnable()
        {
            i = 0;
            _rect.anchoredPosition = new Vector2(_initialPosX, _rect.anchoredPosition.y);
            
            _sequence = DOTween.Sequence()
                .AppendCallback(Move)
                .AppendInterval(_interval).SetLoops(-1);
        }

        private void OnDisable()
        {
            _sequence.Kill();
        }

        private void Move()
        {
            if (_moveRight)
            {
                _rect.anchoredPosition = new Vector2( _rect.anchoredPosition.x + 1, _rect.anchoredPosition.y);
                i++;
                if (i == 8)
                {
                    _moveRight = false;
                    i = 0;
                }
            }
            else
            {
                _rect.anchoredPosition = new Vector2(_rect.anchoredPosition.x - 1, _rect.anchoredPosition.y);
                i++;
                if (i == 8)
                {
                    _moveRight = true;
                    i = 0;
                }
            }
        }
    }
}