public class JiraIssue
{
    public string Key { get; set; }
    public Fields Fields { get; set; }
}

public class Fields
{
    public string Summary { get; set; }
    public string AcceptanceCriteria { get; set; }
    public int StoryPoints { get; set; }
}