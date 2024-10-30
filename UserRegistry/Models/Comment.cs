using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRegistry.Models;

internal class Comment
{
    public int PostId { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Body { get; set; }

    public Comment(int postId, int id, string name, string email, string body)
    {
        PostId = postId;
        Id = id;
        Name = name;
        Email = email;
        Body = body;
    }
}