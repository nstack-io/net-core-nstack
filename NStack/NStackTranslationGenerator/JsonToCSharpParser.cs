using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace NStackTranslationGenerator
{
    public static class JsonToCSharpParser
    {
        public static IDictionary<string, string> ParseResourceItem(JObject resourceItemObject, string resourceItemName, string targetNamespace)
        {
            var dictionairy = new Dictionary<string, string>();

            var sb = new StringBuilder();
            sb.AppendLine(Includes);

            sb.AppendLine("");

            sb.AppendLine($"namespace {targetNamespace}");
            sb.AppendLine("{");
            sb.AppendLine($"{AddTabs(1)}public class {resourceItemName} : ResourceItem");
            sb.AppendLine($"{AddTabs(1)}{{");
            sb.AppendLine($"{AddTabs(2)}public {resourceItemName}() : base() {{ }}");
            sb.AppendLine($"{AddTabs(2)}public {resourceItemName}(ResourceItem item) : base(item) {{ }}");
            sb.AppendLine("");


            foreach (var resourceItem in resourceItemObject)
            {

            }

            sb.AppendLine($"{AddTabs(1)}}}");
            sb.AppendLine("}");

            dictionairy.Add(resourceItemName, sb.ToString());

            return dictionairy;
        }

        private static string AddTabs(int tabs)
        {
            const int tabSize = 4;
            var sb = new StringBuilder();

            for(int i = 0; i < tabs*tabSize; ++i)
            {
                sb.Append(" ");
            }

            return sb.ToString();
        }

        private static string Includes = $"using NStack.Extensions;{Environment.NewLine}using NStack.Models;";
    }
}
