using System;
using System.Collections.Generic;
using Components;
using Controllers.Game;
using ScriptableObjects;
using UnityEngine;

namespace Controllers
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private HealthSettings settings;
        [Space]
        [SerializeField] private GameController gameController;
        [SerializeField] private SliceController sliceController;
        [SerializeField] private ExplosionController explosionController;
        [SerializeField] private GameField2D gameField2D;
        [SerializeField] private List<string> tagsUnitForHealing;
        [SerializeField] private List<string> tagsUnitForDamage;
        [SerializeField] private List<string> tagsUnitForDamageOutsideGameField;

        public event Action<int> OnLoadHealth;
        public event Action<int> OnHealing;
        public event Action<int> OnDamage;
        public event Action ZeroHealth;

        private int _healths;

        private void OnEnable()
        {
            gameController.OnStart += LoadHealth;
            sliceController.OnSlice += UnitHealing;
            explosionController.OnExplosion += UnitDamage;
            gameField2D.OnExitUnit += DamageUnitOutsideGameField;
        }

        private void OnDisable()
        {
            gameController.OnStart -= LoadHealth;
            sliceController.OnSlice -= UnitHealing;
            explosionController.OnExplosion -= UnitDamage;
            gameField2D.OnExitUnit -= DamageUnitOutsideGameField;
        }

        private void LoadHealth()
        {
            _healths = settings.MaxHeart;
            OnLoadHealth?.Invoke(settings.MaxHeart);
        }

        private void Healing()
        {
            if (_healths >= settings.MaxHeart)
            {
                return;
            }

            _healths += settings.Healing;
             OnHealing?.Invoke(settings.Healing);           
        }
        private void Damage()
        {
            if (_healths == 0)
            {
                return;
            }

            _healths -= settings.Damage;
            OnDamage?.Invoke(settings.Damage);

            if (_healths == 0) 
            {
                ZeroHealth?.Invoke();
            }
        }

        private void UnitHealing(GameObject unit)
        {
            if (!tagsUnitForHealing.Contains(unit.tag.ToString()))
            {
                return;
            }
            
            Healing();
        }

        private void UnitDamage(GameObject unit)
        {
            if (!tagsUnitForDamage.Contains(unit.tag.ToString()))
            {
                return;
            }
            
            Damage();
        }
        
        private void DamageUnitOutsideGameField(GameObject unit)
        {
            if (!tagsUnitForDamageOutsideGameField.Contains(unit.tag.ToString()))
            {
                return;
            }
            
            Damage();
        }
    }
}