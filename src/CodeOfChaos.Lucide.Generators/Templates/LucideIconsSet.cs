// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Lucide.Generators.Helpers;
using System.Text;
using System.Xml.Linq;

namespace CodeOfChaos.Lucide.Generators.Templates;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class LucideIconsSet(IEnumerable<(string Name, string? svg)> iconsArray) : ITemplate {
    public IEnumerable<(string Name, string? svg)> IconsArray { get; } = iconsArray;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public string Assemble() {
        StringBuilder sb = new StringBuilder()
            .AppendCopyrightLucide()
            .AppendImports(["Microsoft.AspNetCore.Components","System.Diagnostics.CodeAnalysis"])
            .AppendCodeHeader()
        
            .AppendLine("public static class LucideIconsSet {")
            .AppendLine("    public static IReadOnlyDictionary<string, MarkupString> IconAtlas = new Dictionary<string, MarkupString>() {");

        foreach ((string name, string? svg) in IconsArray) {
            // Add individual key value pairs to the StringBuilder
            sb.AppendLine( "        {");
            sb.AppendLine($"             LucideIconNames.{IconTranslator.GetIconsCSharpName(name)},");
            sb.AppendLine($"             new MarkupString(\"\"\"{Extract(svg!)}\"\"\")");
            sb.AppendLine( "        },");
        }

        sb.AppendLine("    };");
        sb.AppendLine("}");

        return sb.ToString();
    }

    private static string Extract(string value) {
        IEnumerable<string>? elements = XDocument
            .Parse(value)
            .Root?
            .Descendants()
            .Select(element => {
                element.Name = element.Name.LocalName;
                return element.ToString(SaveOptions.DisableFormatting);
            });

        if (elements == null) {
            throw new FileNotFoundException("No Valid elements found");
        }
        
        return string.Concat(elements);
    }
}
