# Code Of Chaos - Lucide
A small library to use Lucide Icons as Razor components.
Version of this package follows the same semantic version as Lucide itself, except for the "Fix" part of the version string.
This is so I can roll out a fix if the automated process made a mistake.


## Usage

```csharp
@using CodeOfChaos.Lucide

// The only required parameter is `Name`
<LucideIcon Name="signature">

// When you need more granulairty
<LucideIcon
    Name="signature"
    Size="24"
    FillColor="none"
    StrokeColor="currentColor"
    StrokeWidth="2"
    SvgMarkup="@iconMarkup"
    Class="custom-icon" />
```

| **Key**       | **Attributes**                                       |
|---------------|------------------------------------------------------|
| `Name`        | The name of the icon, as known in the lucide library |
| `Size`        | Width and height of the icon (default 24).           |
| `FillColor`   | Fill color of the icon (default none).               |
| `StrokeColor` | Stroke color (default currentColor).                 |
| `StrokeWidth` | Thickness of stroke lines (default 2).               |
| `SvgMarkup`   | The SVG path data to render.                         |

---

## Lucide Copyright & Permission
Copyright (c) for portions of Lucide are held by Cole Bemis 2013-2022 as part of Feather (MIT). All other copyright (c) for Lucide are held by Lucide Contributors 2022.

Permission to use, copy, modify, and/or distribute this software for any purpose with or without fee is hereby granted, provided that the above copyright notice and this permission notice appear in all copies.

THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE. 