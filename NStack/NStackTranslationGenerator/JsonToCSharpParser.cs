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
            var dictionary = new Dictionary<string, string>();

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


            foreach (KeyValuePair<string, JToken> resourceInnerItem in resourceItemObject)
            {
                string name = $"{char.ToUpper(resourceInnerItem.Key[0])}{resourceInnerItem.Key.Substring(1)}";
                sb.AppendLine($"{AddTabs(2)}public {name}Section {name} => new {name}Section(this[nameof({name}).FirstCharToLower()]);");

                ParseResourceInnerItem(dictionary, resourceInnerItem.Value, targetNamespace);
            }

            sb.AppendLine($"{AddTabs(1)}}}");
            sb.AppendLine("}");

            dictionary.Add(resourceItemName, sb.ToString());

            return dictionary;
        }

        private static void ParseResourceInnerItem(IDictionary<string, string> dictionary, JToken innerItem, string targetNameSpace)
        {
            string sectionName = $"{char.ToUpper(innerItem.Path[0])}{innerItem.Path.Substring(1)}";

            var sb = new StringBuilder();
            sb.AppendLine(Includes);
            sb.AppendLine("");

            sb.AppendLine($"namespace {targetNameSpace}");
            sb.AppendLine("{");
            sb.AppendLine($"{AddTabs(1)}public class {sectionName}Section : ResourceInnerItem");
            sb.AppendLine($"{AddTabs(1)}{{");
            sb.AppendLine($"{AddTabs(2)}public {sectionName}Section() : base() {{ }}");
            sb.AppendLine($"{AddTabs(2)}public {sectionName}Section(ResourceInnerItem item) : base(item) {{ }}");
            sb.AppendLine("");

            foreach (JProperty entry in innerItem)
            {
                string name = $"{char.ToUpper(entry.Name[0])}{entry.Name.Substring(1)}";
                sb.AppendLine($"{AddTabs(2)}public string {name} => this[nameof({name}).FirstCharToLower()];");
            }

            sb.AppendLine($"{AddTabs(1)}}}");
            sb.AppendLine("}");

            dictionary.Add($"{sectionName}Section.cs", sb.ToString());
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

        private static string Includes = $"using NStack.SDK.Extensions;{Environment.NewLine}using NStack.SDK.Models;";
    }
}
