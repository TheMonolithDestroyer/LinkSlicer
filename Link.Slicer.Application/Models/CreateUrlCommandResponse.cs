using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Slicer.Application.Models
{
    public record CreateUrlCommandResponse(string ShortUrl, string Comment, DateTimeOffset CreatedAt)
    {
    }
}
