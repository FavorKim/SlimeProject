using System.Collections.Generic;

[System.Serializable]

public class ChapterClearData
{
    public List<bool> chapterCleared;
    public int selectedChapter;

    public ChapterClearData(int chapterCount)
    {
        chapterCleared = new List<bool>(new bool[chapterCount]);
        selectedChapter = 0;
    }
}

[System.Serializable]
public class StageClearData
{
    public List<bool> stageCleared;

    public StageClearData(int stageCount)
    {
        stageCleared = new List<bool>(new bool[stageCount]);
    }
}
