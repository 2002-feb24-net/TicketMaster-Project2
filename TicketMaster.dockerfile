# 1. docker build -t ticketmaster-api:1.0 .
# 2. docker run --rm -it -p 8000:80 -e "ConnectionStrings__TicketDb=Server=tcp:rev-stewart.database.windows.net,1433;Initial Catalog=Project2;Persist Security Info=False;User ID=Pstewart;Password=Sherlocked221;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;" ticketmaster-api:1.0

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /app

# when docker goes through a dockerfile's steps, it keeps track of all the "inputs"
# to each given line.
COPY *.sln ./
COPY TicketMaster.REST-Api/*.csproj TicketMaster.REST-Api/
COPY TicketMaster.Domain/*.csproj TicketMaster.Domain/
COPY TicketMaster.DataAccess/*.csproj TicketMaster.DataAccess/
#COPY TicketMaster.Tests/*.csproj TicketMaster.Tests/

RUN dotnet restore

# so long as the csproj/sln files haven't changed, we'll always cache up to this point.
# saves on build time!

# now copy everything else so we can build
COPY . ./

RUN dotnet publish TicketMaster.REST-Api -o publish --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

WORKDIR /app

COPY --from=build /app/publish ./

CMD [ "dotnet", "TicketMaster.REST-Api.dll" ]