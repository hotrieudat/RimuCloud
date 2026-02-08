using System;
using System.Collections.Generic;
using System.Text;

namespace RimuCloud.Domain.Entities
{
    public class Entry
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
