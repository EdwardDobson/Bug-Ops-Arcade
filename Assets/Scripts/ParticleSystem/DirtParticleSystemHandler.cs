using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtParticleSystemHandler : MonoBehaviour
{
    public static DirtParticleSystemHandler Instance { get; private set; }

    private MeshParticleSystem meshParticleSystem;
    private List<Single> singleList;
    private float spawnDelay;
    private void Awake()
    {
        Instance = this;
        singleList = new List<Single>();
        meshParticleSystem = GetComponent<MeshParticleSystem>();
        spawnDelay = .1f;
    }

    private void Update()
    {
        for (int i = 0; i < singleList.Count; i++)
        {
            Single single = singleList[i];
            single.Update();
            if (single.IsParticleComplete())
            {
                single.DestroySelf();
                singleList.RemoveAt(i);
                i++;
            }
        }

        if (spawnDelay > 0) spawnDelay -= Time.deltaTime;
    }

    public void SpawnDirt(Vector3 position, Vector3 direction)
    {
        if(spawnDelay <= 0)
        {
            singleList.Add(new Single(position, direction, meshParticleSystem));
            spawnDelay = .1f;
        }
    }

    private class Single
    {
        private MeshParticleSystem meshParticleSystem;
        private Vector3 position;
        private Vector3 direction;
        private int quadIndex;
        private Vector3 quadSize;
        private float moveSpeed;
        private int uvIndex;
        private float uvIndexTimer;
        private float uvIndexTimerMax;
        public Single(Vector3 position, Vector3 direction, MeshParticleSystem meshParticleSystem)
        {
            this.position = position;
            this.direction = direction;
            this.meshParticleSystem = meshParticleSystem;

            quadSize = new Vector3(.75f, .75f);
            moveSpeed = 3f;
            uvIndex = 0;
            uvIndexTimerMax = 1f / 10;

            quadIndex = meshParticleSystem.AddQuad(position, 0f, quadSize, true, 0);
        }

        public void Update()
        {
            uvIndexTimer += Time.deltaTime;
            if(uvIndexTimer >= uvIndexTimerMax)
            {
                uvIndexTimer -= uvIndexTimerMax;
                uvIndex++;
            }
            position += direction * moveSpeed * Time.deltaTime;

            meshParticleSystem.UpdateQuad(quadIndex, position, 0f, quadSize, true, uvIndex);

            float slowDownFactor = 3.5f;
            moveSpeed -= moveSpeed * slowDownFactor * Time.deltaTime;
        }

        public bool IsParticleComplete()
        {
            return uvIndex >= 8 || moveSpeed < .15f;
        }

        public void DestroySelf()
        {
            meshParticleSystem.DestroyQuad(quadIndex);
        }
    }
}
