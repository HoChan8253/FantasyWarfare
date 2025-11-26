using UnityEngine;

public class SceneBGMPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _bgmClip;

    private void Start()
    {
        if(SoundManager._instance != null && _bgmClip != null)
        {
            SoundManager._instance.PlayBGM(_bgmClip);
        }
    }
}
