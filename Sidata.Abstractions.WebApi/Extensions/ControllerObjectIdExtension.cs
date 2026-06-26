using Sidata.Abstractions.WebApi.Enums;

namespace Sidata.Abstractions.WebApi.Extensions
{
    public static class ControllerObjectIdExtension
    {
        /// <summary>
        /// memabngun kode error utk identifikasi object yang menyebabkan kesalahan timbul.
        /// </summary>
        public static int Builder(int id, BaseStatementId sequencenumber)
        {
            return (id * 100) + (int)sequencenumber;
        }


    }
}
