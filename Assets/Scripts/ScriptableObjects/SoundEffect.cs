using System;
using MyBox;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "NewSoundEffect", menuName = "Audio/New Sound Effect")]
public class SoundEffect : ScriptableObject
{
    enum PlayOrder
    {
        Random,
        InOrder,
        Reverse
    }
    
    // config
    public AudioClip[] audioClips;
    [MinValue(0.0f)]
    [MaxValue(1.0f)]
    public Vector2 volume = new Vector2(0.5f, 0.5f);
    [MinValue(0.0f)]
    [MaxValue(3.0f)]
    public Vector2 pitch = new Vector2(1.0f, 1.0f);
    
    [SerializeField] private int playIndex = 0;
    [SerializeField] private PlayOrder playOrder;

#if UNITY_EDITOR
    #region preview code

    private AudioSource previewSource;

    private void OnEnable()
    {
        previewSource = EditorUtility
            .CreateGameObjectWithHideFlags("AudioPreview", HideFlags.HideAndDontSave, typeof(AudioSource))
            .GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        DestroyImmediate(previewSource.gameObject);
    }
    
    [ButtonMethod]
    private void PlayPreview()
    {
        Play(previewSource);
    }

    [ButtonMethod]
    private void StopPreview()
    {
        previewSource.Stop();
    }

    #endregion
#endif

    private AudioClip GetAudioClip()
    {
        // get current clip
        var clip = audioClips[playIndex >= audioClips.Length ? 0 : playIndex];
        
        // find next clip
        switch (playOrder)
        {
            case PlayOrder.Random:
                playIndex = Random.Range(0, audioClips.Length);
                break;
            case PlayOrder.InOrder:
                playIndex = (playIndex + 1) % audioClips.Length;
                break;
            case PlayOrder.Reverse:
                playIndex = (playIndex - 1 + audioClips.Length) % audioClips.Length;
                break;
            default:
                break;
        }
        
        // return clip
        return clip;
    }
    
    public AudioSource Play(AudioSource audioSource = null)
    {
        if (audioClips.Length == 0)
        {
            Debug.LogWarning(this.name + ": No audio clips found!");
            return null;
        }

        var source = audioSource;
        if (source == null)
        {
            var obj = new GameObject("Audio Source", typeof(AudioSource));
            source = obj.GetComponent<AudioSource>();
        }
        
        // set source config
        source.clip = GetAudioClip();
        source.volume = Random.Range(volume.x, volume.y);
        source.pitch = Random.Range(pitch.x, pitch.y);
        
        source.Play();

#if UNITY_EDITOR
        if (source != previewSource)
        {
            Destroy(source.gameObject, source.clip.length/source.pitch);
        }
#else
        Destroy(source.gameObject, source.clip.length/source.pitch);
#endif

        return source;
    }
}
