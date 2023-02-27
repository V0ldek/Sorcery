// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;
using Sorcery.Shared;

var feed = new SyndicationFeed(
    "Sourcery",
    "Software and Computer Science wizardry blog by Mat Gienieczko",
    new Uri("https://v0ldek.com"),
    "https://v0ldek.com/sourcery",
    DateTime.Now)
{
    Copyright = new TextSyndicationContent($"{ComputeCopyright()} Mateusz Gienieczko"),
    Language = "en-GB",
};
feed.Authors.Add(new SyndicationPerson("mat@gienieczko.com"));
feed.Categories.Add(new SyndicationCategory("software"));
feed.Categories.Add(new SyndicationCategory("programming"));
feed.Categories.Add(new SyndicationCategory("computer science"));

var blogBook = new BlogBook();
var posts = blogBook.Posts.OrderBy(p => p.DateOfPublication);

feed.Items = posts.Select(p => {
    var uri = new Uri($"https://v0ldek.com/{p.Route}");
    return new SyndicationItem(p.Title, p.ShortDescription, uri, uri.ToString(), p.DateOfPublication!.Value);
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
    const int startYear = 2022;
    var year = DateTime.Now.Year;
    return year == startYear ? startYear.ToString() : $"{startYear} - {year}";
}
