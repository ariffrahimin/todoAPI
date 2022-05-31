using System;
using System.Collections.Generic;

namespace todo.Models
{
    public partial class Todo
    {
        public long ListId { get; set; }
        public string? ListContent { get; set; }
        public string? ListStatus { get; set; }
    }
}
