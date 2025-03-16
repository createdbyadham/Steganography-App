# Image Steganography Application

A C# Windows Forms application that allows users to hide text messages within images using steganography techniques.

## Overview

This application implements the LSB (Least Significant Bit) steganography method to embed text messages within image files without visibly altering the appearance of the image. The hidden messages can later be extracted by the same application.

## Features

- **Hide Text in Images**: Encode text messages within PNG or BMP image files
- **Extract Hidden Messages**: Decode and retrieve text that has been hidden in images
- **User-Friendly Interface**: Simple Windows Forms interface for easy operation
- **Optimized Performance**: Direct bitmap data access for efficient processing of large images

## How to run the app?

```bash
# Navigate to the project directory
cd /path/to/SteganographyApp (replace with your own path)

# Run the application
dotnet run
```

## Requirements

- .NET 6.0 or higher
- Windows operating system

## Usage

### Hiding a Message

1. Click "Load Image" to select an image file
2. Enter the message you want to hide in the text box
3. Click "Encode" to embed the message in the image
4. Click "Save Image" to save the image with the hidden message (use PNG or BMP format for best results)

### Retrieving a Message

1. Click "Load Image" to select an image that contains a hidden message
2. Click "Decode" to extract and display the hidden message

## Technical Details

The application uses the following steganography technique:

- Converts the message to binary data
- Stores the message length as a 32-bit integer at the beginning
- Modifies the least significant bit of each color channel (R, G, B) to store the binary data
- Uses direct bitmap data access for improved performance

## Limitations

- The size of the message that can be hidden depends on the dimensions of the image
- JPEG format is not recommended as its lossy compression can destroy the hidden data
- The application works best with PNG and BMP formats which use lossless compression

## License

[MIT License](LICENSE)"# Steganography-App" 
