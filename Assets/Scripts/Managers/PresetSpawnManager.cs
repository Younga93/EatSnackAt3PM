using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ObstacleType
{
    TopA,
    TopB,
    BotA,
    BotB,
    Breakable
}

public enum ItemType
{
    CoinA,
    CoinB,
    Recovery,
    SpeedUp
}

public class PresetSpawnManager : MonoBehaviour
{
    /// <summary>
    /// 각 오브젝트마다 저장해놓을 오브젝트 풀
    /// </summary>
    private ObjectPool<NormalObstacle> obstacleTopAPool;
    private ObjectPool<NormalObstacle> obstacleTopBPool;
    private ObjectPool<NormalObstacle> obstacleBotAPool;
    private ObjectPool<NormalObstacle> obstacleBotBPool;

    private ObjectPool<BreakableObstacle> breakableObstaclePool;

    private ObjectPool<CoinItem> coinItemAPool;
    private ObjectPool<CoinItem> coinItemBPool;

    private ObjectPool<RecoveryItem> recoveryItemPool;

    private ObjectPool<SpeedUpItem> speedUpItemPool;

    /// <summary>
    /// 생성할 오브젝트 프리팹 목록
    /// </summary>
    [Header("Prefabs")]
    [SerializeField] NormalObstacle obstacleTopAPrefab;
    [SerializeField] NormalObstacle obstacleTopBPrefab;
    [SerializeField] NormalObstacle obstacleBotAPrefab;
    [SerializeField] NormalObstacle obstacleBotBPrefab;

    [SerializeField] BreakableObstacle breakableObstaclePrefab;

    [SerializeField] CoinItem coinItemAPrefab;
    [SerializeField] CoinItem coinItemBPrefab;

    [SerializeField] RecoveryItem recoveryItemPrefab;

    [SerializeField] SpeedUpItem SpeedUpItemPrefab;

    /// <summary>
    /// 사용할 프리셋 목록
    /// </summary>
    [Header("Preset")]
    [SerializeField] GameObject[] presets;

    /// <summary>
    /// 오브젝트 풀을 초기화하는 함수
    /// </summary>
    public void Init()
    {
        obstacleTopAPool = new ObjectPool<NormalObstacle>(obstacleTopAPrefab, 10, transform);
        obstacleTopBPool = new ObjectPool<NormalObstacle>(obstacleTopBPrefab, 10, transform);
        obstacleBotAPool = new ObjectPool<NormalObstacle>(obstacleBotAPrefab, 10, transform);
        obstacleBotBPool = new ObjectPool<NormalObstacle>(obstacleBotBPrefab, 10, transform);

        breakableObstaclePool = new ObjectPool<BreakableObstacle>(breakableObstaclePrefab, 10, transform);

        coinItemAPool = new ObjectPool<CoinItem>(coinItemAPrefab, 30, transform);
        coinItemBPool = new ObjectPool<CoinItem>(coinItemBPrefab, 10, transform);

        recoveryItemPool = new ObjectPool<RecoveryItem>(recoveryItemPrefab, 10, transform);

        speedUpItemPool = new ObjectPool<SpeedUpItem>(SpeedUpItemPrefab, 10, transform);
    }

    /// <summary>
    /// 장애물 타입에 따라 장애물 풀에서 장애물을 가져오는 함수
    /// </summary>
    /// <param name="type">가져오고자 하는 장애물 타입</param>
    /// <returns>실제로 가져온 장애물</returns>
    public GameObject GetObstacle(ObstacleType type)
    {
        switch (type)
        {
            case ObstacleType.TopA: return obstacleTopAPool.Get().gameObject;
            case ObstacleType.TopB: return obstacleTopBPool.Get().gameObject;
            case ObstacleType.BotA: return obstacleBotAPool.Get().gameObject;
            case ObstacleType.BotB: return obstacleBotBPool.Get().gameObject;
            case ObstacleType.Breakable: return breakableObstaclePool.Get().gameObject;
            default: return null;
        }
    }

    /// <summary>
    /// 아이템 타입에 따라 아이템 풀에서 아이템을 가져오는 함수
    /// </summary>
    /// <param name="type">가져오고자 하는 아이템 타입</param>
    /// <returns>실제로 가져온 아이템</returns>
    public GameObject GetItem(ItemType type)
    {
        switch (type)
        {
            case ItemType.CoinA: return coinItemAPool.Get().gameObject;
            case ItemType.CoinB: return coinItemBPool.Get().gameObject;
            case ItemType.Recovery: return recoveryItemPool.Get().gameObject;
            case ItemType.SpeedUp: return speedUpItemPool.Get().gameObject;
            default : return null;
        }
    }

