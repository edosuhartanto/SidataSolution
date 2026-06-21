namespace Sidata.Abstractions.DataContext.Interfaces
{
    /// <summary>
    /// interface untuk mengirimkan nama user yang mendapat hak akses
    /// ke DbContext. Turunan DbContext harus mengirimkan satu parameter
    /// constructor dengan type ini.
    /// </summary>
    public interface IContextUser
    {
        public string UserName { get; }
    }
}
