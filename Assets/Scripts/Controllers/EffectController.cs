using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class EffectController : MonoBehaviour
    {
        [Serializable]
        private struct SplatterByObject
        {
            public string tagObject;
            public Color colorSplatter;
        }

        [SerializeField] private SliceController sliceController;
        [SerializeField] private ExplosionController explosionController;
        [Space]
        [SerializeField] private GameField2D gameField2D;
        [SerializeField] private GameObject uiEffectContainer;
        [Space]
        [SerializeField] private Sprite[] splatterSprites;
        [SerializeField] private List<SplatterByObject> splattersByObjects;
        [SerializeField] private float deltaAlphaFade;
        [SerializeField] private float deltaTimeFade;
        [Space]
        [SerializeField] private GameObject explosionEffect;
        [SerializeField] private float lifeTimeExplosionEffect;

        private static int _splatterOrderLayer = 0;

        private void OnEnable()
        {
            sliceController.OnSlice += Splatter;
            explosionController.OnExplosion += Explosion;
        }

        private void OnDisable()
        {
            sliceController.OnSlice -= Splatter;
            explosionController.OnExplosion -= Explosion;
        }

        private IEnumerator DestroyDelayByFadeSplatter(SpriteRenderer spriteRenderer)
        {
            yield return StartCoroutine(FadeSplatter(spriteRenderer));
            
            Destroy(spriteRenderer.gameObject);
            
            _splatterOrderLayer--;
        }
        
        private IEnumerator FadeSplatter(SpriteRenderer spriteRenderer)
        {
            while (spriteRenderer.color.a > 0)
            {
                spriteRenderer.color -= new Color(0, 0, 0,deltaAlphaFade);
                yield return new WaitForSeconds(deltaTimeFade);
            }
        }
        
        private void Splatter(GameObject unit)
        {
            var splatter = new GameObject("SplatterEffect");
            var spriteRenderer = splatter.AddComponent<SpriteRenderer>();
            
            splatter.transform.position = unit.transform.position;
            splatter.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0f, 360f)));
            splatter.transform.parent = gameField2D.transform;
            
            spriteRenderer.sortingLayerName = "Splatters";
            spriteRenderer.sortingOrder = _splatterOrderLayer++;
            
            spriteRenderer.sprite = splatterSprites[Random.Range(0, splatterSprites.Length)];
            spriteRenderer.color = splattersByObjects.Find(obj => obj.tagObject == unit.tag.ToString()).colorSplatter;

            StartCoroutine(DestroyDelayByFadeSplatter(spriteRenderer));
        }

        private void Explosion(GameObject unit)
        {
            var effect = Instantiate(explosionEffect, unit.transform.position, Quaternion.identity, gameField2D.transform);
            Destroy(effect, lifeTimeExplosionEffect);
        }
    }
}