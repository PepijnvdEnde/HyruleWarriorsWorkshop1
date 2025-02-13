using UnityEngine;
using System.Collections;

public class AudioTrigger : MonoBehaviour{

    [SerializeField] private AudioSource audio;

    private void OnTriggerEnter(Collider other)
    {
    if(other.CompareTag("Player"))
        audio.Play();
    }
}