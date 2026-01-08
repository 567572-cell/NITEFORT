using UnityEngine;

public class GunSound : MonoBehaviour
{
    public AudioClip gunShot;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayGunShot()
    {
        audioSource.PlayOneShot(gunShot);
    }
}
