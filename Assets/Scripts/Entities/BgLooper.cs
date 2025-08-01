using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BgLooper : MonoBehaviour
{
    public int numBgCount = 4;  

    public int obstacleCount = 0;
    public Vector3 obstacleLastPosition = Vector3.zero;

    void Start()
    {
        //ObstacleBase[] obstacles = GameObject.FindObjectsOfType<ObstacleBase>();  // 씬에 존재하는 모든 ObstacleBase 객체를 배열로 가져오기 (임시 코드 수정 상태)
        //obstacleLastPosition = obstacles[0].transform.position;
        //obstacleCount = obstacles.Length;

        //for (int i = 0; i < obstacleCount; i++)
        //{
        //    obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
        //}
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered: " + collision.name);

        if (collision.CompareTag("BackGround"))
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * numBgCount;
            collision.transform.position = pos;
            return;
        }
        if(collision.CompareTag("Preset"))
        {
            collision.GetComponent<Preset>().ReturnEveryObjects();
            GameManager.Instance.SpawnManager.MakeNextPos();
        }

        ObstacleBase obstacle = collision.GetComponent<ObstacleBase>(); // 충돌한 객체가 ObstacleBase인지 확인 (임시 코드 수정 상태)
        //if (obstacle)
        //{
        //    obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
        //}
    }
}