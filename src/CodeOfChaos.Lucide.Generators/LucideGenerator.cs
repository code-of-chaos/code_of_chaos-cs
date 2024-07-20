// ---------------------------------------------------------------------------------------------------------------------
// Copyright (c) for portions of Lucide are held by Cole Bemis 2013-2022 as part of Feather (MIT).
// All other copyright (c) for Lucide are held by Lucide Contributors 2022.
// Permission to use, copy, modify, and/or distribute this software for any purpose with or without fee is hereby granted,provided that the above copyright notice and this permission notice appear in all copies.
// THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.
// ---------------------------------------------------------------------------------------------------------------------

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Lucide.Generators.Templates;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace CodeOfChaos.Lucide.Generators;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[Generator]
public class LucideGenerator : IIncrementalGenerator {
    public void Initialize(IncrementalGeneratorInitializationContext context) {
        
        IncrementalValuesProvider<AdditionalText> files = context.AdditionalTextsProvider
            .Where(file => file.Path.Contains("icons") && file.Path.EndsWith(".svg"));

        IncrementalValueProvider<ImmutableArray<(string Name, string? Svg)>> iconsProvider = files
            .Select((file, cancellationToken) => (
                Name: Path.GetFileNameWithoutExtension(file.Path),
                Svg: file.GetText(cancellationToken)?.ToString()
            ))
            .Collect();
        
        IncrementalValueProvider<string?> licenseProvider = context.AdditionalTextsProvider
            .Where(file => file.Path.EndsWith("LICENSE"))
            .Select((file, cancellationToken) => file.GetText(cancellationToken)?.ToString())
            .Collect()
            .Select((texts, _) => texts.FirstOrDefault());

        IncrementalValueProvider<(ImmutableArray<(string Name, string? Svg)> Left, string? Right)> combinedProvider;
        combinedProvider = iconsProvider.Combine(licenseProvider);

        context.RegisterSourceOutput(combinedProvider, (spc, combined) => {
            (ImmutableArray<(string Name, string? Svg)> iconsArray, string? licenseContent) = combined;
            
            // Adds the files with templates and specific names
            spc.AddSource("LucideData.g.cs", new LucideData(iconsArray, licenseContent).Assemble());
            spc.AddSource("LucideIconsSet.g.cs", new LucideIconsSet(iconsArray).Assemble());
            spc.AddSource("LucideIconNames.g.cs", new LucideIconNames(iconsArray).Assemble());
        });
    }
}
