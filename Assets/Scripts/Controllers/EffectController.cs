using System;
using System.Collections;
using UnityEngine;
using Components;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class EffectController : MonoBehaviour
    {
        public static EffectController Instance;

        [SerializeField] private GameField2D gameField2D;
        [SerializeField] private GameObject effectPrefab;
        [SerializeField] private Sprite[] splatterSprites;
        [Space]
        [SerializeField] private float deltaAlphaFade;
        [SerializeField] private float deltaTimeFade;

        private void Awake()
        {
            Instance = this;
        }

        private IEnumerator DestroyDelayByFade(SpriteRenderer spriteRenderer)
        {
            yield return StartCoroutine(FadeSprite(spriteRenderer));
            
            Destroy(spriteRenderer.gameObject);
        }
        
        private IEnumerator FadeSprite(SpriteRenderer spriteRenderer)
        {
            while (spriteRenderer.color.a > 0)
            {
                spriteRenderer.color -= new Color(0, 0, 0,deltaAlphaFade);
                yield return new WaitForSeconds(deltaTimeFade);
            }
        }
        
        public void SplatterEffect2D(Vector3 position, Color color)
        {
            var splatter = Instantiate(
                effectPrefab, 
                position, 
                Quaternion.Euler(new Vector3(0, 0, Random.Range(0f, 360f))), 
                gameField2D.transform);
            
            var spriteRenderer = splatter.GetComponent<SpriteRenderer>();
            
            spriteRenderer.sprite = splatterSprites[Random.Range(0, splatterSprites.Length)];
            spriteRenderer.color = color;

            StartCoroutine(DestroyDelayByFade(spriteRenderer));
        }
    }
}