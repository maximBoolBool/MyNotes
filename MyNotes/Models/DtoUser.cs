namespace MyNotes.Models;

public class DtoUser
{
    public DtoUser()
    {
        
    }
    
    public DtoUser(string login,string password)
    {
        Login = login;
        Password = password;
    }



    public string Login { get; set; }
    public string Password { get; set; }
}