# Dê preferência para imagens do nosso ECR ao invés do DockerHub

# build stuff
FROM 289208114389.dkr.ecr.us-east-1.amazonaws.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App
EXPOSE 8080
EXPOSE 80
EXPOSE 443

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM 289208114389.dkr.ecr.us-east-1.amazonaws.com/dotnet/aspnet:7.0

WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "Api.dll"]
