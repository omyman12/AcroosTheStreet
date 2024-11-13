using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool autoMove = true;
    public GameObject player = null;
    public float speed = 0.25f;
    public Vector3 offset = new Vector3(5, 7, -4);

    Vector3 depth = Vector3.zero;
    Vector3 pos = Vector3.zero;

    void Update()
    {

        if (!Manager.instance.CanPlay()) return; // 플레이가 가능하지 않을때 리턴

        if (autoMove)
        {
            depth = this.gameObject.transform.position += new Vector3(0, 0, speed * Time.deltaTime);
            // 게임오브젝트 포지션 + 스피드를 더해서 depth에 z축저장
            pos = Vector3.Lerp(gameObject.transform.position, player.transform.position + offset, Time.deltaTime);
            //Lerp를 이용해 부드러운 움직임
            gameObject.transform.position = new Vector3(pos.x, offset.y, depth.z);
            // pos값은 x만 쓸거면 나머지는 왜 정했는가.
        }
        else
        {
            pos = Vector3.Lerp(gameObject.transform.position, player.transform.position + offset, Time.deltaTime);
            // Lerp를 이용한 부드러운 움직임
            gameObject.transform.position = new Vector3(pos.x, offset.y, pos.z);
            // pos의 y값을 안쓸거면 왜 정해놓은거지?
        }
    }
}
