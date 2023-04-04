using System.ComponentModel.DataAnnotations.Schema;

namespace MyNotes.Models;

[Serializable]
public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public List<Note> Notes { get; set; } = new();
}