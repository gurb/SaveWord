using SQLite;

namespace SaveWord.Data;

public class WordService
{
    private readonly SQLiteConnection _db;

    public WordService(string dbName)
    {
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbName);
        _db = new SQLiteConnection(dbPath);
        _db.CreateTable<Word>();
    }

    public List<Word> GetWords() 
    {
        return _db.Table<Word>().ToList();
    }

    // create
    public int AddWord(Word word) 
    {
        return _db.Insert(word);
    }

    // update
    public int EditWord(Word word)
    {
        return _db.Update(word);
    }

    // delete
    public int DeleteWord(Word word) 
    {
        return _db.Delete(word);
    }

	internal void AddWord(object model)
	{
		throw new NotImplementedException();
	}

    public int GetTestedCount() 
    {
        return _db.Query<Word>("SELECT * FROM Word WHERE IsTested IS TRUE").Count;
    }

    public int GetCorrectCount()
    {
        return _db.Query<Word>("SELECT * FROM Word WHERE IsCorrect IS TRUE").Count;
    }

    public List<Word> QueryWordRandUnique(int wordCount) 
    {
        return _db.Query<Word>("SELECT * FROM Word ORDER BY RANDOM() LIMIT " + wordCount);
    }

}