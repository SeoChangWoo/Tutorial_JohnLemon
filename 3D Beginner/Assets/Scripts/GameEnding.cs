using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public GameObject player; // �� �������� �÷��̾������Ʈ�� �־��� ���̴�.
    public bool isPlayerAtExit;  // ĵ�����׷��� ���� ���̵������� �����ϱ� ���ؼ� ����� ���̴�.
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

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audiosource) // �ⱸ�� �������� �� � ������ ������ ������ �Լ�
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
