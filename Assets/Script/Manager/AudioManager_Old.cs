using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager_Old : BaseManager<AudioManager_Old>
{
    [Header("音量大小控制")]
    public float BGMVolume;
    private float BGMVolumeDefaultScale = 1f;
    public float SoundEffectVolume;
    private float SoundEffectVolumeDefaultScale = 1f;

    [Header("Reference")]
    private Dictionary<int, AudioPlay> BGMChannel;
    private Dictionary<int, AudioPlay> SoundChannel;
    [SerializeField] private List<AudioPlay> SoundEffectList;
    public VolumeChanged BGMVolumeChanged;
    public VolumeChanged SEVolumeChanged;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        BGMChannel = new Dictionary<int, AudioPlay>();
        SoundEffectList = new List<AudioPlay>();
        SoundChannel = new Dictionary<int, AudioPlay>();
    }

    //播放音频
    public void PlayDicAudio(string dicAudioName, E_SoundType soundType, bool loop, int playSoundChannel = 0, float volumeScale = 1f)
    {
        AudioClip getAudioClip = (AudioClip)ResourceLoader.Instance.Load(E_ResourceType.Audio, dicAudioName);
        if (getAudioClip == null) return;

        AudioPlay getAudioPlay_OP = (AudioPlay)ObjectPool.Instance.GetObject("AudioPlay", new Dictionary<string, object>
        {
            { "audio", getAudioClip },
            { "loop", loop },
            { "soundtype", soundType },
            { "channel", playSoundChannel }
        });

        switch (soundType)
        {
            case E_SoundType.BGM:
                //若BGM轨道有声音轨道，则轨道中放入该名字的音频
                if (BGMChannel.ContainsKey(playSoundChannel))
                {
                    if (BGMChannel[playSoundChannel] != null)
                        BGMChannel[playSoundChannel].Hide();

                    BGMChannel[playSoundChannel] = getAudioPlay_OP;
                }
                else
                {
                    foreach (AudioPlay value in BGMChannel.Values)
                        value.Pause_AS();

                    BGMChannel.Add(playSoundChannel, getAudioPlay_OP);
                }
                getAudioPlay_OP.SetVolume_AS(BGMVolume * BGMVolumeDefaultScale * volumeScale);
                break;

            case E_SoundType.SoundEffect:
                //当声音频道不为0时
                if (playSoundChannel != 0)
                {
                    if (SoundChannel.ContainsKey(playSoundChannel))
                    {
                        if (SoundChannel[playSoundChannel] != null)
                            SoundChannel[playSoundChannel].Hide();

                        SoundChannel[playSoundChannel] = getAudioPlay_OP;
                    }
                    else
                    {
                        SoundChannel.Add(playSoundChannel, getAudioPlay_OP);
                    }
                }
                else
                {
                    SoundEffectList.Add(getAudioPlay_OP);
                }
                getAudioPlay_OP.SetVolume_AS(SoundEffectVolume * SoundEffectVolumeDefaultScale * volumeScale);
                break;
        }
    }

    //暂停、恢复和停止音频
    public void PauseAudio(E_SoundType soundType, int playSoundChannel)
    {
        switch (soundType)
        {
            case E_SoundType.BGM:
                if (BGMChannel.ContainsKey(playSoundChannel) && BGMChannel[playSoundChannel] != null)
                    BGMChannel[playSoundChannel].Pause_AS();
                break;

            case E_SoundType.SoundEffect:
                if (playSoundChannel != 0 && SoundChannel.ContainsKey(playSoundChannel) && SoundChannel[playSoundChannel] != null)
                    SoundChannel[playSoundChannel].Pause_AS();
                break;
        }
    }

    public void ResumeAudio(E_SoundType soundType, int playSoundChannel)
    {
        switch (soundType)
        {
            case E_SoundType.BGM:
                if (BGMChannel.ContainsKey(playSoundChannel) && BGMChannel[playSoundChannel] != null)
                    BGMChannel[playSoundChannel].Resume_AS();
                break;

            case E_SoundType.SoundEffect:
                if (playSoundChannel != 0 && SoundChannel.ContainsKey(playSoundChannel) && SoundChannel[playSoundChannel] != null)
                    SoundChannel[playSoundChannel].Resume_AS();
                break;
        }
    }

    public void StopAudio(E_SoundType soundType, int playSoundChannel)
    {
        switch (soundType)
        {
            case E_SoundType.BGM:
                if (BGMChannel.ContainsKey(playSoundChannel))
                {
                    if (BGMChannel[playSoundChannel] != null)
                    {
                        BGMChannel[playSoundChannel].Hide();
                    }
                    BGMChannel.Remove(playSoundChannel);
                }
                break;

            case E_SoundType.SoundEffect:
                if (playSoundChannel != 0 && SoundChannel.ContainsKey(playSoundChannel))
                {
                    if (SoundChannel[playSoundChannel] != null)
                    {
                        SoundChannel[playSoundChannel].Hide();
                    }
                    SoundChannel.Remove(playSoundChannel);
                }
                break;
        }
    }


    /// <summary>
    /// 设置字典中BGM的响度（通过委托在SettingPanel动态设置）（Invoke时动态传值）
    /// </summary>
    /// <param name="value"></param>
    public void SetBGMVolume(float value)
    {
        PlayerPrefs.SetFloat("BGM", value);
        BGMVolume = value;
        BGMVolumeChanged?.Invoke(value);

        foreach (AudioPlay BGMChannelVolume in BGMChannel.Values)
        {
            if (BGMChannelVolume != null)
                BGMChannelVolume.SetVolume_AS(value * BGMVolumeDefaultScale);
        }
    }

    public void SetSoundEffectVolume(float value)
    {
        PlayerPrefs.SetFloat("SoundEffect", value);
        SoundEffectVolume = value;
        SEVolumeChanged?.Invoke(value);

        foreach (AudioPlay soundEffect in SoundEffectList)
        {
            if (soundEffect != null)
                soundEffect.SetVolume_AS(value * SoundEffectVolumeDefaultScale);
        }

        foreach (AudioPlay SoundChannelVolume in SoundChannel.Values)
        {
            if (SoundChannelVolume != null)
                SoundChannelVolume.SetVolume_AS(value * SoundEffectVolumeDefaultScale);
        }
    }


    /// <summary>
    /// （从对象池中）移除播放的音频
    /// </summary>
    /// <param name="soundType"></param>
    /// <param name="audioPlay"></param>
    public void RemovePlayAudio(E_SoundType soundType, AudioPlay audioPlay)
    {
        switch (soundType)
        {
            case E_SoundType.BGM:
                break;

            case E_SoundType.SoundEffect:
                if (SoundEffectList.Contains(audioPlay))
                    SoundEffectList.Remove(audioPlay);
                break;
        }
    }

    public void RemovePlayAudio(E_SoundType soundType, int channel)
    {
        switch (soundType)
        {
            case E_SoundType.BGM:
                if (BGMChannel.ContainsKey(channel))
                    BGMChannel.Remove(channel);
                break;

            case E_SoundType.SoundEffect:
                if (SoundChannel.ContainsKey(channel))
                    SoundChannel.Remove(channel);
                break;
        }
    }

    /// <summary>
    /// 更新数据中的游戏音量大小
    /// </summary>
    private void UpdateDataGameVolume()
    {
        if (PlayerPrefs.HasKey("BGM")) BGMVolume = PlayerPrefs.GetFloat("BGM");
        else BGMVolume = 0.5f;

        if (PlayerPrefs.HasKey("SoundEffect")) SoundEffectVolume = PlayerPrefs.GetFloat("SoundEffect");
        else SoundEffectVolume = 0.5f;
    }


}
