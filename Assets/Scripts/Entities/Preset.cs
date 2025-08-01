using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preset : MonoBehaviour
{
    private PresetSpawnManager _spawnManager;

    public void Init(PresetSpawnManager spawnManager)
    { _spawnManager = spawnManager; }

    /// <summary>
    /// 해당 오브젝트의 자식 오브젝트들을 모두 오브젝트 풀로 되돌리는 함수
    /// TODO: 이 오브젝트가 제거영역에 닿았을때 실행하도록 구현필요
    /// </summary>
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

        _spawnManager = null;
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position + new Vector3(7.5f, 0, 0), new Vector3(15, 10, 1));
    }
}
