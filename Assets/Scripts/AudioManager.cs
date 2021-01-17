using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip gem;

    private AudioSource audios;


    // Start is called before the first frame update
    void Start()
    {
        audios = GetComponent<AudioSource>();
    }

    public void PlaySound(string sound)
    {
        switch (sound)
        {
            case "gem":
                audios.PlayOneShot(gem);
                break;
            default:
                break;
        }
    }
}
