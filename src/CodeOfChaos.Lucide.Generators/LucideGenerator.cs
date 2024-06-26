// ---------------------------------------------------------------------------------------------------------------------
// Copyright (c) for portions of Lucide are held by Cole Bemis 2013-2022 as part of Feather (MIT).
// All other copyright (c) for Lucide are held by Lucide Contributors 2022.
// ---------------------------------------------------------------------------------------------------------------------

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
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
            .Where(file => file.Path.EndsWith(".svg"));

        IncrementalValueProvider<ImmutableArray<(string Name, string? Svg)>> iconsProvider = files
            .Select((file, cancellationToken) => (
                Name: Path.GetFileNameWithoutExtension(file.Path),
                Svg: file.GetText(cancellationToken)?.ToString()
            ))
            .Collect();

        context.RegisterSourceOutput(iconsProvider, (spc, iconsArray) => {
            // Adds the file template with specific name
            spc.AddSource("IconSet.g.cs", IconsSetTemplate.AssembleFileTemplate(iconsArray));
            spc.AddSource("Icons.g.cs", IconsTemplate.AssembleFileTemplate(iconsArray));
        });
    }
}
