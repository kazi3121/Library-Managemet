public class AuditLog
{
    public int Id {get; set;}
    public string EntityName {get; set;}
    public string Action {get; set;}
    public DateTime OccuredAt {get;set;}
}