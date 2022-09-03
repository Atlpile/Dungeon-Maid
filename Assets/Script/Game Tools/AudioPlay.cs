using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour, IObject
{
    private AudioSource playAudio;
    private float volume;
    private AudioSource Audio
    {
        get
        {
            if (playAudio == null) playAudio = GetComponent<AudioSource>();
            return Audio;
        }
    }
    public E_SoundType SoundType { get; private set; }
    public int SoundChannel { get; private set; }

    public bool IsActive => throw new System.NotImplementedException();

    #region 接口设置

    //创建并获取（对象池）字典数据
    public void Create(Dictionary<string, object> valueDic)
    {
        AudioClip dictClip = valueDic.EM_TryGetValue<AudioClip>("audio");

        bool isLoop = valueDic.EM_TryGetValue<bool>("loop");
        SoundType = valueDic.EM_TryGetValue<E_SoundType>("soundtype");
        SoundChannel = valueDic.EM_TryGetValue<int>("channel");

        Audio.clip = dictClip;
        Audio.loop = isLoop;
        Audio.UnPause();
        gameObject.SetActive(true);

        if (!Audio.isPlaying) Audio.Play();
        if (!isLoop) StartCoroutine(IE_PlayAudioOnce());
    }

    public void Hide()
    {
        gameObject.SetActive(false);

        if (SoundChannel == 0)
            AudioManager_Old.Instance.RemovePlayAudio(SoundType, this);
        else
            AudioManager_Old.Instance.RemovePlayAudio(SoundType, SoundChannel);

    }

    public void Delete()
    {
        Destroy(this);
    }

    #endregion

    #region AudioSource设置

    public void Pause_AS()
    {
        Audio.Pause();
    }

    public void Resume_AS()
    {
        Audio.UnPause();
    }

    public void SetVolume_AS(float value)
    {
        volume = value;
        Audio.volume = value;
    }

    #endregion

    #region 协程

    /// <summary>
    /// 播放一次音频（至音频播放结束）
    /// </summary>
    /// <returns></returns>
    private IEnumerator IE_PlayAudioOnce()
    {
        yield return new WaitUntil(() => !Audio.isPlaying);
        Hide();
    }

    #endregion
}
