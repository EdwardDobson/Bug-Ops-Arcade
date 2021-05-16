using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace WowieJam2.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        public Animator Animator;

        private float m_OriginalMoveSpeed;
        [SerializeField] private float m_MoveSpeed = 10.0f;
        private Rigidbody2D m_Rb;

        private void Start()
        {
            m_OriginalMoveSpeed = m_MoveSpeed;
            m_Rb = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            if(SceneManager.GetActiveScene().buildIndex != 0)
            Rotation();
        }

        private void FixedUpdate()
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
                Movement();
        }

        /// <summary>
        /// Handle the movement for the player
        /// </summary>
        private void Movement()
        {
            Vector3 movement = new Vector3
            {
                x = Input.GetAxisRaw("Horizontal"),
                y = Input.GetAxisRaw("Vertical"),
                z = 0.0f
            }.normalized;

            //transform.position += movement * m_MoveSpeed * Time.deltaTime;

            m_Rb.velocity = new Vector2(movement.x, movement.y) * m_MoveSpeed;

            if (m_Rb.velocity.magnitude > 0)
            {
                DirtParticleSystemHandler.Instance.SpawnDirt(transform.position, new Vector3(-movement.x, -movement.y));
                FootprintParticleSystemHandler.Instance.SpawnFootprint(transform.position, movement);
                Achievements.Progress(Achievements.AchievementType.Walk, Time.deltaTime);
            }
        }

        /// <summary>
        /// Handle the rotation for the player
        /// </summary>
        private void Rotation()
        {
            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        public void SlowDown(float duration)
        {
            m_MoveSpeed = 3f;
            StopAllCoroutines();
            StartCoroutine(SlowDownRoutine(duration));
        }

        public float GetSpeed()
        {
            return m_MoveSpeed;
        }

        public void SetSpeed(float speed)
        {
            m_MoveSpeed = speed;
        }

        IEnumerator SlowDownRoutine(float duration)
        {
            yield return new WaitForSeconds(duration);
            m_MoveSpeed = m_OriginalMoveSpeed;
        }
    }

}