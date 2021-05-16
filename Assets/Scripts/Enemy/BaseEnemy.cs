using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WowieJam2.Enemy
{
    public enum EnemyType { Insect, Arachnid, Myriapod, Clitellata }
    public abstract class BaseEnemy : MonoBehaviour
    {
        public EnemyType BugType;
        public string BugName;
        public float MoveSpeed;
        public float Damage;
        public float Health;
        public int Score;
        protected float m_MaxHealth;

        private Animator m_Animator;
        protected AIPath m_AIPath;
        public AudioClip KillSound;

        protected virtual void Start()
        {
            m_Animator = GetComponent<Animator>();
            m_MaxHealth = Health;
            GetComponent<AIDestinationSetter>().target = GameObject.FindWithTag("Player").transform;
            m_AIPath = GetComponent<AIPath>();
            m_AIPath.maxSpeed = MoveSpeed;
        }

        /// <summary>
        /// Start death animation
        /// </summary>
        protected virtual void StartDeath()
        {
            DisableColliders();
            m_AIPath.canMove = false;
            m_Animator.SetBool("IsDead", true);
            GetComponent<AudioSource>().Play();
        }

        /// <summary>
        /// Called when death animation is finished
        /// </summary>
        protected virtual void Kill()
        {
            GameManager.Instance.LowerEnemyTotal(1);
            GameManager.Instance.SetScoreAdd(Score);
            UIManager.Instance.SetScore(GameManager.Instance.GetScore());

            // BLOOD
            int amount = Random.Range(5, 25);
            for(int i = 0; i < amount; i++)
            {
                BloodParticleSystemHandler.Instance.SpawnBlood(transform.position, UtilsClass.GetVectorFromAngle(Random.Range(0f, 360f)));
            }

            GameManager.Instance.GetComponent<AudioSource>().PlayOneShot(KillSound);
            Destroy(gameObject);
        }

        public void TakeDamage(float dmg, bool isExplosion = false)
        {
            Health -= dmg;
            DamageIndicator.Create(new Vector3(transform.position.x, transform.position.y + .75f), dmg);
            Debug.Log(BugName + " took " + dmg + " damage.");
            if (Health <= 0)
            {
                if (isExplosion)
                    Kill();
                else
                    StartDeath();
            }
        }

        protected void DisableColliders()
        {
            var colliders = GetComponents<Collider2D>();
            foreach (var col in colliders)
            {
                col.enabled = false;
            }
        }
    }
}