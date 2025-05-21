using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutPlanner.Components.Shared.EditModal
{
    partial class EditModal : ComponentBase
    {
        [Parameter] 
        public RenderFragment ChildContent { get; set; }
        [Parameter] 
        public string Title { get; set; } = "Modifier";
        [Parameter]
        [NotNull]
        public object? Model { get; set; }
        [Parameter] 
        public EventCallback OnClose { get; set; }
        [Parameter]
        public EventCallback OnSave { get; set; }
        private bool IsModalVisible { get; set; } = false;
        public void Toggle()
        {
            IsModalVisible = !IsModalVisible;
        }

        private async Task HandleValidSubmit()
        {
            if (OnSave.HasDelegate)
                await OnSave.InvokeAsync();

            Toggle();
        }
    }
}
