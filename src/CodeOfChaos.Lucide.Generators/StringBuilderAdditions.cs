// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text;

namespace CodeOfChaos.Lucide.Generators;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class StringBuilderAdditions {
    public static StringBuilder AppendCopyrightLucide(this StringBuilder sb) {
        sb.AppendLine("// ---------------------------------------------------------------------------------------------------------------------");
        sb.AppendLine("// Copyright (c) for portions of Lucide are held by Cole Bemis 2013-2022 as part of Feather (MIT). All other copyright (c) for Lucide are held by Lucide Contributors 2022.");
        sb.AppendLine("// ---------------------------------------------------------------------------------------------------------------------");

        return sb;
    }
    
    public static StringBuilder AppendNamespace(this StringBuilder sb) {
        sb.AppendLine("namespace CodeOfChaos.Lucide;");
        
        return sb;
    }
}
