using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

namespace System.Health
{

    public class HealthCompoment : MonoBehaviour, IDamagable
    {
        public float Health => CurrentHealth;
        public float MaxHealth => TotalHealth;

        private float CurrentHealth;
        [SerializeField]
        private float TotalHealth;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            CurrentHealth = TotalHealth;  
        }

        // Update is called once per frame
        void Update()
        {

        }

        public virtual void Destroy()
        {
            Destroy(gameObject);
        }

        public virtual void TakeDamage(float damage)
        {
            CurrentHealth -= damage;

            if (CurrentHealth <= 0)
            {
                Destroy();
            }
        }

        public void HealPlayer(int effect)
        {
            if (CurrentHealth < MaxHealth)
            {
                CurrentHealth = Mathf.Clamp(CurrentHealth + effect, 0, MaxHealth);
            }
        }
    }
}
