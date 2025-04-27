using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
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
        public EventCallback OnClose { get; set; }
        [Parameter]
        public EventCallback OnSave { get; set; }
        private bool IsModalVisible { get; set; } = false;
        public void Toggle()
        {
            IsModalVisible = !IsModalVisible;
        }
    }
}
