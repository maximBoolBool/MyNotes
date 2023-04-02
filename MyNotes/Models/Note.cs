using System.ComponentModel.DataAnnotations.Schema;

namespace MyNotes.Models;

[Serializable]
public class Note
{
    public int Id { get; set; }
    public string Head { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }
    public User Owner { get; set; }
}