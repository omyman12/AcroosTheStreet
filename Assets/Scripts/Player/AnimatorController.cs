using UnityEngine;
using UnityEngine.InputSystem;

public class AnimatorController : MonoBehaviour
{
    public PlayerController playerController = null;
    private Animator animator = null;

    private string trJump = "jump";
    private string trDead = "dead";

    private int Jump;
    private int Dead;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        Jump = Animator.StringToHash(trJump); 
        Dead = Animator.StringToHash(trDead);
    }

    void Update()
    {
        if (playerController.isDead)
        {
            animator.SetBool(Dead, true); // 죽엇다면 데드애니메이션 실행
        }
    }

    public void Move(InputAction.CallbackContext context) //인풋액션
    {
        Vector3 input = context.ReadValue<Vector3>(); // 값을 input에 저장
          
        if (input.magnitude > 1f) return; // 대각선이동이면 리턴

        if (context.performed) // 눌릴때
        {
            if (input.magnitude == 0f) // 아무키도 안눌릴때
            {
                animator.SetBool(Jump, false); // 점프 안함
            }
            else
            {
                animator.SetBool(Jump, true); // 점프함
            }
        }
    }
}
