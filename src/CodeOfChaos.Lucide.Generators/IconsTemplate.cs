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
public static class IconsTemplate {
    public static string AssembleFileTemplate(IEnumerable<(string Name, string? svg)> iconsArray) {
        var sb = new StringBuilder();
        sb.AppendCopyrightLucide();
         
        sb.AppendNamespace();

        sb.AppendLine("public enum Icons : ulong {");

        foreach ((string name, _) in iconsArray) {
            // Add individual key value pairs to the StringBuilder
            sb.AppendLine($"    {GetIconsEnumName(name)},");
        }
        
        sb.AppendLine("}");
        
        return sb.ToString();
    } 
    
    public static string GetIconsEnumName(string name) {
        var result = new StringBuilder();
        foreach (string part in name.Split('-')) {
            if (part.Length > 0) {
                result.Append(char.ToUpper(part[0])).Append(part.AsSpan(1));
            }
        }
        return result.ToString();
    }
}