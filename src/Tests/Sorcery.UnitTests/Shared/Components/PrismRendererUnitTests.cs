// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.

using Bunit;
using Microsoft.JSInterop;
using NSubstitute;
using Sorcery.Shared.Components;
using Sorcery.UnitTests.Extensions;

namespace Sorcery.UnitTests.Shared.Components;

public class PrismRendererUnitTests
{
    private const string PrismHighlightAllIdentifier = "window.Prism.highlightAll";

    private const string PrismHighlightUnderIdentifier = "window.Prism.highlightAllUnder";

    [Fact]
    public async Task OnFirstRender_WhenNoContainerIdIsSpecified_InvokesPrismHighlightAll()
    {
        using var ctx = new TestContext();
        var plannedHighlight = ctx.JSInterop.SetupVoid(PrismHighlightAllIdentifier);

        var cut = ctx.RenderComponent<PrismRenderer>();
        plannedHighlight.SetVoidResult();

        await ctx.ShouldNotHaveThrownRenderingExceptionsAsync();
        plannedHighlight.VerifyInvoke(PrismHighlightAllIdentifier);
    }

    [Fact]
    public async Task OnSecondRender_WhenNoContainerIdIsSpecified_MakesNoInvocations()
    {
        using var ctx = new TestContext();
        var plannedHighlight = ctx.JSInterop.SetupVoid(PrismHighlightAllIdentifier);

        var cut = ctx.RenderComponent<PrismRenderer>();
        plannedHighlight.SetVoidResult();
        // Re-render the component.
        cut.Render();

        await ctx.ShouldNotHaveThrownRenderingExceptionsAsync();
        // If the function was invoked twice this would fail.
        plannedHighlight.VerifyInvoke(PrismHighlightAllIdentifier, 1);
    }

    [Fact]
    public async Task OnFirstRender_WhenContainerIdIsSpecified_InvokesPrismHighlightUnder()
    {
        const string GetElementByIdIdentifier = "document.getElementById";
        const string TestElementId = "test-element";

        using var ctx = new TestContext();
        var testElementReference = Substitute.For<IJSInProcessObjectReference>();
        var plannedGetById = ctx.JSInterop.Setup<IJSInProcessObjectReference>(GetElementByIdIdentifier, TestElementId);
        var plannedHighlight = ctx.JSInterop.SetupVoid(PrismHighlightUnderIdentifier, testElementReference);

        var cut = ctx.RenderComponent<PrismRenderer>(x => x.Add(r => r.ContainerId, TestElementId));
        plannedGetById.SetResult(testElementReference);
        plannedHighlight.SetVoidResult();

        await ctx.ShouldNotHaveThrownRenderingExceptionsAsync();
        plannedGetById.VerifyInvoke(GetElementByIdIdentifier);
        plannedHighlight.VerifyInvoke(PrismHighlightUnderIdentifier);
    }

    [Fact]
    public async Task OnSecondRender_WhenContainerIdIsSpecified_MakesNoInvocations()
    {
        const string GetElementByIdIdentifier = "document.getElementById";
        const string TestElementId = "test-element";

        using var ctx = new TestContext();
        var testElementReference = Substitute.For<IJSInProcessObjectReference>();
        var plannedGetById = ctx.JSInterop.Setup<IJSInProcessObjectReference>(GetElementByIdIdentifier, TestElementId);
        var plannedHighlight = ctx.JSInterop.SetupVoid(PrismHighlightUnderIdentifier, testElementReference);

        var cut = ctx.RenderComponent<PrismRenderer>(x => x.Add(r => r.ContainerId, TestElementId));
        plannedGetById.SetResult(testElementReference);
        plannedHighlight.SetVoidResult();
        // Re-render the component.
        cut.Render();

        await ctx.ShouldNotHaveThrownRenderingExceptionsAsync();
        // If the function was invoked twice these would fail.
        plannedGetById.VerifyInvoke(GetElementByIdIdentifier, 1);
        plannedHighlight.VerifyInvoke(PrismHighlightUnderIdentifier, 1);
    }
}
