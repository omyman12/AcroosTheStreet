
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public bool goLeft = false;
    public bool goRight = false;

    public List<GameObject> items;
    public List<Spawner> spawnersLeft = new List<Spawner>(); // 왼쪽 스포너 초기화
    public List<Spawner> spawnersRight = new List<Spawner>(); // 오른쪽 스초너 초기화

    void Start()
    {
        int itemId1 = Random.Range(0, items.Count); // 아이템 개수만큼 랜덤값 itmeid1에 저장
        int itemId2 = Random.Range(0, items.Count); // 아이템 개수만큼 랜덤값 itemid2에 저장

        ItemObject item1 = items[itemId1].GetComponent<ItemObject>(); // 아이템들은 itemobject 컴포넌트를 얻음
        ItemObject item2 = items[itemId2].GetComponent<ItemObject>();

        int direction = Random.Range(0, 2); //방향 랜덤으로

        if (direction > 0) { goLeft = false; goRight = true; } else { goLeft = true; goRight = false; } // 1이 뜨면 오른쪽 0이뜨면 왼쪽

        for(int i = 0; i< spawnersLeft.Count; i++) // 왼쪽 스포너의 개수만큼
        {
            if (i % 2 != 0) 
            {
                spawnersLeft[i].Item = item1; // 짝수는 아이템1
            }
            else
            {
                spawnersLeft[i].Item = item2; // 홀수는 아이템2
            }
            spawnersLeft[i].goLeft = goLeft; // 스포너의 방향을 goleft로
            spawnersLeft[i].gameObject.SetActive(goRight); // 오른쪽 방향일때만 스포너 활성화
            spawnersLeft[i].spawnLeftPos = spawnersLeft[i].transform.position.x; // 스포너 위치 초기화
        }


        for (int i = 0; i < spawnersRight.Count; i++)
        {
            if (i % 2 != 0)
            {
                spawnersRight[i].Item = item1;

            }
            else
            {
                spawnersRight[i].Item = item2;

            }
            spawnersRight[i].goLeft = goLeft; // 방향을 goleft로
            spawnersRight[i].gameObject.SetActive(goLeft); // 왼쪽 방향일때만 스포너 활성화
            spawnersRight[i].spawnLeftPos = spawnersRight[i].transform.position.x; // 스포너 위치 초기화
        }
    }
}
