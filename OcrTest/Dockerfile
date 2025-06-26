# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything and build
COPY . ./
RUN dotnet publish -c Release -o /out

# Use a smaller runtime image for running the app
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app

# Install dependencies and Tesseract
RUN apt-get update && \
    apt-get install -y tesseract-ocr imagemagick && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

# Allow ImageMagick to process PDFs
RUN sed -i 's/rights="none" pattern="PDF"/rights="read|write" pattern="PDF"/' /etc/ImageMagick-6/policy.xml

# Copy the build output
COPY --from=build /out .

# Copy tessdata and images
COPY tessdata ./tessdata
COPY *.png ./
VOLUME ["/app/images"]

# Set the entrypoint
ENTRYPOINT ["dotnet", "OcrTest.dll"]