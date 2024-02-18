namespace GeneralNotes.Communication.Responses;
public class ResponseErrorJson
{
    public List<string> Mensages { get; set; }
    public ResponseErrorJson(string mensage)
    {
        Mensages =
        [
            mensage
        ];

    }

    public ResponseErrorJson(List<string> mensages)
    {
        Mensages = mensages;
    }
}
