namespace Bfn.DevOps.Models;

public static class ThemeCatalog
{
    public static readonly (string Value, string Label)[] All =
    [
        ("bfnorbi", "BFN Orbi"), ("bfnlight", "BFN Açık"), ("bfndark", "BFN Koyu"),
        ("light", "Light"), ("dark", "Dark"), ("cupcake", "Cupcake"), ("bumblebee", "Bumblebee"),
        ("emerald", "Emerald"), ("corporate", "Corporate"), ("synthwave", "Synthwave"),
        ("retro", "Retro"), ("cyberpunk", "Cyberpunk"), ("valentine", "Valentine"),
        ("halloween", "Halloween"), ("garden", "Garden"), ("forest", "Forest"), ("aqua", "Aqua"),
        ("lofi", "Lofi"), ("pastel", "Pastel"), ("fantasy", "Fantasy"), ("wireframe", "Wireframe"),
        ("black", "Black"), ("luxury", "Luxury"), ("dracula", "Dracula"), ("cmyk", "CMYK"),
        ("autumn", "Autumn"), ("business", "Business"), ("acid", "Acid"), ("lemonade", "Lemonade"),
        ("night", "Night"), ("coffee", "Coffee"), ("winter", "Winter"), ("dim", "Dim"),
        ("nord", "Nord"), ("sunset", "Sunset")
    ];

    public static readonly HashSet<string> Values = All.Select(t => t.Value).ToHashSet();
}
