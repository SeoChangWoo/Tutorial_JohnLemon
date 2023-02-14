using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public GameObject player; // 이 변수에는 플레이어오브젝트를 넣어줄 것이다.
    public bool isPlayerAtExit;  // 캔버스그룹을 언제 페이드인할지 결정하기 위해서 사용할 것이다.
    public CanvasGroup exitBackground;
    public float timer;

    public CanvasGroup caughtBackground;
    public bool isPlayerCaught;

    public AudioSource exitAudio;
    public AudioSource caughtAudio;
    private bool isAudioPlay;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            isPlayerAtExit = true;
        }
    }

    public void CaughtPlayer()
    {
        isPlayerCaught = true;
    }

    private void Update()
    {
        if(isPlayerAtExit == true)
        {
            EndLevel(exitBackground, false, exitAudio);
        }
        else if(isPlayerCaught == true)
        {
            EndLevel(caughtBackground, true, caughtAudio);
        }
    }

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audiosource) // 출구에 도착했을 때 어떤 동작을 할지를 정의한 함수
    {
        if(isAudioPlay == false)
        {
            audiosource.Play();
            isAudioPlay = true;
        }
        timer = timer + Time.deltaTime;
        imageCanvasGroup.alpha = timer / fadeDuration;
        if(timer > fadeDuration + 3f)
        {
            if(doRestart == true)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                Application.Quit();
            }

        }
    }
}
