using UnityEngine;

/*
 * Manager audio
 */

public class AudioSourcePool : PoolPolling<AudioSource>
{
    public AudioSourcePool(AudioSource prefab) : base(prefab)
    {

    }

    protected override bool IsActive(AudioSource component)
    {
        return component.isPlaying;
    }

    public AudioSource GetSource()
    {
        return Get();
    }
}

public class AudioManager : MonoSingleton<AudioManager>
{
    #region CONST
    #endregion

    #region EDITOR PARAMS
    [SerializeField] AudioSource audioPrefab;
    [SerializeField] AudioSourcePool audioSourcePool = null;
    [SerializeField] AudioAsset audioAsset = null;
    [SerializeField] AudioSource soundSource = null;
    [SerializeField] AudioSource musicSource = null;

    [SerializeField] bool isMusicOn = true;
    [SerializeField] bool isSoundOn = true;
    #endregion

    #region PARAMS    
    private bool isInit = false;
    #endregion

    #region PROPERTIES
    #endregion

    #region EVENTS
    #endregion

    #region METHODS
    #endregion

    #region DEBUG
    #endregion

    protected override void Initiate()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void OnInit()
    {
        if (!isInit)
        {
            isInit = true;

            audioSourcePool = new AudioSourcePool(audioPrefab);
            audioAsset?.InitDic();
        }
    }

    public AudioSource GetSource()
    {
        return audioSourcePool.GetSource();
    }

    public AudioClip GetClip(AudioType audioType)
    {
        return audioAsset.GetClip(audioType);
    }

    public void PlayMusic(AudioType audioType, float volume = .85f, bool isLoop = true)
    {
        if (!isInit)
            return;

        AudioClip audioClip = GetClip(audioType);
        if (audioClip != null)
        {
            musicSource.volume = volume;
            musicSource.loop = isLoop;
            musicSource.clip = audioClip;
            musicSource.mute = !isMusicOn;
            musicSource.Play();
        }
        else
        {
            musicSource.Stop();
            Debug.LogErrorFormat("Miss AudioType :{0}", audioType);
        }
    }

    public void PauseMusic()
    {
        musicSource.mute = true;
        // KoreoManager.Instance.SetMuteKoreo(true);
    }

    public void UnPauseMusic()
    {
        musicSource.mute = false;
        // KoreoManager.Instance.SetMuteKoreo(false);
    }

    public void PlaySound(AudioType type)
    {
        if (!isInit)
            return;

        if (!isSoundOn)
            return;
        try
        {
            PlaySound(GetClip(type));
        }
        catch (System.Exception ex)
        {
            ex.Log();
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (!isInit)
            return;

        if (!isSoundOn || clip == null)
            return;
        try
        {
            soundSource = GetSource();
            soundSource.transform.SetParent(transform);
            soundSource.clip = clip;
            soundSource.loop = false;
            soundSource.Play();
        }
        catch (System.Exception ex)
        {
            ex.Log();
        }

    }

    public void PlayOnceShot(AudioType type)
    {
        if (!isInit)
            return;

        if (!isSoundOn)
            return;
        try
        {
            if (soundSource == null)
                soundSource = gameObject.AddComponent<AudioSource>();
            AudioClip clip = GetClip(type);
            if (clip != null)
                soundSource.PlayOneShot(clip);
        }
        catch (System.Exception ex)
        {
            ex.Log();
        }
    }

    public void PauseSound()
    {
        soundSource?.Pause();
    }

    public void UnPauseSound()
    {
        soundSource?.UnPause();
    }

    public void SetMusic(bool isOn)
    {
        isMusicOn = isOn;
        if (isMusicOn)
        {
            UnPauseMusic();
        }
        else
        {
            PauseMusic();
        }
    }

    public void SetSound(bool isOn)
    {
        isSoundOn = isOn;
        if (isSoundOn)
        {
            UnPauseSound();
        }
        else
        {
            PauseSound();
        }
    }

    public void PlaySound(AudioSource audio, AudioClip clip)
    {
        if(isSoundOn)
        {
            audio.clip = clip;
            audio.Play();
        }
    }
}
