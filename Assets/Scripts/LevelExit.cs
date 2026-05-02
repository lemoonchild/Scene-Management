using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public string nextSceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadingManager.GetOrCreate().LoadScene(nextSceneName);
        }
    }
}