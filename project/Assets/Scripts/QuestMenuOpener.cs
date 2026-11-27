using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestMenuOpener : MonoBehaviour
{
    private bool on = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (on == false)
            {
                SceneManager.LoadScene("QuestMenu", LoadSceneMode.Additive);
                on = true;
            }
            else
            {
                SceneManager.UnloadSceneAsync("QuestMenu");
                on = false;
            }
        }
        
    }
}
