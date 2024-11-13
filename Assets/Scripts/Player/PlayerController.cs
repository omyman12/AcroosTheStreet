using UnityEngine;
using UnityEngine.InputSystem;

public interface IHit
{
    public void GetHit();
}

public class PlayerController : MonoBehaviour, IHit
{
    public float moveDistance = 1;
    private Vector3 curPos;
    private Vector3 moveValue;
    public float moveTime = 0.4f;
    public float colliderDistCheck = 1.1f;

    public ParticleSystem particle = null;
    public Transform chick = null;
    public bool isDead = false;

    void Start()
    {
        moveValue = Vector3.zero; // 무브밸류 0
        curPos = transform.position; // 컬포스 지금위치로
    }

    public void Move(InputAction.CallbackContext context) // 인풋액션으로 받아옴
    {
        Vector3 input = context.ReadValue<Vector3>(); // 입력값을 벡터로 받아 저장

        if (input.magnitude > 1f) return; // 대각선이동은 하지않기위해 리턴

        if (context.performed) // 눌릴때
        {
            if(input.magnitude == 0f) // 대각선이동이 없을때
            {
                Moving(transform.position + moveValue); // 현재 위치에 무브밸류를 더해이동
                Rotate(moveValue); // 회전 왼쪽이면 -1 정면0 오른쪽 1 * 90
                moveValue = Vector3.zero; // 한번 이동햇으니 초기화?
            }
            else // 단일 방향으로 이동할떄
            {
                moveValue = input * moveDistance; // 이동할 방향과 거리를 설정
            }
        }
    }

    void Moving(Vector3 pos)
    {
        LeanTween.move(this.gameObject, pos, moveTime).setOnComplete(() => { if (pos.z > curPos.z) SetMoveForwardState(); });
        // 린트윈을 이용한 무브로직
    }

    void Rotate(Vector3 pos)
    {
        chick.rotation = Quaternion.Euler(270, pos.x * 90, 0); // 회전. +가 아니고 대입.
    }

    void SetMoveForwardState()
    {
        Manager.instance.UpdateDistanceCount(); //거리 +1 카운트
        curPos = transform.position; // 커포스를 지금위치로 설정
    }

    public void GetHit() //맞으면
    {
        Manager.instance.GameOver(); //게임오버
        isDead = true; // 쥬금
        ParticleSystem.EmissionModule em = particle.emission;
        em.enabled = true;
    }
}
