open System.Drawing
open System.IO
open System

type Plotter = {
    position: int*int
    color: Color
    direction: float
    bitmap: Bitmap
}

let betterLine (x1,y1) plotter =
    let updatedPlotter = { plotter with position = x1,y1 }
    let bitmap = plotter.bitmap
    let color = plotter.color
    let bitmapSize = bitmap.Size.Height - 1
    let x0,y0 = plotter.position 
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
            let y = if yReverse && not xReverse then bitmapSize-(y1-increment) else bitmapSize-(y0+increment)                
            bitmap.SetPixel(x, y, color)
    elif yLen >= xLen && yLen > 0.0 then
        for y in y0..y1 do
            let ratio = float(y-y0)/yLen
            let increment = int(Math.Round(ratio*xLen))
            let x = if yReverse && not xReverse then x1 - increment else x0 + increment
            let y = if xReverse && not yReverse then bitmapSize-(y1-y) else bitmapSize-y
            bitmap.SetPixel(x, y, color)
    updatedPlotter

let turn degrees plotter = 
    let newDir = (plotter.direction + degrees) % 360.0
    let angled = { plotter with direction = newDir }
    printfn "%A" (newDir)
    angled

let move dist plotter =
    let curPos = plotter.position
    let angle = plotter.direction
    let startX = fst curPos
    let startY = snd curPos
    let rads = angle * (Math.PI/180.0)
    let endX = int(Math.Round((float startX) + ((float dist) * cos rads)))
    let endY = int(Math.Round((float startY) + ((float dist) * sin rads)))
    let plotted = betterLine (endX, endY) plotter
    plotted

let path = Path.Combine(__SOURCE_DIRECTORY__, "plotter.png")

let initialPlotter = {
    position = 255,255
    color = Color.OrangeRed
    direction = 0.0
    bitmap = new Bitmap(512, 512)
}
(*bounds*)
let bitmap = initialPlotter.bitmap
bitmap.SetPixel(0, 0, Color.AliceBlue)
bitmap.SetPixel(0, 511, Color.AliceBlue)
bitmap.SetPixel(511, 0, Color.AliceBlue)
bitmap.SetPixel(511, 511, Color.AliceBlue)

(*draw the thing*)
let mutable newPlotter = (move 100 initialPlotter)
newPlotter <- (turn 90.0 newPlotter)
newPlotter <- (move 100 newPlotter)
newPlotter <- (turn 90.0 newPlotter)
newPlotter <- (move 100 newPlotter)
newPlotter <- (turn 90.0 newPlotter)
newPlotter <- (move 100 newPlotter)
newPlotter.bitmap.Save(path)


let pubPlotter = {
    position = 5,10
    color = Color.White
    direction = 90.0
    bitmap = new Bitmap(100,30)
}

let pubBitmap = pubPlotter.bitmap
pubBitmap.SetPixel(0, 0, Color.AliceBlue)
pubBitmap.SetPixel(0, 29, Color.AliceBlue)
pubBitmap.SetPixel(99, 0, Color.AliceBlue)
pubBitmap.SetPixel(99, 29, Color.AliceBlue)

let newPath = Path.Combine(__SOURCE_DIRECTORY__, "pub.png")
let piedPiper = 
    pubPlotter
    |> move 15
    |> turn -90.0
    |> move 60
    |> turn -90.0
    |> move 20
    |> turn 90.0
    |> move 30
    |> turn 90.0
    |> move 15
piedPiper.bitmap.Save(newPath)
