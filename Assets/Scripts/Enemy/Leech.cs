using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WowieJam2.Enemy
{
    public class Leech : BaseEnemy
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Player")
            {
                base.StartDeath();
            }
        }

        protected override void Kill()
        {
            DisableColliders();
            var colls = Physics2D.OverlapCircleAll(transform.position, 2.5f);
            foreach (var col in colls)
            {
                if (col.tag == "Player")
                {
                    col.GetComponent<PlayerManager>().TakeDamage(Damage);
                }
            }
            base.Kill();
        }
    }
}