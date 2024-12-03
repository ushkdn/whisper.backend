### For migration from other service ###

#### For example: create migration from Whisper.User service u need to do ####
dotnet ef --startup-project ..\Whisper.User\ migrations add Migration_Name -c WhisperDbContext
 