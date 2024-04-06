using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadlinesManager : MonoBehaviour
{
    [System.Serializable]
    public class Headline
    {
        public string Id;
        public string HeadLine;
        public int BanChanceThreshold;
    }

    [System.Serializable]
    public class HeadlinesCategory
    {
        public List<Headline> HealthHeadlines;
        public List<Headline> CelebrityHeadlines;
        public List<Headline> EnvironmentHeadlines;
    }

    public List<Headline> healthHeadlines = new List<Headline>();
    public List<Headline> celebrityHeadlines = new List<Headline>();
    public List<Headline> environmentHeadlines = new List<Headline>();

    private string headlinesJsonLocation = "/Data/headlines.json"; // Update the path as necessary

    // Start is called before the first frame update
    void Start()
    {
        // Read the JSON data from the file
        string json = System.IO.File.ReadAllText(Application.dataPath + headlinesJsonLocation);

        // Deserialize the JSON string into the HeadlinesCategory object
        HeadlinesCategory headlinesCategory = JsonUtility.FromJson<HeadlinesCategory>(json);

        // Directly assign the deserialized headlines to the lists
        healthHeadlines = headlinesCategory.HealthHeadlines;
        celebrityHeadlines = headlinesCategory.CelebrityHeadlines;
        environmentHeadlines = headlinesCategory.EnvironmentHeadlines;

        // Example: Print each health headline to the console
        foreach (Headline headline in healthHeadlines)
        {
            Debug.Log("Health Headline ID: " + headline.Id + ", Headline: " + headline.HeadLine + ", BanChanceThreshold: " + headline.BanChanceThreshold);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
