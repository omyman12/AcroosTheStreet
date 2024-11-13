using UnityEngine;
using System;
using System.Collections.Generic;

public class Manager : MonoBehaviour
{
    public int levelCount = 50;
    public Camera camera = null;
    public LevelGenerator levelGenerator = null;

    private int currentCoins = 0;
    private int currentDistance = 0;
    private bool canPlay = false;
    private AudioSource effect;
    private AudioClip clip = null;

    public event Action<int> coins;
    public event Action<int> distance;
    public event Action gameOver;

    public List<ItemObject> pools;
    public Dictionary<int, Queue<GameObject>> poolDict;

    private static Manager s_Instance;
    public static Manager instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(Manager)) as Manager;
            }

            return s_Instance;
        }
    }

    private void Awake()
    {
        poolDict = new Dictionary<int, Queue<GameObject>>(); // 풀 딕셔너리 초기화
        foreach(var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>(); //오브젝트풀 초기화
            for(int i = 0; i < 3; i++) // 3번
            {
                GameObject obj = Instantiate(pool.prefab); // 풀에 있는 프리팹 복제해서 obj에 넣음
                obj.SetActive(false); // obj 끄고
                objectPool.Enqueue(obj); // 풀에 추가
            }
            poolDict.Add(pool.id, objectPool);  //풀딕셔너리에 풀아이디와 아이템을 넣어줌
        }
    }

    private void Start()
    {
        effect = GetComponent<AudioSource>();
        clip = effect.clip; // 이펙트소리

        for (int i = 0; i < levelCount; i++)
        {
            levelGenerator.RandomGenerator(); // 시작될때 맵 생성
        }
    }

    public bool CanPlay()
    {
        return canPlay;
    }

    public void StartPlay()
    {
        canPlay = true;
    }

    public void UpdateCoinCount(int value) // 코인카운트 ++
    {
        currentCoins += value;
        effect.PlayOneShot(clip);
        coins?.Invoke(currentCoins);
    }

    public void UpdateDistanceCount() // 거리 카운트++
    {
        currentDistance += 1;
        distance?.Invoke(currentDistance);
        levelGenerator.RandomGenerator(); // 새로운 레벨 생성
    }

    public GameObject SpawnFromPool(int id)
    {
        if (!poolDict.ContainsKey(id)) return null; // id가 풀에 없으면 null

        if (poolDict[id].Count > 0) // 풀에 id가 있으면
        {
            GameObject obj = poolDict[id].Dequeue(); // 큐에서 오브젝트 가져옴
            obj.SetActive(true); // 오브젝트 켜
            return obj;
        }
        else
        {
            Debug.Log("Create Item");
            var newObj = Instantiate(pools.Find(x => x.id == id).prefab); // 찾고 복제해서 newobj에 담아줌
            newObj.SetActive(false); //오브제 꺼
            poolDict[id].Enqueue(newObj); // 오브제 넣어
            return newObj;
        }
    }

    public void ReturnObject(GameObject obj, int id)
    {
        if (!poolDict.ContainsKey(id)) return; // 풀에 id가 업으면 리턴

        obj.SetActive(false); // 오브제 꺼 > 
        poolDict[id].Enqueue(obj); // 오브제 풀에 넣어
    }

    public void GameOver() // 게임오버
    {
        camera.GetComponent<CameraShake>().Shake();
        camera.GetComponent<CameraFollow>().enabled = false;
        gameOver?.Invoke();
    }
}
