using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f, displayDuration = 2f;
    public GameObject player;
    public CanvasGroup exitCanvasGroup, caughtCanvasGroup;
    public AudioSource caughtAudio, winAudio;
    public Text scoreBoard;

    bool m_IsGameEnding = false, m_IsCaught = false;
    bool m_HasAudioPlayed = false;
    float timer = 0;
    int score = 0;

    // Update is called once per frame
    void Update()
    {
        if (m_IsGameEnding)
            EndLevel(exitCanvasGroup, true, winAudio);
        if (m_IsCaught)
            EndLevel(caughtCanvasGroup, false, caughtAudio);
    }

    public void CaughtPlayer()
    {
        m_IsCaught = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && score == 3)
            m_IsGameEnding = true;
    }

    void EndLevel(CanvasGroup canvasGroup, bool isEnd, AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }

        timer += Time.deltaTime;

        if (timer < fadeDuration)
            canvasGroup.alpha += timer / fadeDuration;
        if (timer > fadeDuration + displayDuration)
        {
            if (isEnd)
                Application.Quit();
            else
                SceneManager.LoadScene(0);
        }
    }

    public void AddScore()
    {
        ++score;
        scoreBoard.text = "Score: " + score + "/3";
    }
}
