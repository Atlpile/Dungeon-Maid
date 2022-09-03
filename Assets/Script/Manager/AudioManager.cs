using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : BaseManager<AudioManager>
{
    private Dictionary<string, int> AudioDict = new Dictionary<string, int>();
    [SerializeField] private AudioSource BGM_AS;
    [SerializeField] private AudioSource SoundClip_AS;
    public string currentBGM = "";
    protected override void Awake()
    {
        base.Awake();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            SoundPlay("throw_1");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            BGMPlay("bgm_0");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            BGMStop();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            BGMPause();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            BGMResume();
        }
    }

    public void SoundPlay(string audioName, float volume = 0.5f)
    {
        //若对应的audioName存在字典中（若为重复的音频）
        if (AudioDict.ContainsKey(audioName))
        {
            #region 同步加载音频

            // //当字典中音频的数量<=10时，播放对应名称的音频
            // if (AudioDict[audioName] <= 10)
            // {
            //     AudioClip audioClip = GetAudioClip(audioName);

            //     if (audioClip != null)
            //     {
            //         StartCoroutine(PlayEndRemoveClip(audioClip, audioName));
            //         PlayClip(audioClip, null, volume);
            //         AudioDict[audioName]++;
            //     }
            // }

            #endregion

            #region 异步加载音频

            if (AudioDict[audioName] <= 10)
            {
                ResourcesManager.Instance.LoadAsync<AudioClip>("Audio/" + audioName, (clip) =>
                {
                    if (clip != null)
                    {
                        StartCoroutine(PlayEndRemoveClip(clip, audioName));
                        PlayClip(clip, null, volume);
                        AudioDict[audioName]++;
                    }
                });
            }

            #endregion

            #region 对象池异步加载音频

            //TODO:由于对象池中只满足GameObject预设体，故暂时无法用对象池播放音频，之后会对对象池进行泛型优化

            // if (AudioDict[audioName] <= 10)
            // {
            //     // ResourcesManager.Instance.LoadAsync<AudioClip>("Audio/" + audioName, (clip) =>
            //     // {
            //     //     if (clip != null)
            //     //     {
            //     //         StartCoroutine(PlayEndRemoveClip(clip, audioName));
            //     //         PlayClip(clip, null, volume);
            //     //         AudioDict[audioName]++;
            //     //     }
            //     // });
            // }

            #endregion

        }
        //若对应的AudioName不存在字典中（若为新音频）
        else
        {
            AudioDict.Add(audioName, 1);

            #region 同步加载音频

            // AudioClip audioClip2 = GetAudioClip(audioName);

            // if (audioClip2 != null)
            // {
            //     StartCoroutine(PlayEndRemoveClip(audioClip2, audioName));
            //     PlayClip(audioClip2, null, volume);
            //     AudioDict[audioName]++;
            // }

            #endregion

            #region 异步加载音频

            ResourcesManager.Instance.LoadAsync<AudioClip>("Audio/" + audioName, (clip2) =>
            {
                if (clip2 != null)
                {
                    StartCoroutine(PlayEndRemoveClip(clip2, audioName));
                    PlayClip(clip2, null, volume);
                    AudioDict[audioName]++;
                }
            });

            #endregion

            #region 对象池异步加载音频
            #endregion
        }
    }

    public void BGMPlay(string bgmName, float volume = 1f)
    {
        BGMStop();
        currentBGM = bgmName;

        if (bgmName != null)
        {
            #region 同步加载BGM

            // AudioClip audioClip = GetAudioClip(bgmName);

            // if (audioClip != null)
            //     PlayBGMClip(audioClip, "BGM", 1, true);

            #endregion

            #region 异步加载BGM

            ResourcesManager.Instance.LoadAsync<AudioClip>("Audio/" + bgmName, (clip) =>
            {
                PlayBGMClip(clip, "BGM", 1, true);
            });

            #endregion

        }
    }

    public void BGMStop()
    {
        if (BGM_AS != null && BGM_AS.gameObject)
        {
            Destroy(BGM_AS.gameObject);
            BGM_AS = null;
        }
    }

    public void BGMPause()
    {
        if (BGM_AS != null) BGM_AS.Pause();
    }

    public void BGMResume()
    {
        if (BGM_AS != null) BGM_AS.Play();
    }

    private AudioClip GetAudioClip(string audioName)
    {
        AudioClip resAudioClip = (AudioClip)ResourceLoader.Instance.Load(E_ResourceType.Audio, audioName);
        return resAudioClip;
    }


    private void PlayBGMClip(AudioClip audioClip, string name = null, float volume = 1f, bool isloop = true)
    {
        if (audioClip != null)
        {
            GameObject obj = new GameObject(name);
            obj.transform.parent = base.transform;
            AudioSource audioSource = obj.AddComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.volume = volume;
            audioSource.loop = isloop;
            audioSource.pitch = 1f;
            audioSource.Play();
            BGM_AS = audioSource;
        }
    }

    private void PlayClip(AudioClip audioClip, string objectName = null, float volume = 1f)
    {
        GameObject gameObject = new GameObject((objectName == null) ? "SoundClip" : objectName);
        gameObject.transform.parent = base.transform;
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(PlayEndDestroyClip(audioClip, gameObject));

        audioSource.pitch = 1f;
        audioSource.volume = volume;
        audioSource.clip = audioClip;
        audioSource.Play();
        SoundClip_AS = audioSource;
    }

    /// <summary>
    /// 音频播放结束后，销毁音频
    /// </summary>
    /// <param name="audioClip"></param>
    /// <param name="soundObj"></param>
    /// <returns></returns>
    private IEnumerator PlayEndDestroyClip(AudioClip audioClip, GameObject soundObj)
    {
        if (soundObj != null && audioClip != null)
        {
            yield return new WaitForSeconds(audioClip.length * 1f);
            Destroy(soundObj);
        }
    }

    /// <summary>
    /// 音频播放结束后，在字典中移除该音频
    /// </summary>
    /// <param name="audioClip"></param>
    /// <param name="audioName"></param>
    /// <returns></returns>
    private IEnumerator PlayEndRemoveClip(AudioClip audioClip, string audioName)
    {
        if (audioClip != null)
        {
            yield return new WaitForSeconds(audioClip.length * Time.timeScale);
            AudioDict[audioName]--;

            if (AudioDict[audioName] <= 0)
            {
                AudioDict.Remove(audioName);
            }
        }
    }

}
