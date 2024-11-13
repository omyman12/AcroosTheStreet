using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ObjectRange : MonoBehaviour
{
    bool isTarget = false;
    public ParticleSystem particle;

    void Update()
    {
        UpdateTarget();
    }

    private void UpdateTarget()
    {
        if (isTarget) return;

        Collider[] cols = Physics.OverlapSphere(transform.position, 2f);
        // 현재 오브젝트 위치를 중심으로 반지름 2의 구 내에 있는 모든 콜라이더를 검사하여 배열로 반환

        if (cols.Length > 0) // 콜라이더 감지하면
        {
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].CompareTag("Player")) // 플레이어태그와 닿으면
                {
                    particle.Play(); // 파티클
                    Destroy(gameObject, particle.main.duration); // 파티클 끝나면 파괴
                    isTarget = true;
                }
            }
        }


    }
}
