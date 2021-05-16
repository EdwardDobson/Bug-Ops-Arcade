﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WowieJam2.Utils;

public class MeshParticleSystem : MonoBehaviour
{
    private const int MAX_QUAD_AMOUNT = 15000;

    [System.Serializable]
    public struct ParticleUVPixels
    {
        public Vector2Int uv00Pixels;
        public Vector2Int uv11Pixels;
    }

    private struct UVCoords
    {
        public Vector2 uv00;
        public Vector2 uv11;
    }

    [SerializeField] private ParticleUVPixels[] particleUVPixelsArray;
    private UVCoords[] uvCoordsArray;

    private Mesh mesh;
    private Vector3[] vertices;
    private Vector2[] uv;
    private int[] triangles;
    private int quadIndex;

    private bool updateVertices;
    private bool updateUV;
    private bool updateTriangles;

    private void Awake()
    {
        mesh = new Mesh();

        vertices = new Vector3[4 * MAX_QUAD_AMOUNT];
        uv = new Vector2[4 * MAX_QUAD_AMOUNT];
        triangles = new int[6 * MAX_QUAD_AMOUNT];

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 10000f);

        GetComponent<MeshFilter>().mesh = mesh;

        // Set up normalised array
        Material material = GetComponent<MeshRenderer>().material;
        Texture mainTexture = material.mainTexture;
        int textureWidth = mainTexture.width;
        int textureHeight = mainTexture.height;

        List<UVCoords> uvCoordsList = new List<UVCoords>();
        foreach(ParticleUVPixels particleUVPixels in particleUVPixelsArray)
        {
            UVCoords uvCoords = new UVCoords
            {
                uv00 = new Vector2((float)particleUVPixels.uv00Pixels.x / textureWidth, (float)particleUVPixels.uv00Pixels.y / textureHeight),
                uv11 = new Vector2((float)particleUVPixels.uv11Pixels.x / textureWidth, (float)particleUVPixels.uv11Pixels.y / textureHeight),
            };
            uvCoordsList.Add(uvCoords);
        }
        uvCoordsArray = uvCoordsList.ToArray();
    }

    public int AddQuad(Vector3 position, float rotation, Vector3 quadSize, bool skewed, int uvIndex)
    {
        if (quadIndex >= MAX_QUAD_AMOUNT) return 0;

        UpdateQuad(quadIndex, position, rotation, quadSize, skewed, uvIndex);

        int spawnedQuadIndex = quadIndex;

        StartCoroutine(DestroyAfterDelay(5, spawnedQuadIndex));

        quadIndex++;
        return spawnedQuadIndex;
    }

    private IEnumerator DestroyAfterDelay(float delay, int quadPos)
    {
        yield return new WaitForSeconds(delay);
        DestroyQuad(quadPos);
    }

    public void UpdateQuad(int quadIndex, Vector3 position, float rotation, Vector3 quadSize, bool skewed, int uvIndex)
    {
        // Relocate vertices
        int vIndex = quadIndex * 4;
        int vIndex0 = vIndex;
        int vIndex1 = vIndex + 1;
        int vIndex2 = vIndex + 2;
        int vIndex3 = vIndex + 3;

        if (skewed)
        {
            vertices[vIndex0] = position + Quaternion.Euler(0, 0, rotation) * new Vector3(-quadSize.x, -quadSize.y);
            vertices[vIndex1] = position + Quaternion.Euler(0, 0, rotation) * new Vector3(-quadSize.x, +quadSize.y);
            vertices[vIndex2] = position + Quaternion.Euler(0, 0, rotation) * new Vector3(+quadSize.x, +quadSize.y);
            vertices[vIndex3] = position + Quaternion.Euler(0, 0, rotation) * new Vector3(+quadSize.x, -quadSize.y);
        }
        else
        {
            vertices[vIndex0] = position + Quaternion.Euler(0, 0, rotation - 180) * quadSize;
            vertices[vIndex1] = position + Quaternion.Euler(0, 0, rotation - 270) * quadSize;
            vertices[vIndex2] = position + Quaternion.Euler(0, 0, rotation - 0) * quadSize;
            vertices[vIndex3] = position + Quaternion.Euler(0, 0, rotation - 90) * quadSize;
        }

        // UV
        UVCoords uvCoords = uvCoordsArray[uvIndex];
        uv[vIndex0] = uvCoords.uv00;
        uv[vIndex1] = new Vector2(uvCoords.uv00.x, uvCoords.uv11.y);
        uv[vIndex2] = uvCoords.uv11;
        uv[vIndex3] = new Vector2(uvCoords.uv11.x, uvCoords.uv00.y);

        // Create triangles
        int tIndex = quadIndex * 6;

        triangles[tIndex + 0] = vIndex0;
        triangles[tIndex + 1] = vIndex1;
        triangles[tIndex + 2] = vIndex2;

        triangles[tIndex + 3] = vIndex0;
        triangles[tIndex + 4] = vIndex2;
        triangles[tIndex + 5] = vIndex3;

        updateVertices = true;
        updateUV = true;
        updateTriangles = true;
    }

    public void SpawnBlood(Vector3 position)
    {
        Vector3 quadPosition = position;
        Vector3 quadSize = new Vector3(1f, 1f);
        float rotation = 0f;

        int uvIndex = Random.Range(0, 8);
        int spawnedQuadIndex = AddQuad(quadPosition, rotation, quadSize, true, uvIndex);

        FunctionUpdater.Create(() =>
        {
            quadPosition += new Vector3(1, 1) * 3f * Time.deltaTime;
            //rotation += 360f * Time.deltaTime;
            UpdateQuad(spawnedQuadIndex, quadPosition, rotation, quadSize, true, uvIndex);
        });
    }

    public void DestroyQuad(int quadIndex)
    {
        int vIndex = quadIndex * 4;
        int vIndex0 = vIndex;
        int vIndex1 = vIndex + 1;
        int vIndex2 = vIndex + 2;
        int vIndex3 = vIndex + 3;

        vertices[vIndex0] = Vector3.zero;
        vertices[vIndex1] = Vector3.zero;
        vertices[vIndex2] = Vector3.zero;
        vertices[vIndex3] = Vector3.zero;

        updateVertices = true;
    }

    private void LateUpdate()
    {
        if (updateVertices)
        {
            mesh.vertices = vertices;
            updateVertices = false;
        }
        if (updateUV)
        {
            mesh.uv = uv;
            updateUV = false;
        }
        if (updateTriangles)
        {
            mesh.triangles = triangles;
            updateTriangles = false;
        }
    }
}
