// ---------------------------------------------------------------------------------------------------------------------
// Copyright (c) for portions of Lucide are held by Cole Bemis 2013-2022 as part of Feather (MIT).
// All other copyright (c) for Lucide are held by Lucide Contributors 2022.
// ---------------------------------------------------------------------------------------------------------------------

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text;
using System.Xml.Linq;

namespace CodeOfChaos.Lucide.Generators;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class IconsSetTemplate {
    public static string AssembleFileTemplate(IEnumerable<(string Name, string? svg)> iconsArray) {
        StringBuilder sb = new StringBuilder()
            .AppendCopyrightLucide()
        
            .AppendLine("using Microsoft.AspNetCore.Components;")
            .AppendLine("using System.Diagnostics.CodeAnalysis;")
            .AppendNamespace()
        
            .AppendLine("public static class IconSet {")
            .AppendLine("    public static IReadOnlyDictionary<Icons, MarkupString> IconAtlas = new Dictionary<Icons, MarkupString>() {");

        foreach ((string name, string? svg) in iconsArray) {
            // Add individual key value pairs to the StringBuilder
            sb.AppendLine($$"""        { Icons.{{IconsTemplate.GetIconsEnumName(name)}}, new MarkupString({{"\"\"\""}}{{Extract(svg!)}}{{"\"\"\""}}) },""");
        }
        sb.AppendLine("    };");
        
        sb.AppendLine("    public static bool TryFromLucideString(string name, [NotNullWhen(true)] MarkupString? output){")
            .AppendLine("        output = null;")
            .AppendLine("        string newName = string.Join(\"\", name.Split(\"-\").Select(part => char.ToUpper(part[0]) + part[1..]));")
            .AppendLine("        bool result = Enum.TryParse<Icons>(\"\", out Icons icon);")
            .AppendLine("        if (!result) return false;")
            .AppendLine("        output = IconAtlas[icon];")
            .AppendLine("        return result;")
            .AppendLine("    }");
        
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
