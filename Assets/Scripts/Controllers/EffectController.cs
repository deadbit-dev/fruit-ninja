using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class EffectController : MonoBehaviour
    {
        [SerializeField] private GameField2D gameField2D;
        [Space]
        [SerializeField] private Sprite[] splatterSprites;
        [SerializeField] private float deltaAlphaFade;
        [SerializeField] private float deltaTimeFade;
        [Space]
        [SerializeField] private GameObject explosionEffect;
        [SerializeField] private float lifeTimeExplosionEffect;

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
            var splatter = new GameObject("SplatterEffect");
            var spriteRenderer = splatter.AddComponent<SpriteRenderer>();
            splatter.transform.position = position;
            splatter.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0f, 360f)));
            splatter.transform.parent = gameField2D.transform;
            spriteRenderer.sprite = splatterSprites[Random.Range(0, splatterSprites.Length)];
            spriteRenderer.color = color;

            StartCoroutine(DestroyDelayByFade(spriteRenderer));
        }

        public void ExplosionEffect2D(Vector3 position)
        {
            var effect = Instantiate(explosionEffect, position, Quaternion.identity, gameField2D.transform);
            Destroy(effect, lifeTimeExplosionEffect);
        }
    }
}