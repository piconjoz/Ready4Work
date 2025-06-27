# ---------- build stage ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy project file and restore first
COPY OcrWEB.csproj .
RUN dotnet restore OcrWEB.csproj

# copy the remaining source
COPY . .
RUN dotnet publish OcrWEB.csproj -c Release -o /out

# runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Install Tesseract, ImageMagick, and Poppler (pdftoppm)
RUN apt-get update && \
    apt-get install -y tesseract-ocr imagemagick poppler-utils && \
    sed -i 's/rights="none" pattern="PDF"/rights="read|write" pattern="PDF"/' /etc/ImageMagick-6/policy.xml && \
    rm -rf /var/lib/apt/lists/*

# Copy app
COPY --from=build /out ./

# Copy Tesseract language data
COPY tessdata ./tessdata

# Expose HTTP port
EXPOSE 80

ENTRYPOINT ["dotnet", "OcrWEB.dll"]