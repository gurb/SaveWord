using SQLite;

namespace SaveWord.Data;

public class Word
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string WordText { get; set; } = string.Empty;
    public string MeaningText { get; set; } = string.Empty;
    public string SentenceText { get; set; } = string.Empty;
    public bool IsCorrect { get; set; } = false;
    public bool IsTested { get; set; } = false;
    public int rowid { get; set; }

    [Ignore]
    public bool EditState { get; set; } = false;
}
