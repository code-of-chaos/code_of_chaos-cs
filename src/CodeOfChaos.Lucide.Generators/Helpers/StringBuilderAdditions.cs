// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text;

namespace CodeOfChaos.Lucide.Generators.Helpers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class StringBuilderAdditions {
    public static StringBuilder AppendCopyrightLucide(this StringBuilder sb) {
        return sb
            .AppendLine("// ---------------------------------------------------------------------------------------------------------------------")
            .AppendLine("// Copyright (c) for portions of Lucide are held by Cole Bemis 2013-2022 as part of Feather (MIT).")
            .AppendLine("// All other copyright (c) for Lucide are held by Lucide Contributors 2022.")
            .AppendLine("// Permission to use, copy, modify, and/or distribute this software for any purpose with or without fee is hereby granted, " +
                        "provided that the above copyright notice and this permission notice appear in all copies.")
            .AppendLine("// THE SOFTWARE IS PROVIDED \"AS IS\" AND THE AUTHOR DISCLAIMS ALL WARRANTIES WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS. " +
                        "IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, " +
                        "WHETHER IN AN ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.")
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
