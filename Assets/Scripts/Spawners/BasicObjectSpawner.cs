using System.Linq;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class BasicObjectSpawner : MonoBehaviour
    {
        [SerializeField]
        BasicPrefabsAsset _basicObjectPrefabsAsset;
        [SerializeField]
        SphereCollider _radiusVolume;
        [SerializeField]
        int _maxObjects = 10;
        [SerializeField]
        float _spawnRadius = 10f;
        
        Terrain _terrain;

        void OnEnable()
        {
            _radiusVolume.radius = _spawnRadius;
            _terrain = Terrain.activeTerrain;
            SpawnObjects();
        }

        void OnValidate()
        {
            _radiusVolume.radius = _spawnRadius;
        }

        void SpawnObjects()
        {
            var randomObjectsCount = Random.Range(1, _maxObjects);
            Debug.Log($"Spawning {randomObjectsCount} objects");
            for (var i = 0; i < randomObjectsCount; i++)
            {
                SpawnObject();
            }
        }

        GameObject SpawnObject()
        {
            var objectPrefab = _basicObjectPrefabsAsset.GetRandomPrefab();
            var spawnPosition = GetRandomPositionWithinRadius();
            var rotation = CalculateRotationBasedOnTerrain(spawnPosition);
            spawnPosition = AdjustObjectSpawnHeight(objectPrefab, spawnPosition);
            return Instantiate(objectPrefab, spawnPosition, rotation);
        }
        
        Vector3 GetRandomPositionWithinRadius()
        {
            var randomPoint = Random.insideUnitCircle * _spawnRadius;
            var spawnPosition = new Vector3(randomPoint.x, 0, randomPoint.y) + transform.position;
            spawnPosition.y = _terrain.SampleHeight(spawnPosition);
            return spawnPosition;
        }
        
        Quaternion CalculateRotationBasedOnTerrain(Vector3 position)
        {
            var terrainSize = _terrain.terrainData.size;
            var normalizedPosition = new Vector2(position.x / terrainSize.x, position.z / terrainSize.z);
            return Quaternion.FromToRotation(Vector3.up, 
                _terrain.terrainData.GetInterpolatedNormal(normalizedPosition.x, normalizedPosition.y));
        }

        static Vector3 AdjustObjectSpawnHeight(GameObject spawnableObject, Vector3 position)
        {
            var childrenWithMeshRenderers = spawnableObject.GetComponentsInChildren<MeshRenderer>();
            var additionalHeight = childrenWithMeshRenderers.Select(child => child.bounds.size.y / 2).Max();
            return position + new Vector3(0, additionalHeight, 0);
        }
    }
}