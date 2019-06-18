using System.Collections.Generic;

public class CardInfo
{
    public int Id { get; set; }
    public int Name { get; set; }
    public int Count { get; set; }
    public bool IsLock { get; set; }
    public List<CardProperty> PropertyList { get; set; }
}

public class CardProperty
{
    public int Id { get; set; }
    public int MaxLevel { get; set; }
    public int CurrentLevel { get; set; }
}