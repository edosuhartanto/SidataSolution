
// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto 
// ******************************************************

using Sidata.Abstractions.WebApi.Enums;

namespace Sidata.Abstractions.WebApi.Extensions
{
    public static class ControllerObjectIdExtension
    {
        /// <summary>
        /// memabngun kode error utk identifikasi object yang menyebabkan kesalahan timbul.<br/>
        /// scopeid = code for project,
        /// id = code for controller,
        /// sequencenumber = code for endpoint
        /// </summary>
        public static int Builder(int id, BaseStatementId sequencenumber)
        {
            return Builder(id, (int)sequencenumber);
        }

        public static int Builder(int id, int sequencenumber)
        {
            return (id * 100) + sequencenumber;
        }

    }
}
