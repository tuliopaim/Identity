using System.Text.Json.Serialization;

namespace Identity.Business.Response;

public class ResponseBase
{
    public List<string> Erros { get; set; } = new List<string>();

    [JsonIgnore]
    public bool Valido => !Erros.Any();

    public virtual ResponseBase AddErrors(params string[] errors)
    {
        Erros.AddRange(errors);

        return this;
    }

    public static ResponseBase Error(params string[] errors)
    {
        return new ResponseBase
        {
            Erros = errors.ToList(),
        };
    }
}
