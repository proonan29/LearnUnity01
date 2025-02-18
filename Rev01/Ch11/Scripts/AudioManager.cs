using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource source;
    public List<AudioClip> sounds;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(int idx)
    {
        if (idx >=0 && sounds.Count > idx)
        {
            source.clip = sounds[idx];
            source.Play();
        }
    }
}
