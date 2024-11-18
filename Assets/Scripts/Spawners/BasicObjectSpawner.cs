using ScriptableObjects;
using UnityEngine;

namespace Spawners
{
    public class BasicObjectSpawner : MonoBehaviour
    {
        [SerializeField]
        BasicPrefabsAsset _basicObjectPrefabsAsset;
        [SerializeField]
        int _maxObjects = 10;
        [SerializeField]
        float _spawnRadius = 10f;
        
        Terrain _terrain;

        void OnEnable()
        {
            _terrain = Terrain.activeTerrain;
            SpawnObjects();
        }
        
        void SpawnObjects()
        {
            var randomObjectsCount = Random.Range(1, _maxObjects);
            for (var i = 1; i < randomObjectsCount; i++)
            {
                SpawnObject();
            }
        }

        GameObject SpawnObject()
        {
            var objectPrefab = _basicObjectPrefabsAsset.GetRandomPrefab();
            var spawnPosition = GetRandomPositionWithinRadius();
            var rotation = CalculateRotationBasedOnTerrain(spawnPosition);
            return Instantiate(objectPrefab, transform.position, rotation);
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
    }
}