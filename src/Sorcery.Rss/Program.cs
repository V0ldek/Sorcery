// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;
using Sorcery.Shared;

var v0ldek = new SyndicationPerson("mat@gienieczko.com", "Mat Gienieczko", "https://v0ldek.com");
var feed = new SyndicationFeed(
    "Sourcery",
    "Software and Computer Science wizardry blog by Mat Gienieczko",
    new Uri("https://v0ldek.com"),
    "https://v0ldek.com/sourcery",
    DateTime.Now)
{
    Authors = { v0ldek },
    Copyright = new TextSyndicationContent($"{ComputeCopyright()} Mateusz Gienieczko"),
    Language = "en-GB",
    ImageUrl = new Uri("https://v0ldek.com/img/icon.png"),
    Categories =
    {
        new SyndicationCategory("software"),
        new SyndicationCategory("programming"),
        new SyndicationCategory("computer science"),
    }
};

var blogBook = new BlogBook();
var posts = blogBook.Posts.OrderBy(p => p.DateOfPublication);

feed.Items = posts.Select(p =>
{
    var uri = new Uri($"https://v0ldek.com{p.Route}");
    return new SyndicationItem(p.Title, p.ShortDescription, uri, uri.ToString(), p.DateOfPublication!.Value)
    {
        Authors = { v0ldek },
        PublishDate = p.DateOfPublication!.Value,
    };
});

var settings = new XmlWriterSettings
{
    Encoding = Encoding.UTF8,
    NewLineHandling = NewLineHandling.Entitize,
    NewLineOnAttributes = true,
    Indent = true
};

using (var xmlWriter = XmlWriter.Create(Console.Out, settings))
{
    var rssFormatter = new Rss20FeedFormatter(feed, false);
    rssFormatter.WriteTo(xmlWriter);
    xmlWriter.Flush();
}

static string ComputeCopyright()
{
    const int StartYear = 2022;
    var year = DateTime.Now.Year;
    return year == StartYear ? StartYear.ToString() : $"{StartYear} - {year}";
}
