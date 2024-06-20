namespace CrudDataApplication.Interfaces
{
    public interface ILoggerRepository<T> where T : class
    {
        void InfoMessage(string message);
        void InfoWithObjectMessage(object objectMessage);
        void ErrorMessage(object errorMessage);
    }
}
