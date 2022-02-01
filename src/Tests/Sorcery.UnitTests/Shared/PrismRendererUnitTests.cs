// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.

using Bunit;
using Microsoft.JSInterop;
using NSubstitute;
using Sorcery.Shared;

namespace Sorcery.UnitTests.Shared;

public class PrismRendererUnitTests
{
    private const string PrismHighlightAllIdentifier = "window.Prism.highlightAll";

    private const string PrismHighlightUnderIdentifier = "window.Prism.highlightAllUnder";

    [Fact]
    public void OnFirstRender_WhenNoContainerIdIsSpecified_InvokesPrismHighlightAll()
    {
        using var ctx = new TestContext();
        var plannedHighlight = ctx.JSInterop.SetupVoid(PrismHighlightAllIdentifier);

        var cut = ctx.RenderComponent<PrismRenderer>();
        plannedHighlight.SetVoidResult();

        plannedHighlight.VerifyInvoke(PrismHighlightAllIdentifier);
    }

    [Fact]
    public void OnSecondRender_WhenNoContainerIdIsSpecified_MakesNoInvocations()
    {
        using var ctx = new TestContext();
        var plannedHighlight = ctx.JSInterop.SetupVoid(PrismHighlightAllIdentifier);

        var cut = ctx.RenderComponent<PrismRenderer>();
        plannedHighlight.SetVoidResult();
        // Re-render the component.
        cut.Render();

        // If the function was invoked twice this would fail.
        plannedHighlight.VerifyInvoke(PrismHighlightAllIdentifier, 1);
    }

    [Fact]
    public void OnFirstRender_WhenContainerIdIsSpecified_InvokesPrismHighlightUnder()
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

        plannedGetById.VerifyInvoke(GetElementByIdIdentifier);
        plannedHighlight.VerifyInvoke(PrismHighlightUnderIdentifier);
    }

    [Fact]
    public void OnSecondRender_WhenContainerIdIsSpecified_MakesNoInvocations()
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

        // If the function was invoked twice these would fail.
        plannedGetById.VerifyInvoke(GetElementByIdIdentifier, 1);
        plannedHighlight.VerifyInvoke(PrismHighlightUnderIdentifier, 1);
    }
}
