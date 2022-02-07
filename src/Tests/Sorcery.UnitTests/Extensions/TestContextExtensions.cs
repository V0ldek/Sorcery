// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.

using Bunit;

namespace Sorcery.UnitTests.Extensions;

internal static class TestContextExtensions
{
    public static async Task ShouldNotHaveThrownRenderingExceptionsAsync(this TestContext ctx)
    {
        var timer = Task.Delay(1000);

        var task = await Task.WhenAny(timer, ctx.Renderer.UnhandledException);

        if (task == ctx.Renderer.UnhandledException)
        {
            throw ctx.Renderer.UnhandledException.Result;
        }
    }
}
