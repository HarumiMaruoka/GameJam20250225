/// <summary>
/// Jsonに保存するためのData
/// </summary>
[System.Serializable]
public class ScoreData
{
    public string _name;
    public float _score;

    public string Name => _name;
    public float Score => _score;

    public ScoreData(string name, float score)
    {
        _name = name;
        _score = score;
    }
}
