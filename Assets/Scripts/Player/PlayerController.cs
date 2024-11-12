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
        moveValue = Vector3.zero;
        curPos = transform.position;
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
                Rotate(moveValue); // 회전
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
        chick.rotation = Quaternion.Euler(270, pos.x * 90, 0); // 회전
    }

    void SetMoveForwardState()
    {
        Manager.instance.UpdateDistanceCount();
        curPos = transform.position;
    }

    public void GetHit()
    {
        Manager.instance.GameOver();
        isDead = true;
        ParticleSystem.EmissionModule em = particle.emission;
        em.enabled = true;
    }
}
