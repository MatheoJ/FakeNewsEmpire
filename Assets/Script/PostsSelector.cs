using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostManager : MonoBehaviour
{
    [System.Serializable]
    public class Post
    {
        public int Id;
        public string Title;
        public string HeadLine;
        public List<string> Categories;
        public Stats Stats;
    }

    [System.Serializable]
    public class Stats
    {
        public int BanChances;
        public int Money;
        public int Views;
        public int Members;
    }

    [System.Serializable]
    public class PostsContainer
    {
        public List<Post> Posts;
    }

    public List<Post> posts = new List<Post>();
    public List<int> healthPostIndex = new List<int>();
    public List<int> celebrityPostIndex = new List<int>();
    public List<int> environmentPostIndex = new List<int>();

    private string postsJsonLocation = "/Data/posttemplate.json"; // Adjust the path as needed

    // Start is called before the first frame update
    void Start()
    {
        // Read the JSON data from the file
        string json = System.IO.File.ReadAllText(Application.dataPath + postsJsonLocation);

        // Deserialize the JSON string into the PostsContainer object
        PostsContainer postsContainer = JsonUtility.FromJson<PostsContainer>(json);

        // Assign the deserialized posts directly to the list
        posts = postsContainer.Posts;

        // Fill the index lists for each category
        for (int i = 0; i < posts.Count; i++)
        {
            foreach (string category in posts[i].Categories)
            {
                if (category == "Health")
                {
                    healthPostIndex.Add(i);
                }
                else if (category == "Celebrity")
                {
                    celebrityPostIndex.Add(i);
                }
                else if (category == "Environment")
                {
                    environmentPostIndex.Add(i);
                }
            }
        }

        // Example usage
        foreach (Post post in posts)
        {
            Debug.Log("Post ID: " + post.Id + ", Title: " + post.Title + "Headline:"+ post.HeadLine + ", Categories: " + string.Join(", ", post.Categories));
        }
    }

}
