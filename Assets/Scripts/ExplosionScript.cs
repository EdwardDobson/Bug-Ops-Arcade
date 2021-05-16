using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public static void Create(Vector3 position, float scaleSize = 4f)
    {
        var explosion = Instantiate((GameObject)Resources.Load("Explosion"), position, new Quaternion(0,0, Random.Range(0,360f), 0));
        explosion.transform.localScale = new Vector3(scaleSize, scaleSize);
    }
}

public class ExplosionScript : MonoBehaviour
{
    void Start()
    {
        CameraShake.Instance.ExplosionShake();
        Destroy(gameObject, 5);
    }
}
