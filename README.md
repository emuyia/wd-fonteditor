# White Day Font Editor

## Introduction

![wdfe_1.2](https://i.imgur.com/LyU4n4r.jpg)

White Day Font Editor is a .NET desktop application for editing the proprietary font files (FNT and WFT) for an old Korean horror game named White Day (2001). The application provides a UI for managing these files, exporting parts of the font's characters into BMP files, and then importing edited versions back into the game.

## Features

1. **Load Font Files:** The application allows you to load font files (FNT and WFT) of the game. It then reads the byte data and prepares it for further editing.

2. **Export to BMP:** The loaded font files can be exported to BMP files for each character. This enables the editing of font characters using any image editor.

3. **Import from BMP:** After editing the BMP files, they can be imported back into the application. The application will convert the edited BMP files back into the proprietary font files.

4. **Save Font Files:** The edited font files can be saved and used in the game.

## Code Overview

The application consists of two main files: `FontEditor.cs` and `Program.cs`.

`FontEditor.cs` contains the `FontEditor` class, which represents the main form of the application. It provides the implementation for loading, exporting, and importing font files, along with saving the modified files.

`Program.cs` includes the `Program` class, which serves as the entry point for the application. It also includes the `FontParameters` class, which is used to store the state of the currently loaded font file, including details such as the byte data, pixel width, and padding related values. The `Utils` class provides utility methods used across the application. The `BitmapExtensions` class contains extension methods for the `Bitmap` class to support various operations, including cropping, resizing, and converting between Bitmap and byte array.

## How to Use

1. Launch the application. You will be presented with an interface with several buttons.

2. Click on "Load Font" to load a font file (FNT or WFT) from the game. 

3. Select a character from the loaded font file that you want to edit.

4. Click on "Export BMP" to export the selected character to a BMP file.

5. Edit the BMP file using any image editor of your choice.

6. Back in the application, click on "Import BMP" to load the edited BMP file.

7. Repeat steps 3-6 for all the characters that you want to edit.

8. After you've finished editing the characters, click on "Save Font" to save the modified font file.

9. You can now use this modified font file in the game.