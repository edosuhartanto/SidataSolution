namespace Sidata.Abstractions.DataContext.Interfaces
{
    /// <summary>
    /// interface untuk mengirimkan nama user yang mendapat hak akses
    /// ke DbContext. Turunan DbContext harus mengirimkan satu parameter
    /// constructor dengan type ini.
    /// </summary>
    /// <remarks>
    /// kirimkan lewat DI akan lebih baik dan clean.
    /// </remarks>
    public interface IContextUser
    {
        public string UserName { get; }
    }
}
