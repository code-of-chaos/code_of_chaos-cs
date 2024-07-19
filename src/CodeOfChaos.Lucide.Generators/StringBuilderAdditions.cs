// ---------------------------------------------------------------------------------------------------------------------
// Copyright (c) for portions of Lucide are held by Cole Bemis 2013-2022 as part of Feather (MIT).
// All other copyright (c) for Lucide are held by Lucide Contributors 2022.
// ---------------------------------------------------------------------------------------------------------------------

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
        return sb
            .AppendLine("// ---------------------------------------------------------------------------------------------------------------------")
            .AppendLine("// Copyright (c) for portions of Lucide are held by Cole Bemis 2013-2022 as part of Feather (MIT).")
            .AppendLine("// All other copyright (c) for Lucide are held by Lucide Contributors 2022.")
            .AppendLine("// ---------------------------------------------------------------------------------------------------------------------")
            .AppendLine()
        ;
    }
    
    public static StringBuilder AppendNamespace(this StringBuilder sb) {
        return sb.AppendLine("namespace CodeOfChaos.Lucide;");
    }

    public static StringBuilder AppendImports(this StringBuilder sb, IEnumerable<string>? lines = null) {
        sb
            .AppendLine("// ---------------------------------------------------------------------------------------------------------------------")
            .AppendLine("// Imports")
            .AppendLine("// ---------------------------------------------------------------------------------------------------------------------")
        ;
        
        // ReSharper disable once InvertIf
        if (lines != null) foreach (string ns in lines) sb.AppendLine($"using {ns};");

        return sb.AppendNamespace();
    }
    
    public static StringBuilder AppendCodeHeader(this StringBuilder sb) {
        sb
            .AppendLine("// ---------------------------------------------------------------------------------------------------------------------")
            .AppendLine("// Code")
            .AppendLine("// ---------------------------------------------------------------------------------------------------------------------")
            ;

        return sb;
    }
}
