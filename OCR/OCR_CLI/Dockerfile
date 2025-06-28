# build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . ./
RUN dotnet publish -c Release -o /out

# runtime stage
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app

# Install Tesseract and ImageMagick
RUN apt-get update && \
    apt-get install -y tesseract-ocr imagemagick && \
    sed -i 's/rights="none" pattern="PDF"/rights="read|write" pattern="PDF"/' /etc/ImageMagick-6/policy.xml && \
    rm -rf /var/lib/apt/lists/*

# Copy app
COPY --from=build /out ./

# Copy Tesseract language data
COPY tessdata ./tessdata

# Declare images folder mount point for runtime input
VOLUME ["/app/images"]

ENTRYPOINT ["dotnet", "OcrCLI.dll"]