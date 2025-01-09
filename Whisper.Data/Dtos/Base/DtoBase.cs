namespace Whisper.Data.Dtos.Base;

public record DtoBase(Guid Id, DateTime DateCreated, DateTime DateUpdated) : Dto(Id);