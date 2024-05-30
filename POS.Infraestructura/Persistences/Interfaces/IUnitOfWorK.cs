namespace POS.Infraestructura.Persistences.Interfaces
{
    public interface IUnitOfWorK : IDisposable
    {
        //declaración o matricula de nuestra de nuestra interfaces a nivel repository
        ICategoryRepository Category { get; }
        void SaveChanges();
        Task SaveChangesAsync();


    }
}
