namespace MyNotes.Models.ResponseClasses;

public class ResponseClass<T>
{
    
    public T response { get; set; }

    public ResponseClass(T _response)
    {
        response = _response;
    }

}