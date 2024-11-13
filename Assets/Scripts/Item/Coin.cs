using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 트리거를 이용해서 태그 플레이어와 닿으면
        {
            Manager.instance.UpdateCoinCount(coinValue); //코인이 오릅니다
            Destroy(this.gameObject); // 코인은 삭제
        }
    }
}
