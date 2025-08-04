using System.Linq;
using UnityEngine;

public class ShowGizmos : MonoBehaviour
{
    [SerializeField] BoxCollider2D topA;
    [SerializeField] BoxCollider2D topB;
    [SerializeField] BoxCollider2D BotA;
    [SerializeField] BoxCollider2D BotB;
    [SerializeField] BoxCollider2D brkObs;
    [SerializeField] CircleCollider2D coinA;
    [SerializeField] CircleCollider2D coinB;
    [SerializeField] CircleCollider2D recoveryItem;
    [SerializeField] CircleCollider2D speedUpItem;
    [SerializeField] CircleCollider2D magnetItem;
    [SerializeField] CircleCollider2D invincibleItem;

    /// <summary>
    /// 프리셋 만들때 에디터에서 기즈모 표시되게 하는 스크립트
    /// 일반 장애물 = 하얀색, 공격 가능한 장애물 = 빨간색, 코인 = 노란색, 체력회복 = 파란색, 속도업 = 초록색, 자석 = 시안, 무적 = 마젠타
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(new Vector3(10f, 0, 0), new Vector3(20, 10, 1));

        Transform[] obstacleTopAPos = transform.Find("ObstacleTopASpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.parent != transform).ToArray();
        Transform[] obstacleTopBPos = transform.Find("ObstacleTopBSpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.parent != transform).ToArray();
        Transform[] obstacleBotAPos = transform.Find("ObstacleBotASpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.parent != transform).ToArray();
        Transform[] obstacleBotBPos = transform.Find("ObstacleBotBSpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.parent != transform).ToArray();
        Transform[] obstacleBreakPos = transform.Find("ObstacleBreakSpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.parent != transform).ToArray();
        Transform[] coinItemAPos = transform.Find("CoinItemASpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.parent != transform).ToArray();
        Transform[] coinItemBPos = transform.Find("CoinItemBSpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.parent != transform).ToArray();
        Transform[] recoveryItemPos = transform.Find("RecoverItemSpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.parent != transform).ToArray();
        Transform[] speedUpItemPos = transform.Find("SpeedUpItemSpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.parent != transform).ToArray();
        Transform[] magnetItemPos = transform.Find("MagnetItemSpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.parent != transform).ToArray();
        Transform[] invincibleItemPos = transform.Find("InvincibleItemSpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.parent != transform).ToArray();



        foreach (Transform t in obstacleTopAPos)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawCube(new Vector3(t.position.x, topA.offset.y, 0), new Vector3(topA.size.x, topA.size.y, 1));
        }
        foreach (Transform t in obstacleTopBPos)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawCube(new Vector3(t.position.x, topB.offset.y, 0), new Vector3(topB.size.x, topB.size.y, 1));
        }
        foreach (Transform t in obstacleBotAPos)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawCube(new Vector3(t.position.x, BotA.offset.y, 0), new Vector3(BotA.size.x, BotA.size.y, 1));
        }
        foreach (Transform t in obstacleBotBPos)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawCube(new Vector3(t.position.x, BotB.offset.y, 0), new Vector3(BotB.size.x, BotB.size.y, 1));
        }
        foreach (Transform t in obstacleBreakPos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(new Vector3(t.position.x, brkObs.offset.y, 0), new Vector3(brkObs.size.x, brkObs.size.y, 1));
        }
        foreach (Transform t in coinItemAPos)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(new Vector3(t.position.x, t.position.y, 0), coinA.radius);
        }
        foreach (Transform t in coinItemBPos)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(new Vector3(t.position.x, t.position.y, 0), coinB.radius);
        }
        foreach (Transform t in recoveryItemPos)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(new Vector3(t.position.x, t.position.y, 0), recoveryItem.radius);
        }
        foreach (Transform t in speedUpItemPos)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(new Vector3(t.position.x, t.position.y, 0), speedUpItem.radius);
        }
        foreach (Transform t in magnetItemPos)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(new Vector3(t.position.x, t.position.y, 0), magnetItem.radius);
        }
        foreach (Transform t in invincibleItemPos)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(new Vector3(t.position.x, t.position.y, 0), invincibleItem.radius);
        }
    }
}
