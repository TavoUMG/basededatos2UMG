#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 443

ENV ASPNETCORE_ENVIRONMENT "Production"
ENV ASPNETCORE_URLS=http://*:8080

#Configuración Base de Datos
ENV DB_DRIVE "SQLServer"
ENV CONEXION_STRING ""
ENV DB_HOST "34.172.4.22"
ENV DB_USER "sqlserver"
ENV DB_PASS "@Dmin12345."
ENV DB_NAME "db_proyecto"

#Variables de entorno
ENV APP_NAME "Sistema Tavo"
ENV version "1.0"
ENV versionInstalada "1.0"
ENV versionRegistrada "1.0"

ENV MailSmtpHost "smtp.gmail.com"
ENV MailSmtpPort "465"
ENV MailSmtpUseSSL "true"
ENV MailSmtpFromName "Sistema"
ENV MailSmtpUserName "emisor.tarea@gmail.com"
ENV MailSmtpPassword "comunicacionesaa"
ENV MailSmtpCreateMessageId "false"

ENV RecoverPasswordURL "https://localhost:49167/Home/CambiarPassword/{0}/{1}"

#Tiempo de Session
ENV Session "15"

#Google Cloud Data Protection
ENV IsGoogleCloud "false"
ENV Bucket "tofarmastorage"
ENV Object "DataProtectionProviderKeys.xml"
ENV KmsKeyName "projects/tofarma-383813/locations/global/keyRings/dataprotectionprovider/cryptoKeys/key"

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Sistema.csproj", "."]
RUN dotnet restore "./Sistema.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Sistema.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sistema.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sistema.dll"]