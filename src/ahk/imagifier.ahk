#CommentFlag //
SendMode input

// Include definition for image array.
#Include data.ahk

// Sets the current draw color to the given RGB array value. Color elements are in [0, 255].
set_color(rgb) {
	global origin_x
	global origin_y

	static top_offset := 1
	static bottom_offset := 320
	static rgb_offsets := [415, 475, 535]

	Loop, 3 {
		click_x := origin_x + rgb_offsets[A_index]
		click_y := origin_y + top_offset + (bottom_offset - top_offset) * rgb[A_index] / 255
		Click, %click_x%, %click_y%
	}
}

// Sets the pixel at the given 0-based image coordinates to the current color.
set_pixel(x, y) {
	global origin_x
	global origin_y

	static pixel_size := 10
	static pixel_offset := 4

	click_x := origin_x + x * pixel_size + pixel_offset
	click_y := origin_y + y * pixel_size + pixel_offset
	Click, %click_x%, %click_y%
}

// Converts an RGB AutoHotkey color to an RGB array.
rgb_color_to_rgb_array(color) {
	return [((color & 0xFF0000) >> 16), ((color & 0xFF00) >> 8), (color & 0xFF)]
}

// Get the origin coordinates.
MsgBox Click to the left of the image border.
KeyWait LButton, D
MouseGetPos origin_x, origin_y
while (color != 0) {
	// Move right and recheck.
	origin_x++
	PixelGetColor color, %origin_x%, %origin_y%
	MouseMove, %origin_x%, %origin_y%
}
PixelGetColor color, %origin_x%, %origin_y%
while (color == 0) {
	// Move up and recheck.
	origin_y--
	PixelGetColor color, %origin_x%, %origin_y%
	MouseMove, %origin_x%, %origin_y%
}
origin_y++
MouseMove, %origin_x%, %origin_y%

// Loop over image.
x := 0
y := 0
image_res = 32
loop_count := image_res * image_res
Loop, %loop_count% {
	// Set color.
	set_color(image[A_index])

	// Draw.
	set_pixel(x, y)

	x++
	if (x == IMAGE_RES) {
		x := 0
		y++
	}
}
// Exit.
ExitApp
