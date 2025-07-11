// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.

using AspNetStatic;

namespace Sorcery.ServerSide.AspNetStatic;

internal sealed class SorceryStaticResourcesInfoProvider : StaticResourcesInfoProvider
{
    public SorceryStaticResourcesInfoProvider Add(TextResource textResource)
    {
        ArgumentNullException.ThrowIfNull(textResource);
        this.resources.Add(textResource);
        return this;
    }
    
    public SorceryStaticResourcesInfoProvider Add(IEnumerable<TextResource> textResources)
    {
        ArgumentNullException.ThrowIfNull(textResources);
        this.resources.AddRange(textResources);
        return this;
    }
}
