// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto
// ******************************************************
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Text;

namespace Sidata.Tools.GeneratorAndBuilder
{
    public class HttpFileGenerator
    {
        private readonly IApiDescriptionGroupCollectionProvider _provider;

        public HttpFileGenerator(
            IApiDescriptionGroupCollectionProvider provider)
        {
            _provider = provider;
        }

        public string Generate()
        {
            var sb = new StringBuilder();

            foreach (var group in _provider.ApiDescriptionGroups.Items)
            {
                foreach (var api in group.Items)
                {
                    sb.AppendLine($"### {api.ActionDescriptor.DisplayName}");
                    sb.AppendLine(
                        $"{api.HttpMethod} {{host}}/{api.RelativePath}");
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }
    }
}