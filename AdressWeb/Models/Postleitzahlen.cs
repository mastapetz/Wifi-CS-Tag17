﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace AdressWeb.Models;

public partial class Postleitzahlen
{
    public int PlzId { get; set; }

    public int Plz { get; set; }

    public string Ort { get; set; }

    public virtual ICollection<Adressen> Adressen { get; set; } = new List<Adressen>();
}