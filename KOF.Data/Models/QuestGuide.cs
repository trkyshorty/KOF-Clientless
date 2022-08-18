namespace KOF.Data.Models;

public class QuestGuide
{
    public int Id { get; protected set; } // 0
    public int MinLevel { get; protected set; } // 1
    public string Title { get; protected set; } // 3
    public string Description { get; protected set; } // 4

    public QuestGuide(int id, int minLevel, string title, string description)
    {
        Id = id;
        MinLevel = minLevel;
        Title = title;
        Description = description;
    }
}
