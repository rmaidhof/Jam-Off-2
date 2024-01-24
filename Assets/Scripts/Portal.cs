using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string sceneName;
    public float delay = 0f;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(LoadScene());
    }

    public IEnumerator LoadScene()
    {
        Debug.Log("called loadscene coroutine");
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
        yield return null;
    }
}
