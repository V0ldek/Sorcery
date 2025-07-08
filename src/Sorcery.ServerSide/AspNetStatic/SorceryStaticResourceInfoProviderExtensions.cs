// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.

using System.Text.RegularExpressions;
using AspNetStatic;

namespace Sorcery.ServerSide.AspNetStatic;

internal static partial class SorceryStaticResourceInfoProviderExtensions
{
    [GeneratedRegex("""
                    ^[\s]*@page[\s]+"([^"]+)"
                    """)]
    private static partial Regex PageDirectiveRegex();
    
    /// <summary>
    /// Find all Blazor pages and add them to the static resources.
    /// </summary>
    /// <param name="provider">AspNetStatic static resource container.</param>
    /// <param name="environment">Web host environment.</param>
    /// <returns>The <paramref name="provider"/> object with all Blazor pages registered.</returns>
    public static SorceryStaticResourcesInfoProvider AddAllPages(
        this SorceryStaticResourcesInfoProvider provider,
        IWebHostEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(provider);
        ArgumentNullException.ThrowIfNull(environment);
        const string RazorExtension = ".razor";
        var pageDirectiveRegex = PageDirectiveRegex();
        
        // Pages are located in Pages/.
        // We look through all .razor files in the directory tree and search for the @page directive,
        // which specifies the URL path.
        var pagesFolderPath = Path.Combine(environment.ContentRootPath, "Pages");
        var razorPageFiles = Directory.GetFiles(
            pagesFolderPath,
            $"*{RazorExtension}",
            SearchOption.AllDirectories);
        var razorPageRoutes = 
            from razorPage in razorPageFiles
            let fileContents = File.ReadAllText(razorPage)
            let match = pageDirectiveRegex.Match(fileContents)
            where match.Success
            select match.Groups[1].Value;

        provider.Add(razorPageRoutes.Select(route => new PageResource(route)));

        return provider;
    }

    public static SorceryStaticResourcesInfoProvider AddAllWebContent(
        this SorceryStaticResourcesInfoProvider provider,
        IWebHostEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(provider);
        ArgumentNullException.ThrowIfNull(environment);
        
        // Only add the bundled JS and CSS and exceptions.
        AddRequiredJs(provider, environment);
        AddRequiredCss(provider, environment);
        
        // Add all fonts we use.
        AddFonts(provider, environment);
        
        // Add all of asciicast as text files.
        AddAsciicastContent(provider, environment);
        
        // Add all img and pdf content as binary files.
        AddImgContent(provider, environment);
        AddPdfContent(provider, environment);
        
        // Add the RSS feed.
        provider.Add(new TextResource("/feed.rss"));
        
        // Add robots.txt.
        provider.Add(new TextResource("/robots.txt"));
        
        // Add the favicon.
        provider.Add(new BinResource("/favicon.ico"));
        
        return provider;
    }

    private static SorceryStaticResourcesInfoProvider AddRequiredJs(
        this SorceryStaticResourcesInfoProvider provider,
        IWebHostEnvironment environment)
    {
        provider.Add(new JsResource("/_framework/blazor.web.js"))
            .Add(new JsResource("/_content/MudBlazor/MudBlazor.min.js"))
            .Add(new JsResource("/scripts/lib/asciinema-player.min.js"))
            .Add(new JsResource("/scripts/bundled/bundle.min.js"))
            .Add(new JsResource("/scripts/lib/katex-auto-render.min.js"));
        return provider;
    }
    
    private static SorceryStaticResourcesInfoProvider AddRequiredCss(
        this SorceryStaticResourcesInfoProvider provider,
        IWebHostEnvironment environment)
    {
        provider.Add(new CssResource("/_content/MudBlazor/MudBlazor.min.css"))
            .Add(new CssResource("/Sorcery.ServerSide.styles.css"))
            .Add(new CssResource("/css/bundled/bundle.min.css"));
        return provider;
    }

    private static SorceryStaticResourcesInfoProvider AddFonts(
        this SorceryStaticResourcesInfoProvider provider,
        IWebHostEnvironment environment)
    {
        // There are three font groups:
        // - Our fonts: Fira and MesloLGS
        // - Katex lib fonts
        // - Fontawesome webfonts
        string[] fontExtensions = [".woff", ".woff2"];
        
        var mainFontDirectory = Path.Combine(environment.WebRootPath, "css", "fonts");
        var katexFontDirectory = Path.Combine(environment.WebRootPath, "css", "lib", "fonts");
        var fontawesomeFontDirectory = Path.Combine(environment.WebRootPath, "css", "lib", "fontawesome", "webfonts");

        var fontFiles = 
            from directory in new[] { mainFontDirectory, katexFontDirectory, fontawesomeFontDirectory }
            from ext in fontExtensions
            from file in Directory.GetFiles(
                directory,
                $"*{ext}",
                SearchOption.TopDirectoryOnly)
            select file;

        provider.Add(fontFiles.Select(f => new BinResource(f.MakeRoute(environment))));
        return provider;
    }
    
    private static SorceryStaticResourcesInfoProvider AddAsciicastContent(
        this SorceryStaticResourcesInfoProvider provider,
        IWebHostEnvironment environment)
    {
        const string AsciicastExtension = ".cast";
        var asciicastDirectory = Path.Combine(environment.WebRootPath, "asciicast");
        var asciicastFiles = 
            from file in Directory.GetFiles(
                asciicastDirectory,
                $"*{AsciicastExtension}",
                SearchOption.AllDirectories)
            select file;

        provider.Add(asciicastFiles.Select(f => new TextResource(f.MakeRoute(environment))));
        return provider;
    }

    private static SorceryStaticResourcesInfoProvider AddImgContent(
        this SorceryStaticResourcesInfoProvider provider,
        IWebHostEnvironment environment)
    {
        // We don't filter on extension, just assume everything in img is a binary resource.
        var imgDirectory = Path.Combine(environment.WebRootPath, "img");
        var imgFiles = 
            from file in Directory.GetFiles(
                imgDirectory,
                "*",
                SearchOption.AllDirectories)
            select file;

        provider.Add(imgFiles.Select(f => new BinResource(f.MakeRoute(environment))));
        return provider;
    }
    
    private static SorceryStaticResourcesInfoProvider AddPdfContent(
        this SorceryStaticResourcesInfoProvider provider,
        IWebHostEnvironment environment)
    {
        // We don't filter on extension, just assume everything in pdf is a binary resource.
        var pdfDirectory = Path.Combine(environment.WebRootPath, "pdf");
        var pdfFiles = 
            from file in Directory.GetFiles(
                pdfDirectory,
                "*",
                SearchOption.AllDirectories)
            select file;

        provider.Add(pdfFiles.Select(f => new BinResource(f.MakeRoute(environment))));
        return provider;
    }

    private static string MakeRoute(this string absolutePath, IWebHostEnvironment environment) => 
        absolutePath.Replace(environment.WebRootPath, "").Replace(Path.DirectorySeparatorChar, '/');
}
