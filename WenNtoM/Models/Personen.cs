﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WenNtoM.Models;

public partial class Personen
{
    public int PersonenId { get; set; }

    public string Name { get; set; }

    public virtual ICollection<PerInt> PerInt { get; set; } = new List<PerInt>();
}