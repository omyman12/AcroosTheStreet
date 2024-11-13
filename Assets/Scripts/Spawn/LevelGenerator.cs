using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<GameObject> platform = new List<GameObject>(); // 플랫폼으로 사용할 게임오브젝트의 리스트
    public List<float> height = new List<float>(); // 각 플랫폼의 높이값을 저장하는 리스트

    private int rndRange = 0;
    private float lastPos = 0;
    private float lastScale = 0;

    public void RandomGenerator()
    {
        rndRange = Random.Range(0, platform.Count); // 플랫폼 개수에서 랜덤
        for (int i = 0; i < platform.Count; i++)
        {
            CreateLevelObject(platform[i], height[i], i); // 각 플랫폼마다 crate
        }
    }

    public void CreateLevelObject(GameObject obj, float height, int value)
    {
        if (rndRange == value) // 랜덤값과 밸류가 같을때 즉 랜덤으로 플랫폼이 생성됌
        {
            GameObject go = Instantiate(obj) as GameObject;

            // 여기 두줄이 핵심
            float offset = lastPos + (lastScale * 0.5f); // 이전 플랫폼의 끝위치 + 크기를 곱해서 끝에서 시작할수있게함 
            offset += (go.transform.localScale.z) * 0.5f; // 새 플렛폼의 길이를 반영하여 위치 조정

            Vector3 pos = new Vector3(0, height, offset); // 새로운 플랫폼 위치 설정

            go.transform.position = pos; // 플팻폼의 위치 설정

            lastPos = go.transform.position.z; //현재 포지션 z를 lastpos에 저장
            lastScale = go.transform.localScale.z; //현재 크기z를 lastsacle에 저장

            go.transform.parent = this.transform; // 플랫폼의 부모를 여기로 설정
        }
    }
}
