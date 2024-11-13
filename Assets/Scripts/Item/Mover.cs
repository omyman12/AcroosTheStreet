using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed = 1.0f;
    public float moveDirection = 0;
    public bool parentOnTrigger = true;
    public bool hitBoxOnTrigger = false;
    public GameObject moverObject = null;

    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0); // x값 스피드만큼 ++
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 태그 플레이어와 닿으면
        { 
            if (parentOnTrigger) 
            {
                other.transform.parent = this.transform; // 트리거에 닿은친구 부모를 여기로함 (같이 움직일라고)
            }

            if (hitBoxOnTrigger) //히트박스에 닿으면
            {
                other.GetComponent<IHit>().GetHit(); //죽 음
            }

        }
    }

    void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Player")) //태그 플레이어와 닿은게 깨지면
        {
            if (parentOnTrigger)
            {
                other.transform.parent = null; //부모 널
            }
        }
    }
}
