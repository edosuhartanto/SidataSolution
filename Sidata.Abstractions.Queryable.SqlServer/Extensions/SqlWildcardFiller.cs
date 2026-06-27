// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto
// ******************************************************
using Sidata.Abstractions.Queryable.SqlServer.Enums;
using System.Text.Json;

namespace Sidata.Abstractions.Queryable.SqlServer.Extensions
{
    public static class SqlWildcardFiller
    {
        /// <summary>
        /// wildcard auto filler with  following rule:<br/>
        /// added prefix % => if options enable on FillWildcardOptions.PrefixOnly<br/>
        /// added suffix % => if options enable on FillWildcardOptions.SuffixOnly<br/>
        /// space (" ") => will become "%"<br/>
        /// double quote ("\"") => will become " " (space)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string? FillWildcard(this string? input, FillWildcardOptions options)
        {
            if (string.IsNullOrEmpty(input)) return "%";

            ReadOnlySpan<char> source = input.AsSpan();

            // Hitung panjang tambahan
            int extra = options switch
            {
                FillWildcardOptions.PrefixOnly => 1,
                FillWildcardOptions.SuffixOnly => 1,
                FillWildcardOptions.Both => 2,
                _ => 0
            };
            Span<char> buffer = stackalloc char[source.Length + extra];
            int index = 0;

            // Prefix
            if (options == FillWildcardOptions.PrefixOnly || options == FillWildcardOptions.Both)
            {
                buffer[index++] = '%';
            }

            // Transformasi karakter
            for (int i = 0; i < source.Length; i++)
            {
                buffer[index++] = source[i] switch
                {
                    ' ' => '%',
                    '"' => ' ',
                    _ => source[i]
                };
            }

            // Suffix
            if (options == FillWildcardOptions.SuffixOnly || options == FillWildcardOptions.Both)
            {
                buffer[index++] = '%';
            }

            return new string(buffer[..index]);
        }
    }
}