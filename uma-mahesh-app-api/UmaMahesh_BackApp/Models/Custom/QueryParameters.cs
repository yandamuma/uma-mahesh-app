namespace UmaMahesh_BackApp.Models.Custom;

public class QueryParameters
{
    private readonly int _maxSize = 100;

    private int _size = 10;
    public int Page { get; set; } = 1;
    public int Size
    {
        get
        { return _size; }
        set
        {
            _size = Math.Min(value, _maxSize);
        }
    }

    public string SortBy { get; set; } = string.Empty;

    private string _sortOrder = "asc";
    public string SortOrder
    {
        get
        {
            return _sortOrder;
        }
        set
        {
            if (value == "asc" || value == "desc")
                _sortOrder = value;
        }
    }
}
