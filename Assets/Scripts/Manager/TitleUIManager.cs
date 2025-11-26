using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField] private string _gameSceneName = "GameScene";
    [SerializeField] private Image _fadeImage;
    [SerializeField] private float _fadeDuration = 1.5f;

    private bool _isLoading = false;

    private void Start()
    {
        if (_fadeImage != null)
        {
            Color c = _fadeImage.color;
            c.a = 0f;
            _fadeImage.color = c;
        }
    }

    public void OnClickStart()
    {
        if(_isLoading) // 중복 클릭 방지
        {
            return;
        }
        StartCoroutine(FadeAndLoad());
    }

    private IEnumerator FadeAndLoad()
    {
        _isLoading = true;

        float t = 0f;
        Color c = _fadeImage.color;

        while(t < _fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Clamp01(t / _fadeDuration);

            c.a = alpha;
            _fadeImage.color = c;

            yield return null;
        }
        SceneManager.LoadScene(_gameSceneName);
    }
}
