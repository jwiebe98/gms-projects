#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime-deps:5.0-alpine3.12-amd64
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV DOTNET_HOSTBUILDER__RELOADCONFIGONCHANGE=false
COPY /app/publish/individual-hmc-backend  .
ENTRYPOINT ["./individual-hmc-backend"]