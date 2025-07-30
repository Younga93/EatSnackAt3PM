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
    private static PresetSpawnManager _instance;
    public static PresetSpawnManager Instance { get { return _instance; } }

    private ObjectPool<NormalObstacle> obstacleTopAPool;
    private ObjectPool<NormalObstacle> obstacleTopBPool;
    private ObjectPool<NormalObstacle> obstacleBotAPool;
    private ObjectPool<NormalObstacle> obstacleBotBPool;

    private ObjectPool<BreakableObstacle> breakableObstaclePool;

    private ObjectPool<CoinItem> coinItemAPool;
    private ObjectPool<CoinItem> coinItemBPool;

    private ObjectPool<RecoveryItem> recoveryItemPool;

    private ObjectPool<SpeedUpItem> speedUpItemPool;

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

    [Header("Preset")]
    [SerializeField] GameObject[] presets;

    public void Init()
    {
        obstacleTopAPool = new ObjectPool<NormalObstacle>(obstacleTopAPrefab, 10, transform);
        obstacleTopBPool = new ObjectPool<NormalObstacle>(obstacleTopBPrefab, 10, transform);
        obstacleBotAPool = new ObjectPool<NormalObstacle>(obstacleBotAPrefab, 10, transform);
        obstacleBotBPool = new ObjectPool<NormalObstacle>(obstacleBotBPrefab, 10, transform);

        //breakableObstaclePool = new ObjectPool<BreakableObstacle>(breakableObstaclePrefab, 10);

        //coinItemAPool = new ObjectPool<CoinItem>(coinItemAPrefab, 30);
        //coinItemBPool = new ObjectPool<CoinItem>(coinItemBPrefab, 10);

        //recoveryItemPool = new ObjectPool<RecoveryItem>(recoveryItemPrefab, 10);

        //speedUpItemPool = new ObjectPool<SpeedUpItem>(SpeedUpItemPrefab, 10);
    }

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

    public void MakePreset(GameObject preset, Transform parent)
    {
        Transform[] obstacleTopAPos = preset.transform.Find("ObstacleTopASpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.position != Vector3.zero).ToArray();
        Transform[] obstacleTopBPos = preset.transform.Find("ObstacleTopBSpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.position != Vector3.zero).ToArray();
        Transform[] obstacleBotAPos = preset.transform.Find("ObstacleBotASpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.position != Vector3.zero).ToArray();
        Transform[] obstacleBotBPos = preset.transform.Find("ObstacleBotBSpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.position != Vector3.zero).ToArray();
        Transform[] obstacleBreakPos = preset.transform.Find("ObstacleBreakSpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.position != Vector3.zero).ToArray();

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
    }

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

    private void Start()
    {
        Init();

        if (presets[0] != null)
        {
            GameObject go = new GameObject("empty");
            go.AddComponent<Preset>();
            go.GetComponent<Preset>().Init(this);
            MakePreset(presets[0], go.transform);
            StartCoroutine(Return(go.GetComponent<Preset>()));
        }

    }

    IEnumerator Return(Preset go)
    {
        yield return new WaitForSeconds(1f);
        go.ReturnEveryObjects();
    }

}
