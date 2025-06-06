﻿//Sourced from MudBlazor (https://mudblazor.com/)

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace WorkoutPlanner.Components.Shared.SwipeArea
{
#nullable enable

    /// <summary>
    /// An area which receives swipe events for devices where touch events are supported.
    /// </summary>
    public partial class MudSwipeArea : ComponentBase
    {
        private static readonly string[] _preventDefaultEventNames = ["onpointerdown", "onpointerup", "onpointercancel"];

        private double? _swipeDelta;
        internal int[]? _listenerIds;
        internal double? _xDown, _yDown;
        private bool _preventDefaultChanged;
        private ElementReference _componentRef;

        /// <summary>
        /// The content within this swipe area.
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// Occurs when a swipe has ended.
        /// </summary>
        [Parameter]
        public EventCallback<SwipeEventArgs> OnSwipeEnd { get; set; }

        /// <summary>
        /// The amount of pixels which must be swiped to raise the <see cref="OnSwipeEnd"/> event.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>100</c> (100 pixels).
        /// </remarks>
        [Parameter]
        public int Sensitivity { get; set; } = 100;

        /// <summary>
        /// The CSS classes applied to this component.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>null</c>.  You can use spaces to separate multiple classes.  Use the <see cref="Style"/> property to apply custom CSS styles.
        /// </remarks>
        [Parameter]
        public string? Class { get; set; }

        /// <summary>
        /// The CSS styles applied to this component.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>null</c>.  Use the <see cref="Class"/> property to apply CSS classes.
        /// </remarks>
        [Parameter]
        public string? Style { get; set; }

        /// <summary>
        /// Prevents the default behavior of the browser when swiping.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>false</c>. Typically <c>true</c> when swiping up or down, which will prevent the whole page from scrolling.
        /// </remarks>
        [Parameter]
        public bool PreventDefault { get; set; }

        protected string Classname =>
            new CssBuilder("mud-swipearea")
                .AddClass(Class)
                .Build();

        /// <inheritdoc />
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            var preventDefault = parameters.GetValueOrDefault<bool>(nameof(PreventDefault));
            if (preventDefault != PreventDefault)
            {
                _preventDefaultChanged = true;
            }

            await base.SetParametersAsync(parameters);
        }

        private async Task SetPreventDefaultInternal(bool value)
        {
        }

        /// <inheritdoc />
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (_preventDefaultChanged)
            {
                _preventDefaultChanged = false;
                await SetPreventDefaultInternal(PreventDefault);
            }
        }

        internal void OnPointerDown(Microsoft.AspNetCore.Components.Web.PointerEventArgs arg)
        {
            _xDown = arg.ClientX;
            _yDown = arg.ClientY;
        }

        internal async Task OnPointerUp(Microsoft.AspNetCore.Components.Web.PointerEventArgs arg)
        {
            if (_xDown is null || _yDown is null)
            {
                return;
            }

            var xDiff = _xDown.Value - arg.ClientX;
            var yDiff = _yDown.Value - arg.ClientY;

            if (Math.Abs(xDiff) < Sensitivity && Math.Abs(yDiff) < Sensitivity)
            {
                _xDown = _yDown = null;
                return;
            }

            var swipeDirection = Math.Abs(xDiff) > Math.Abs(yDiff) ?
                xDiff > 0 ? SwipeDirection.RightToLeft : SwipeDirection.LeftToRight :
                yDiff > 0 ? SwipeDirection.BottomToTop : SwipeDirection.TopToBottom;

            if (Math.Abs(xDiff) > Math.Abs(yDiff))
            {
                _swipeDelta = xDiff;
            }
            else
            {
                _swipeDelta = yDiff;
            }

            await OnSwipeEnd.InvokeAsync(new SwipeEventArgs(arg, swipeDirection, _swipeDelta, this));
            _xDown = _yDown = null;
        }

        internal void OnPointerCancel(Microsoft.AspNetCore.Components.Web.PointerEventArgs arg)
        {
            _xDown = _yDown = null;
        }
    }
    
    
    
}
