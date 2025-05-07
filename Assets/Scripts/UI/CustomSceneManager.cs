using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomSceneManager : MonoBehaviourSingleton<CustomSceneManager>
{
    [SerializeField] private Image image;
    [SerializeField] private float maxTime = 10f;

    private IEnumerator loadingScene;

    protected override void OnAwaken()
    {
        if(gameObject.activeSelf)
        gameObject.SetActive(false);
    }

    public void ChangeSceneTo(string sceneName)
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        if (loadingScene == null)
        {
            loadingScene = LoadingScene(sceneName);
            StartCoroutine(loadingScene);
        }
    }

    private IEnumerator LoadingScene(string sceneName)
    { 
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        operation.completed += Operation_Completed;
        float onTime = 0;
        float percentage = 0.9f;

        while (onTime < maxTime * percentage)
        {
            onTime += Time.deltaTime;

            image.fillAmount = onTime / maxTime;

            yield return null;
        }

        while (operation.progress < 0.9f)
        {
            yield return null;
        }

        while (onTime < maxTime)
        {
            onTime += Time.deltaTime * 10;

            image.fillAmount = onTime / maxTime;

            yield return null;
        }

        gameObject.SetActive(false);
        operation.allowSceneActivation = true;

        yield return null;
        operation.completed -= Operation_Completed;
        loadingScene = null;
    }

    private void Operation_Completed(AsyncOperation operation)
    {
        Debug.Log("ESCENA CARGADA");
    }

    public void ReloadSceneImmediately()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
