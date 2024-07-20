// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components;
namespace CodeOfChaos.Lucide;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class LucideIcon {
    #region Properties
    
    /// <summary>
    /// The name of the Lucide Icon to be used. For example "
    /// </summary>
    [Parameter, EditorRequired] public required string Name { get; set; }
    
    /// <summary>
    /// Represents the size of an icon in the LucideIcon component.
    /// </summary>
    [Parameter] public string Size { get; set; } = "1em";
    
    /// <summary>
    /// The fill color of the SVG element.
    /// </summary>
    [Parameter] public string FillColor { get; set; } = "none";
    
    /// <summary>
    /// The color of the stroke for the SVG element. The default value is "currentColor".
    /// </summary>
    [Parameter] public string StrokeColor { get; set; } = "currentColor";
    
    /// <summary>
    /// The width of the stroke applied to the SVG element. Default value is "2".
    /// </summary>
    [Parameter] public string StrokeWidth { get; set; } = "2";
    
    /// <summary>
    /// Gets or sets the stroke linecap attribute for the SVG element. Determines the shape of the line endings. Default value is "round".
    /// </summary>
    [Parameter] public string StrokeLinecap { get; set; } = "round";
    
    /// <summary>
    /// The stroke line join property determines the shape used to join two line segments where they meet.
    /// </summary>
    [Parameter] public string StrokeLinejoin { get; set; } = "round";
    
    /// <summary>
    /// Represents an SVG icon component.
    /// </summary>
    [Parameter] public string Class { get; set; } = string.Empty;
    
    /// <summary>
    /// A dictionary of additional attributes to be applied to the SVG element.
    /// </summary>
    /// <remarks>
    /// This property allows you to provide additional attributes to the SVG element that are not specifically defined in the <see cref="LucideIcon"/> component. The additional attributes will be rendered as HTML attributes on the SVG element.
    /// </remarks>
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object> AdditionalAttributes { get; set; } = [];
    
    private MarkupString SvgMarkup => LucideIconsSet.IconAtlas[Name];
    #endregion
}
