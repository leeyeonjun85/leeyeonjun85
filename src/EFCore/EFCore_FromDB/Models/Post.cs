using System;
using System.Collections.Generic;

namespace EFCore_FromDB.Models;

public partial class Post
{
    public long PostId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public long BlogId { get; set; }

    public virtual Blog Blog { get; set; } = null!;
}
