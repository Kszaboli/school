using System;
using System.Collections.Generic;

namespace school.Models;

public partial class Student
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public int? Age { get; set; }

    public string? Email { get; set; }

    public DateTime? CreatedTime { get; set; }

    public virtual Mark? Mark { get; set; }
}
