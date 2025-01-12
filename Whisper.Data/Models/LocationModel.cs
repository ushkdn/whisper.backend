using Whisper.Data.Entities;

namespace Whisper.Data.Models;

public class LocationModel
{
    public string? Country { get; set; }

    public virtual List<UserModel>? User { get; set; } = [];
}