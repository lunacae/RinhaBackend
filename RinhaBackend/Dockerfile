FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Configurar o diretório de trabalho no contêiner
WORKDIR /app

# Copiar o arquivo csproj e restaurar as dependências
COPY /*.csproj ./
RUN dotnet restore

# Copiar o restante do código da aplicação
COPY . ./

# Publicar o aplicativo
RUN dotnet publish -c Release -o out


# Usar uma imagem base menor para a imagem final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Definir o diretório de trabalho no contêiner
WORKDIR /app

# Copiar os arquivos publicados do estágio de compilação
COPY --from=build /app/out ./

# Expor a porta em que o aplicativo vai escutar
EXPOSE 5000
EXPOSE 8080
EXPOSE 80

# Iniciar o aplicativo quando o contêiner for executado
ENTRYPOINT ["dotnet", "RinhaBackend.dll"]

# Set environment variables
# ENV ASPNETCORE_ENVIRONMENT="production"

# # Copy files to app directory
# COPY . /app

# # Copy nuget.config to app directory
# #COPY ./NuGet.config /app/Hub2b.Orders.Marketplace.Shopee.Retrieve

# # Set working directory
# WORKDIR /app/RinhaBackend

# # Restore NuGet packages
# RUN ["dotnet", "restore"]

# # Build the app
# RUN dotnet build -c Release -o /bin

# # Open port
# EXPOSE 5000/tcp

# # Run the app
# WORKDIR /bin

# # Run the app
# ENTRYPOINT ["dotnet", "RinhaBackend.dll"]