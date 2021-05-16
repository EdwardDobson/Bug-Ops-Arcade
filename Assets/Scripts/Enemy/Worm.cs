using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WowieJam2.Enemy
{
    public class Worm : BaseEnemy
    {
        public string PrefabName;
        public int Splits;

        protected override void Kill()
        {
            if (Splits > 0)
            {
                CreateWorm(transform.position + (transform.right * 1), transform.rotation);
                CreateWorm(transform.position + (-transform.right * 1), transform.rotation);
            }
            base.Kill();
        }

        private void CreateWorm(Vector3 pos, Quaternion rot)
        {
            var worm = Instantiate((GameObject)Resources.Load(PrefabName));
            worm.transform.position = pos;
            worm.transform.rotation = rot;
            worm.GetComponentInChildren<Worm>().Splits = Splits - 1;
            worm.GetComponent<BaseEnemy>().MoveSpeed = MoveSpeed + 3;
            GameManager.Instance.IncreaseEnemyTotal(1);
           
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Player")
            {
                DisableColliders();
                collision.GetComponent<PlayerManager>().TakeDamage(Damage);
                if (Splits > 0) StartDeath(); else Kill();
            }
        }
    }
}
