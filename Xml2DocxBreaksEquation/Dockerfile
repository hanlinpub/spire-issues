FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

LABEL maintainer="jack@hanlin.com.tw,rememberjack@gmail.com"

# Official recommended setup
RUN apt-get update \
	&& apt-get install -y \
		libc6-dev \
		libgdiplus \
	&& rm -rf /var/lib/apt/lists/*
  
WORKDIR /app

# copy csproj and restore as distinct layers
COPY ./*.csproj .
RUN dotnet restore

COPY . .
RUN dotnet build

FROM build AS testrunner
ENTRYPOINT ["dotnet", "test"]

FROM build AS test
RUN dotnet test -v n

FROM scratch AS export-stage
COPY --from=test /app/bin/Debug/netcoreapp3.1/test-outputs /