using System.Collections;
using UnityEngine;
using Components;

namespace Controllers
{
    public class SplatterController : MonoBehaviour
    {
        public static SplatterController Instance;

        [SerializeField] private GameField2D gameField2D;
        [SerializeField] private GameObject splatterPrefab;
        [SerializeField] private Sprite[] splatterSprites;

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
        
        public void InstanceSplatter(Vector3 position, Color color)
        {
            var splatter = Instantiate(
                splatterPrefab, 
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