    /// <summary>
    /// 프리셋 정보를 바탕으로 오브젝트 풀에서 가져와 실제로 배치하는 함수
    /// 되게 쓸때없이 반복적이고 긴데 개선할 방법이 있는지 찾아봐야 할 듯
    /// </summary>
    /// <param name="preset">사용하고자 하는 배치 프리셋 정보</param>
    /// <param name="parent">가져온 오브젝트들을 묶어둘 부모 오브젝트</param>
    public void PlaceObjects(GameObject preset, Transform parent)
    {
        Transform[] obstacleTopAPos = preset.transform.Find("ObstacleTopASpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.parent != preset.transform).ToArray();
        Transform[] obstacleTopBPos = preset.transform.Find("ObstacleTopBSpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.parent != preset.transform).ToArray();
        Transform[] obstacleBotAPos = preset.transform.Find("ObstacleBotASpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.parent != preset.transform).ToArray();
        Transform[] obstacleBotBPos = preset.transform.Find("ObstacleBotBSpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.parent != preset.transform).ToArray();
        Transform[] obstacleBreakPos = preset.transform.Find("ObstacleBreakSpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.parent != preset.transform).ToArray();
        Transform[] coinItemAPos = preset.transform.Find("CoinItemASpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.parent != preset.transform).ToArray();
        Transform[] coinItemBPos = preset.transform.Find("CoinItemBSpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.parent != preset.transform).ToArray();
        Transform[] recoveryItemPos = preset.transform.Find("RecoverItemSpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.parent != preset.transform).ToArray();
        Transform[] speedUpItemPos = preset.transform.Find("SpeedUpItemSpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.parent != preset.transform).ToArray();

        foreach (Transform t in obstacleTopAPos)
        {
            GameObject go = GetObstacle(ObstacleType.TopA);
            go.transform.parent = parent;
            go.transform.localPosition = t.position;
        }
        foreach (Transform t in obstacleTopBPos)
        {
            GameObject go = GetObstacle(ObstacleType.TopB);
            go.transform.parent = parent;
            go.transform.localPosition = t.position;
        }
        foreach (Transform t in obstacleBotAPos)
        {
            GameObject go = GetObstacle(ObstacleType.BotA);
            go.transform.parent = parent;
            go.transform.localPosition = t.position;
        }
        foreach (Transform t in obstacleBotBPos)
        {
            GameObject go = GetObstacle(ObstacleType.BotB);
            go.transform.parent = parent;
            go.transform.localPosition = t.position;
        }
        foreach (Transform t in obstacleBreakPos)
        {
            GameObject go = GetObstacle(ObstacleType.Breakable);
            go.transform.parent = parent;
            go.transform.localPosition = t.position;
        }
        foreach (Transform t in coinItemAPos)
        {
            GameObject go = GetItem(ItemType.CoinA);
            go.transform.parent = parent;
            go.transform.localPosition = t.position;
        }
        foreach (Transform t in coinItemBPos)
        {
            GameObject go = GetItem(ItemType.CoinB);
            go.transform.parent = parent;
            go.transform.localPosition = t.position;
        }
        foreach (Transform t in recoveryItemPos)
        {
            GameObject go = GetItem(ItemType.Recovery);
            go.transform.parent = parent;
            go.transform.localPosition = t.position;
        }
        foreach (Transform t in speedUpItemPos)
        {
            GameObject go = GetItem(ItemType.SpeedUp);
            go.transform.parent = parent;
            go.transform.localPosition = t.position;
        }
    }

    /// <summary>
    /// Obstacle타입의 오브젝트를 오브젝트 풀로 되돌리는 함수
    /// </summary>
    /// <param name="obj">되돌리고자 하는 장애물 오브젝트</param>
    public void ReturnObstacle(ObstacleBase obj)
    {
        ObstacleType type = obj.GetObstacleType();
        switch (type)
        {
            case ObstacleType.TopA: obstacleTopAPool.Return(obj as NormalObstacle); break;
            case ObstacleType.TopB: obstacleTopBPool.Return(obj as NormalObstacle); break;
            case ObstacleType.BotA: obstacleBotAPool.Return(obj as NormalObstacle); break;
            case ObstacleType.BotB: obstacleBotBPool.Return(obj as NormalObstacle); break;
            case ObstacleType.Breakable: breakableObstaclePool.Return(obj as BreakableObstacle); break;
            default: break;
        }
    }

    /// <summary>
    /// Item타입의 오브젝트를 오브젝트 풀로 되돌리는 함수
    /// </summary>
    /// <param name="item">되돌리고자 하는 아이템 오브젝트</param>
    public void ReturnItem(ItemBase item)
    {
        ItemType type = item.GetItemType();
        switch (type)
        {
            case ItemType.CoinA: coinItemAPool.Return(item as CoinItem); break;
            case ItemType.CoinB: coinItemBPool.Return(item as CoinItem); break;
            case ItemType.Recovery: recoveryItemPool.Return(item as RecoveryItem); break;
            case ItemType.SpeedUp: speedUpItemPool.Return(item as SpeedUpItem); break;
            default : break;
        }
    }

    /// <summary>
    /// 랜덤한 배치 프리셋을 선택하고 이를 지정 위치에 생성하는 함수
    /// </summary>
    /// <param name="spawnPos">해당 배치 프리셋을 생성할 위치</param>
    public void MakePreset(float x)
    {
        int n = Random.Range(0, presets.Length); // 랜덤한 프리셋을 선택
        if (n < presets.Length) // 해당 프리셋이 실제로 존재하는 지 확인
        {
            GameObject go = new GameObject("[PRESET]");
            go.transform.position = new Vector3(x,0,0);
            go.AddComponent<Preset>();
            go.GetComponent<Preset>().Init(this);
            PlaceObjects(presets[n], go.transform);
        }
    }

    private void Awake()
    {
        Init();

        MakePreset(5);
        MakePreset(30);
        MakePreset(55);
        MakePreset(80);
        MakePreset(105);
    }
}
