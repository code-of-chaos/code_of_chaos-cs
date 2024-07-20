// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text;

namespace CodeOfChaos.Lucide.Generators.Helpers;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------

public static class IconTranslator {
    public static string GetIconsCSharpName(string name) {
        var result = new StringBuilder();
        foreach (string part in name.Split('-')) {
            if (part.Length > 0) {
                result.Append(char.ToUpper(part[0])).Append(part.AsSpan(1));
            }
        }
        return result.ToString();
    }
}
