using System.Collections.Generic;

[System.Serializable]

public class ChapterClearData
{
    public List<bool> chapterCleared;

    public ChapterClearData(int chapterCount)
    {
        chapterCleared = new List<bool>(new bool[chapterCount]);
    }
}
