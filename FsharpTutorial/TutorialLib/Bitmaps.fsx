open System.Drawing
open System.IO

let range = 16

let bitmap = new Bitmap(range,range)
let name = "testBitmap"
let extension = ".png"
let fileName = name + extension
let path = Path.Combine(__SOURCE_DIRECTORY__, fileName)

(*for i in 0 .. range - 1 do
    for j in 0 .. range - 1 do
        if i % 2 = 0 && j % 2 = 0 then bitmap.SetPixel(i, j, Color.Aqua)
        else bitmap.SetPixel(i, j, Color.Black)*)

let square (s:int, e:int) =
    for x in s..e do 
        bitmap.SetPixel(x, s, Color.White)
        bitmap.SetPixel(s, x, Color.White)
        bitmap.SetPixel(x, e, Color.White)
        bitmap.SetPixel(e, x, Color.White)

square(2, 10)
bitmap.Save(path, Imaging.ImageFormat.Png)
