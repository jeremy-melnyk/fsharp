open System.Drawing
open System.IO
open System

let path = Path.Combine(__SOURCE_DIRECTORY__, "envelope.png")
let naiveLine (x0,y0) (x1,y1) color (bitmap:Bitmap) =
    let xLen = float(x1 - x0)
    let yLen = float(y1 - y0)
    if xLen <> 0.0 then
        for x in x0..x1 do
            let proportion = float(x - x0) / xLen
            let y = (int (Math.Round(proportion * yLen))) + y0
            bitmap.SetPixel(x, y, color)
    if yLen <> 0.0 then
        for y in y0..y1 do
            let proportion = float(y - y0) / yLen
            let x = (int (Math.Round(proportion * xLen))) + x0
            bitmap.SetPixel(x, y, color)

let betterLine (x0,y0) (x1,y1) (color) (bitmap:Bitmap) =
    let bitmapSize = bitmap.Size.Height - 1
    let xLen = float(abs(x1-x0))
    let yLen = float(abs(y1-y0))
    let xReverse = if x0 > x1 then true else false
    let yReverse = if y0 > y1 then true else false
    let x0,x1 = if x0 > x1 then x1,x0 else x0,x1
    let y0,y1 = if y0 > y1 then y1,y0 else y0,y1
    if xLen > yLen && xLen > 0.0 then
        for x in x0..x1 do
            let ratio = float(x-x0)/xLen
            let increment = int(Math.Round(ratio*yLen))
            let x = if xReverse && not yReverse then x1 - x else x
            let y = if yReverse && not xReverse then bitmapSize-(y1-increment) else bitmapSize-(y0+increment)
            bitmap.SetPixel(x, y, color)
    elif yLen >= xLen && yLen > 0.0 then
        for y in y0..y1 do
            let ratio = float(y-y0)/yLen
            let increment = int(Math.Round(ratio*xLen))
            let x = if yReverse && not xReverse then x1 - increment else x0 + increment
            let y = if xReverse && not yReverse then bitmapSize-(y1-y) else bitmapSize-y
            bitmap.SetPixel(x, y, color)

let bitmap = new Bitmap(256, 256)
(*bounds*)
bitmap.SetPixel(0, 0, Color.AliceBlue)
bitmap.SetPixel(0, 255, Color.AliceBlue)
bitmap.SetPixel(255, 0, Color.AliceBlue)
bitmap.SetPixel(255, 255, Color.AliceBlue)
(*cool envelope*)
betterLine (0,75) (0,255) Color.White bitmap
betterLine (0,75) (255,75) Color.White bitmap
betterLine (255,75) (255,255) Color.White bitmap
betterLine (0,255) (255,255) Color.White bitmap

betterLine (0,255) (127,127) Color.Red bitmap
betterLine (127,127) (255,255) Color.Red bitmap

bitmap.Save(path)
