using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preset : MonoBehaviour
{
    private PresetSpawnManager _spawnManager;

    public void Init(PresetSpawnManager spawnManager)
    { _spawnManager = spawnManager; }

    public void ReturnEveryObjects()
    {
        ObstacleBase[] obstacles = GetComponentsInChildren<ObstacleBase>();
        ItemBase[] itemBases = GetComponentsInChildren<ItemBase>();

        foreach(var obstacle in obstacles)
        {
            _spawnManager.ReturnObstacle(obstacle);
        }

        foreach(var item in itemBases)
        {
            _spawnManager.ReturnItem(item);
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
    }
}
