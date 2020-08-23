FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

LABEL maintainer="jack@hanlin.com.tw,rememberjack@gmail.com"

# Official recommended setup
RUN apt-get update \
	&& apt-get install -y \
		libc6-dev \
		libgdiplus \
	&& rm -rf /var/lib/apt/lists/*
  
# Original setup from itembank-export
# RUN apt-get update && apt-get install -y libgif-dev autoconf libtool automake build-essential gettext libglib2.0-dev libcairo2-dev libtiff-dev libexif-dev unzip
# RUN git clone https://github.com/mono/libgdiplus.git
# RUN cd libgdiplus && ./autogen.sh && make && make install
# RUN cp /usr/local/lib/libgdiplus.s*  /usr/lib/x86_64-linux-gnu/
  
# Official recommended setup with Mono
# https://www.e-iceblue.cn/spiredoc/how-to-use-spire-doc-on-net-core.html
# https://github.com/dotnet/dotnet-docker/issues/618
# Per tech support, install Mono to prevent certain documents from crashing Spire.
# RUN apt update \
#     && apt install -y apt-transport-https dirmngr gnupg ca-certificates \
# 	&& apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
# 
# RUN echo "deb https://download.mono-project.com/repo/debian stable-stretch main" | tee /etc/apt/sources.list.d/mono-official-stable.list \
# 	&& apt update \
# 	&& apt install -y \
# 		libc6-dev \
# 		libgdiplus \
# 		mono-devel \
#     && rm -rf /var/lib/apt/lists/*
# 
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