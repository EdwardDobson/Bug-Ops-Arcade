using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootprintParticleSystemHandler : MonoBehaviour
{
    public static FootprintParticleSystemHandler Instance { get; private set; }

    private MeshParticleSystem meshParticleSystem;
    private List<Single> singleList;
    private float spawnDelay;
    private void Awake()
    {
        Instance = this;
        singleList = new List<Single>();
        meshParticleSystem = GetComponent<MeshParticleSystem>();
        spawnDelay = .15f;
    }

    private void Update()
    {
        //for (int i = 0; i < singleList.Count; i++)
        //{
        //    Single single = singleList[i];
        //    single.Update();
        //    if (single.IsMovementComplete())
        //    {
        //        singleList.RemoveAt(i);
        //        i++;
        //    }
        //}

        if (spawnDelay > 0) spawnDelay -= Time.deltaTime;
    }

    public void SpawnFootprint(Vector3 position, Vector3 direction)
    {
        if (spawnDelay <= 0)
        {
            singleList.Add(new Single(position, direction, meshParticleSystem));
            spawnDelay = .15f;
        }
    }

    private class Single
    {
        private MeshParticleSystem meshParticleSystem;
        private Vector3 position;
        private Vector3 direction;
        private int quadIndex;
        private Vector3 quadSize;
        private float rotation;
        private float moveSpeed;
        public Single(Vector3 position, Vector3 direction, MeshParticleSystem meshParticleSystem)
        {
            this.position = position;
            this.direction = direction;
            this.meshParticleSystem = meshParticleSystem;

            quadSize = new Vector3(.4f, .4f);
            rotation = Random.Range(0, 360f);
            moveSpeed = Random.Range(5f, 20f);

            quadIndex = meshParticleSystem.AddQuad(position, rotation, quadSize, true, 0);
        }

        public void Update()
        {
            position += direction * moveSpeed * Time.deltaTime;
            rotation += 360f * (moveSpeed / 10f) * Time.deltaTime;

            meshParticleSystem.UpdateQuad(quadIndex, position, rotation, quadSize, true, 0);

            float slowDownFactor = 3.5f;
            moveSpeed -= moveSpeed * slowDownFactor * Time.deltaTime;
        }

        public bool IsMovementComplete()
        {
            return moveSpeed < .15f;
        }
    }
}
