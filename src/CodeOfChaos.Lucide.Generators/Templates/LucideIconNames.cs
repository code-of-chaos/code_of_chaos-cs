// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Lucide.Generators.Helpers;
using System.Text;

namespace CodeOfChaos.Lucide.Generators.Templates;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class LucideIconNames(IEnumerable<(string Name, string? svg)> iconsArray) : ITemplate {
    public IEnumerable<(string Name, string? svg)> IconsArray { get; } = iconsArray.ToArray();
    
    public string Assemble() {
        StringBuilder sb = new StringBuilder()
            .AppendCopyrightLucide()
            .AppendImports()
            .AppendCodeHeader()
            .AppendLine("public static class LucideIconNames {");

        foreach ((string name, _) in IconsArray) {
            // Add individual key value pairs to the StringBuilder
            sb.AppendLine($"    public const string {IconTranslator.GetIconsCSharpName(name)} = \"{name}\";");
        }
        
        sb.AppendLine("}");
        
        return sb.ToString();
    } 
}