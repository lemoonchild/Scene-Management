using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance;

    private void Start()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public static LoadingManager GetOrCreate()
    {
        if (Instance == null)
        {
            GameObject go = new GameObject("LoadingManager");
            go.AddComponent<LoadingManager>();
        }
        return Instance;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAsync(sceneName));
    }

    private IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation loadingScene = SceneManager.LoadSceneAsync("LoadingScreen", LoadSceneMode.Additive);
        yield return loadingScene;

        LoadingScreenUI loadingUI = FindFirstObjectByType<LoadingScreenUI>();

        AsyncOperation targetScene = SceneManager.LoadSceneAsync(sceneName);
        targetScene.allowSceneActivation = false;

        float minTime = 5f;
        float elapsed = 0f;

        while (targetScene.progress < 0.9f || elapsed < minTime)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        targetScene.allowSceneActivation = true;
        yield return new WaitForSeconds(0.1f);
        SceneManager.UnloadSceneAsync("LoadingScreen");
    }
}