namespace CrudDataApplication.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string userName);
    }
}
