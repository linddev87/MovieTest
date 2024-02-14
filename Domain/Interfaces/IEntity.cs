namespace Domain.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }
        string AlternateKey {get; set;}
    }
}
