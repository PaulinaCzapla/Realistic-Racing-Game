using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class Fade : MonoBehaviour
    {
        public UnityAction OnFadedIn { get; set; }
        public UnityAction OnFadedOut { get; set; }
        

        [Header("Fade params")]
        [SerializeField] private float maxAlpha;
        [SerializeField] private float minAlpha;
        [SerializeField] private float fadeSpeed;
        
        [Header("Graphic component to fade")]
        [SerializeField] private Graphic graphic;

        public void FadeIn()
        {
            StopAllCoroutines();
            StartCoroutine(FadeInCoroutine());
        }

        public void FadeOut()
        {
            StopAllCoroutines();
            StartCoroutine(FadeOutCoroutine());
        }

        private IEnumerator FadeInCoroutine()
        {
            while (graphic.color.a < maxAlpha)
            {
                graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, graphic.color.a + 0.05f);
                yield return new WaitForSecondsRealtime(fadeSpeed);
            }

            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b,
                maxAlpha);
            
            OnFadedIn?.Invoke();
        }

        private IEnumerator FadeOutCoroutine()
        {
            if (!Mathf.Approximately(graphic.color.a, minAlpha))
                while (graphic.color.a > minAlpha)
                {
                    graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b,
                        graphic.color.a - 0.05f);
                    yield return new WaitForSecondsRealtime(fadeSpeed);
                }

            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b,
                minAlpha);
            
            OnFadedOut?.Invoke();
        }
    }
}