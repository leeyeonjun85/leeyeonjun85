using System;
using System.Collections.Generic;

namespace EFCore_FromDB.Models;

public partial class Blog
{
    public long BlogId { get; set; }

    public string Url { get; set; } = null!;

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
