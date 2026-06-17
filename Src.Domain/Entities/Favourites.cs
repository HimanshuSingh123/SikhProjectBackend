using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Domain.Entities;

public class Favourites
{
    public string Username { get; set; } = default!;
    public string ItemTitle { get; set; } = default!;
    public string ItemDescription { get; set; } = default!;
    public double Price { get; set; }
    public string Category { get; set; } = default!;
    public int FavId { get; set; }
    public int SubmissionId { get; set; }

    public Merch Merch { get; set; } = default!;
    public User User { get; set; } =  default!;
}

