using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class ShowGizmos : MonoBehaviour
{
    /// <summary>
    /// 프리셋 만들때 에디터에서 기즈모 표시되게 하는 스크립트
    /// 일반 장애물 = 하얀색, 공격 가능한 장애물 = 빨간색, 코인 = 노란색, 아이템 = 파란색
    /// </summary>
    private void OnDrawGizmos()
    {
        Transform[] obstacleTopAPos = transform.Find("ObstacleTopASpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.parent != transform).ToArray();
        Transform[] obstacleTopBPos = transform.Find("ObstacleTopBSpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.parent != transform).ToArray();
        Transform[] obstacleBotAPos = transform.Find("ObstacleBotASpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.parent != transform).ToArray();
        Transform[] obstacleBotBPos = transform.Find("ObstacleBotBSpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.parent != transform).ToArray();
        Transform[] obstacleBreakPos = transform.Find("ObstacleBreakSpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.parent != transform).ToArray();
        Transform[] coinItemAPos = transform.Find("CoinItemASpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.parent != transform).ToArray();
        Transform[] coinItemBPos = transform.Find("CoinItemBSpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.parent != transform).ToArray();
        Transform[] recoveryItemPos = transform.Find("RecoverItemSpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.parent != transform).ToArray();
        Transform[] speedUpItemPos = transform.Find("SpeedUpItemSpawnPoints").GetComponentsInChildren<Transform>().Where(t => t.parent != transform).ToArray();

        

        foreach (Transform t in obstacleTopAPos)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawCube(new Vector3(t.position.x, 3.5f, 0), new Vector3(1, 3, 1));
        }
        foreach (Transform t in obstacleTopBPos)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawCube(new Vector3(t.position.x, 1.5f, 0), new Vector3(1, 7, 1));
        }
        foreach (Transform t in obstacleBotAPos)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawCube(new Vector3(t.position.x, -4f, 0), new Vector3(1, 2, 1));
        }
        foreach (Transform t in obstacleBotBPos)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawCube(new Vector3(t.position.x, -3.5f, 0), new Vector3(1, 3, 1));
        }
        foreach (Transform t in obstacleBreakPos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(new Vector3(t.position.x, -4f, 0), new Vector3(1, 2, 1));
        }
        foreach (Transform t in coinItemAPos)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(new Vector3(t.position.x, t.position.y, 0), 0.5f);
        }
        foreach (Transform t in coinItemBPos)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(new Vector3(t.position.x, t.position.y, 0), 1f);
        }
        foreach (Transform t in recoveryItemPos)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(new Vector3(t.position.x, t.position.y, 0), 0.5f);
        }
        foreach (Transform t in speedUpItemPos)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(new Vector3(t.position.x, t.position.y, 0), 0.5f);
        }
    }
}
