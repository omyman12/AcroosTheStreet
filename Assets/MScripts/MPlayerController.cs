using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MPlayerController : MonoBehaviour
{
    public void Move(InputAction.CallbackContext context)
    {
        Vector3 move = context.ReadValue<Vector3>();
        if (move.magnitude > 1f) return;
        if (context.phase == InputActionPhase.Performed)
        {
            if (move.magnitude == 0f)
            {
                //이동
            }
        }
    }

    public void Moveing()
    {
        
    }

    public void Rotate()
    {
        
    }
   
}
