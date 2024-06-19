// ---------------------------------------------------------------------------------------------------------------------
// Copyright (c) for portions of Lucide are held by Cole Bemis 2013-2022 as part of Feather (MIT). All other copyright (c) for Lucide are held by Lucide Contributors 2022.
//
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
        var sb = new StringBuilder();
        sb.AppendCopyrightLucide();
        
        sb.AppendLine("using Microsoft.AspNetCore.Components;");
        sb.AppendLine("using System.Diagnostics.CodeAnalysis;");
        sb.AppendNamespace();
        
        sb.AppendLine("public static class IconSet {");
        sb.AppendLine("    public static IReadOnlyDictionary<Icons, MarkupString> IconAtlas = new Dictionary<Icons, MarkupString>() {");

        foreach ((string name, string? svg) in iconsArray) {
            // Add individual key value pairs to the StringBuilder
            sb.AppendLine($$"""        { Icons.{{IconsTemplate.GetIconsEnumName(name)}}, new MarkupString({{"\"\"\""}}{{Extract(svg!)}}{{"\"\"\""}}) },""");
        }
        sb.AppendLine("    };");

        sb.AppendLine("    public static bool TryFromLucideString(string name, [NotNullWhen(true)] MarkupString? output){");
        sb.AppendLine("        output = null;");
        sb.AppendLine("        string newName = string.Join(\"\", name.Split(\"-\").Select(part => char.ToUpper(part[0]) + part[1..]));");
        sb.AppendLine("        bool result = Enum.TryParse<Icons>(\"\", out Icons icon);");
        sb.AppendLine("        if (!result) return false;");
        sb.AppendLine("        output = IconAtlas[icon];");
        sb.AppendLine("        return result;");
        sb.AppendLine("    }");
        
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
