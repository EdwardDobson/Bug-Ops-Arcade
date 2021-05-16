using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WowieJam2.Enemy
{
    public class SlimeShot : MonoBehaviour
    {
        public Vector3 Target;
        public GameObject SlimePuddlePrefab;

        void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position, Target, 4f * Time.deltaTime);
            if (transform.position == Target)
            {
                var rot = Random.Range(1, 360);
         //       Debug.Log(rot);
                var slimePuddle = Instantiate(SlimePuddlePrefab, transform.position, Quaternion.identity);
                slimePuddle.transform.localEulerAngles = new Vector3(0, 0, rot);
                Destroy(gameObject);
            }
        }
    }
}