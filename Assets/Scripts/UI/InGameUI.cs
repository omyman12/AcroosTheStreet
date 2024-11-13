using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public Text coin = null;
    public Text distance = null;
    public GameObject guiGameOver = null;

    private void Start()
    {
        Manager.instance.coins += UpdateCoinCount; // 코인 업데이트
        Manager.instance.distance += UpdateDistanceCount; //거리 업데이트
        Manager.instance.gameOver += GameOver; // 게임오버
    }

    public void UpdateCoinCount(int value) 
    {
        coin.text = value.ToString();
    }

    public void UpdateDistanceCount(int value)
    {
        distance.text = value.ToString();
    }

    void GameOver()
    {
        guiGameOver.SetActive(true);
    }

    public void PlayAgain() // 다시시작
    {
        Scene scene = SceneManager.GetActiveScene(); // 현재 씬 정보를 씬에 저장
        Manager.instance.coins -= UpdateCoinCount; // 이벤트 등록취소
        Manager.instance.distance -= UpdateDistanceCount;
        Manager.instance.gameOver -= GameOver;
        SceneManager.LoadScene(scene.name); // 씬로드
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
