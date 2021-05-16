using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace WowieJam2.Enemy
{
    public class Slime : BaseEnemy
    {
        public GameObject SlimeBallPrefab;

        public float Range = 15f;
        private Transform m_PlayerTransform;
        private float m_ShotDelay = 0f;

        protected override void Start()
        {
            m_PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            base.Start();
        }

        void Update()
        {
            var hit2 = Physics2D.Linecast(transform.position, m_PlayerTransform.position, ~(1 << 9));
            if(hit2.collider != null && hit2.collider.tag == "Player" && hit2.distance <= Range && m_ShotDelay <= 0)
            {
                var slimeBall = Instantiate(SlimeBallPrefab, transform.position, transform.rotation);
                slimeBall.GetComponent<SlimeShot>().Target = hit2.collider.transform.position;
                m_ShotDelay = 2f;
                m_AIPath.canMove = false;
            }

            if (m_ShotDelay > 0) m_ShotDelay -= Time.deltaTime;
            else
            {
                m_AIPath.canMove = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Player")
            {
                base.StartDeath();
            }
        }
    }
}