using System;
using System.Collections.Generic;

namespace MoqAPI.Model;

public partial class Employee
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? Age { get; set; }
}
