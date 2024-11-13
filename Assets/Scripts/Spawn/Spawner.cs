using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform startPos = null;

    public float delayMin = 1.5f;
    public float delayMax = 5;
    public float speedMin = 1;
    public float speedMax = 4;

    public bool useSpawnPlacement = false;
    public int spawnCount = 4;

    private float lastTime = 0;
    private float delayTime = 0;
    private float speed = 0;

    [HideInInspector] public ItemObject Item = null;
    [HideInInspector] public bool goLeft = false;
    [HideInInspector] public float spawnLeftPos = 0;
    [HideInInspector] public float spawnRightPos = 0;

    void Start()
    {
        if (useSpawnPlacement)
        {
            for(int i = 0; i < spawnCount; i++)
            {
                SpawnItem();
            }
        }
        else
        {
            speed = Random.Range(speedMin, speedMax); // 스피드 랜덤
        }
    }

    void Update()
    {
        if (useSpawnPlacement) return;

        if (Time.time > lastTime + delayTime) // 딜레이 
        {
            lastTime = Time.time; // 라스트타임 초기화
            delayTime = Random.Range(delayMin, delayMax); // 딜레이타임 랜덤
            SpawnItem(); // 아이템 스폰
        }
    }

    void SpawnItem()
    {
        Debug.Log("Spawn Item");
        GameObject obj = Manager.instance.SpawnFromPool(Item.id); //풀에서 꺼내옴
        obj.transform.position = GetSpawnPosition(); // 오브제의 포지션을 겟스폰포지션으로

        float direction = 0; //방향 0
        if (goLeft) direction = 180; //왼쪽일때 방향 180

        if (!useSpawnPlacement)
        {
            obj.GetComponent<Mover>().speed = speed; // 오브제 속도를 스피드로 맞춤
            obj.transform.rotation = obj.transform.rotation * Quaternion.Euler(0, direction, 0);
            // 오브제의 회전에 direction을 곱함
        }

    }

    Vector3 GetSpawnPosition() // 
    {
        if (useSpawnPlacement)
        {
            return new Vector3(Random.Range(spawnLeftPos, spawnRightPos), startPos.position.y, startPos.position.z);
            //왼쪽 오른쪽중에 랜덤x 를 구해 랜덤스폰되게
        }
        else
        {
            return startPos.position;
        }
    }
}
