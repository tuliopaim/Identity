namespace Identity.Business.Response;

public class ResponseBase
{
    public List<string> Erros { get; set; } = new List<string>();

    public bool Valido => !Erros.Any();

    public void AddErrors(params string[] errors)
    {
        Erros.AddRange(errors);
    }

    public static ResponseBase Error(params string[] errors)
    {
        return new ResponseBase
        {
            Erros = errors.ToList(),
        };
    }
}
