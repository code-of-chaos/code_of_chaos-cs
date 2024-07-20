// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Lucide.Generators.Helpers;
using System.Text;

namespace CodeOfChaos.Lucide.Generators.Templates;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------

public class LucideData (IEnumerable<(string Name, string? svg)> iconsArray, string? licenseContent) : ITemplate {
    public IEnumerable<(string Name, string? svg)> IconsArray { get; } = iconsArray.ToArray();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public string Assemble() {
        StringBuilder sb = new StringBuilder()
            .AppendCopyrightLucide()
            .AppendImports()
            .AppendCodeHeader()
            .AppendLine("public static class LucideData {")
            .AppendLine($"    public const int AmountOfIcons = {IconsArray.Count()};")
            .AppendLine($"    public static string LucideLicence = \"\"\"\n{licenseContent?.Replace("\"\"\"", "\"\"\"\"\"\"")}\"\"\";")
        ;
        
        sb.AppendLine("}");
        
        return sb.ToString();
    }
}
