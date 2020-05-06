FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR "/src"
EXPOSE 80
EXPOSE 443

COPY ["*.csproj", "."]
RUN dotnet restore "InvoiceManagerApi.csproj"

COPY . .
RUN dotnet publish "InvoiceManagerApi.csproj" -c Release -o /publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "InvoiceManagerApi.dll"]