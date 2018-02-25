# TwoCansImagifier
AutoHotkey-based profile pic converter for Two Cans & String.

## Overview

This tool allows you to set your profile image on Two Cans & String. It has two components:
1) A C# GUI for converting a 32-by-32 image into an AutoHotkey array file
2) An [AutoHotkey](https://autohotkey.com/) script to enter the resulting data into the Two Cans [profile image editor](https://twocansandstring.com/profile/draw)

Unfortunately, this solution is Windows-only because AutoHotkey is Windows-only.

## Usage

1. Install [AutoHotkey](https://autohotkey.com/).
2. Download the latest [release](https://github.com/jonathansharman/TwoCansImagifier/releases) of Two Cans Imagifier.
2. Create a 32-by-32 version of your desired profile picture.
3. Run TwoCansImageConverter.exe.
    1. Import the image you created.
    2. Export the data to the directory containing imagifier.ahk as "data.ahk".
3. Open the [profile image editor](https://twocansandstring.com/profile/draw) page.
4. Run imagifier.ahk.
    3. Focus the image editor window and click "OK".
    4. Click somewhere in the white area to the left of the image.
    5. Wait. This part is pretty laggy, so it may take a minute before the image fully renders on screen.
    6. Click "OK".
