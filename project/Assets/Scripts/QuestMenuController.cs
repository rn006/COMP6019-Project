using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestMenuController : MonoBehaviour
{
    public Canvas StoryCanvas;
    public Canvas ChallengesCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showStory()
    {
        StoryCanvas.GetComponent<Canvas>().enabled = true;
        ChallengesCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void showChallenges()
    {
        ChallengesCanvas.GetComponent<Canvas>().enabled = true;
        StoryCanvas.GetComponent<Canvas>().enabled = false;
    }
}
