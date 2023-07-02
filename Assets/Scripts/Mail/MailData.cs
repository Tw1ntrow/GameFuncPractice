
public class MailData
{
    public int id;
    public string body;
    public string title;
    public string itemId;
    public int itemCount;

    public MailData(int id, string body, string title, string itemId, int itemCount)
    {
        this.id = id;
        this.body = body;
        this.title = title;
        this.itemId = itemId;
        this.itemCount = itemCount;
    }
}
