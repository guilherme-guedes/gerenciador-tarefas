FROM --platform=linux/amd64  mcr.microsoft.com/dotnet/sdk:8.0-alpine-amd64 AS build
WORKDIR /src
COPY . .
RUN dotnet publish "GerenciadorTarefas.csproj" -c Release --use-current-runtime --self-contained true  -o build

FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/runtime:8.0-alpine-amd64
WORKDIR /app
COPY --from=build /src/build .

ENV ASPNETCORE_URLS=http://+:7000
EXPOSE 7000
ENTRYPOINT ["./GerenciadorTarefas"]
