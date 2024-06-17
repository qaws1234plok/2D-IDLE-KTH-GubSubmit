using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    [SerializeField] List<AudioSource> AudioSources;
    protected override void Awake()
    {
        base.Awake();
    }

    public void PlayerAttackSound()
    {
        AudioSources[0].PlayOneShot(AudioSources[0].clip);
    }

    public void PlayerUpgradeSound()
    {
        AudioSources[1].PlayOneShot(AudioSources[1].clip);
    }
}
