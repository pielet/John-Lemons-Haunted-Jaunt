using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetScore : MonoBehaviour
{
    public AudioSource getScoreAudio;
    public GameObject player;
    public GameEnding gameEnding;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            gameEnding.AddScore();
            getScoreAudio.Play();
            gameObject.SetActive(false);
        }
    }

}
