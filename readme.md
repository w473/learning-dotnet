dotnet aspnet-codegenerator controller -name EventsController -async -api -m Event -dc EventsContext -outDir Controllers

dotnet aspnet-codegenerator controller -name RegistrationController -async -api -m Registration -dc EventsContext -outDir Controllers

dotnet ef migrations add InitialCreate    

dotnet ef database update 


dotnet add package XXXXXX --prerelease

http://localhost:5297/swagger/v1/swagger.json

User 1:
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJkNmFiZjQxMC0zMjJjLTRhZWYtOWVmYy04YTg0NWI2ZDc5ODUiLCJuYW1lIjoiSmFjZWsiLCJpYXQiOjE1MTYyMzkwMjJ9.ij_DamAKq7qKECoXY7QUpp4BdrxitMgBBxwNGg8Lhm0

User 2:
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI1Mzc3MDRlMy03Y2YzLTQxYjAtYmI5OC05MGUwNjAxY2NkMWEiLCJuYW1lIjoiQW5keSIsImlhdCI6MTUxNjIzOTAyMn0.QfyF2DiQRvfV_GgOA2wX0Lo-g7L9CuKIUZMaTC-sHFU

docker build -t events -f Dockerfile .


docker run -d -p 5000:5000 --name events-app events