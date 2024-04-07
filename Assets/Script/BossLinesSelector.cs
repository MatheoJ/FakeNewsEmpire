using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLinesSelector : MonoBehaviour
{
    public List<BossLine> bossLines = new List<BossLine>();
    public Dictionary<string, List<int>> lineIndexesByCategory = new Dictionary<string, List<int>>();

    private string linesJsonLocation = "/Data/bossLines.json"; // Adjust the path as needed

    // Start is called before the first frame update
    void Awake()
    {
        // Read the JSON data from the file
        string json = System.IO.File.ReadAllText(Application.dataPath + linesJsonLocation);

        // Deserialize the JSON string into the BossLinesContainer object
        BossLinesContainer linesContainer = JsonUtility.FromJson<BossLinesContainer>(json);

        // Assign the deserialized lines directly to the list
        bossLines = linesContainer.BossLines;

        // Fill the dictionary with line indexes for each category
        foreach (var line in bossLines)
        {
            if (!lineIndexesByCategory.ContainsKey(line.Category))
            {
                lineIndexesByCategory[line.Category] = new List<int>();
            }
            lineIndexesByCategory[line.Category].Add(bossLines.IndexOf(line));
        }

        // Debug output
        Debug.Log("Boss lines loaded and categorized.");

        // Example usage
        List<BossLine> linesForTheGame = GetBossLinesForTheGame();
        foreach (BossLine line in linesForTheGame)
        {
            Debug.Log("Line ID: " + line.Id + ", Category: " + line.Category + ", Line Order: " + line.LineOrderInCategory + ", Line: " + line.Line);
        }

    }

    public List<BossLine> GetLinesByCategory(string category)
    {
        List<BossLine> linesInCategory = new List<BossLine>();
        if (lineIndexesByCategory.ContainsKey(category))
        {
            foreach (int index in lineIndexesByCategory[category])
            {
                linesInCategory.Add(bossLines[index]);
            }
        }
        return linesInCategory;
    }


    public List<BossLine> GetBossLinesForTheGame() { 
        //Return 1 or 2 lines for each category
        List<BossLine> bossLines = new List<BossLine>();
        List<string> categories = new List<string>(lineIndexesByCategory.Keys);
        foreach (string category in categories)
        {
            List<BossLine> linesInCategory = GetLinesByCategory(category);
            if (linesInCategory.Count > 2)
            {
                bossLines.Add(linesInCategory[Random.Range(0, linesInCategory.Count)]);
                // add a second line different from the first one
                BossLine secondLine = linesInCategory[Random.Range(0, linesInCategory.Count)];
                while (secondLine.Id == bossLines[bossLines.Count - 1].Id)
                {
                    secondLine = linesInCategory[Random.Range(0, linesInCategory.Count)];
                }
                bossLines.Add(secondLine);
            }
        }
        return bossLines; 
    }
}

[System.Serializable]
public struct BossLine
{
    public string Id;
    public string Category;
    public int LineOrderInCategory;
    public string Line;
}

[System.Serializable]
public struct BossLinesContainer
{
    public List<BossLine> BossLines;
}
