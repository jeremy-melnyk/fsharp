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
    let xLen = float(x1-x0)
    let yLen = float(y1-y0)

    let x0,y0,x1,y1 = if x0 > x1 then x1,y1,x0,y0 else x0,y0,x1,y1
    if xLen <> 0.0 then
        for x in x0..x1 do
            let ratio = float(x-x0)/xLen
            let increment = int(Math.Round(ratio*yLen))
            let y = increment + y0           
            bitmap.SetPixel(x, y, color)

    let x0,y0,x1,y1 = if y0 > y1 then x1,y1,x0,y0 else x0,y0,x1,y1
    if yLen <> 0.0 then
        for y in y0..y1 do
            let ratio = float(y-y0)/yLen
            let increment = int(Math.Round(ratio*xLen))
            let x = increment + x0
            bitmap.SetPixel(x, y, color)
    updatedPlotter

let turn degrees plotter = 
    let newDir = (plotter.direction + degrees) % 360.0
    let angled = { plotter with direction = newDir }
    angled

let move dist plotter =
    let curPos = plotter.position
    let angle = plotter.direction
    let startX = fst curPos
    let startY = snd curPos
    let rads = (angle - 90.0) * (Math.PI/180.0)
    let endX = int(Math.Round((float startX) + ((float dist) * cos rads)))
    let endY = int(Math.Round((float startY) + ((float dist) * sin rads)))
    let plotted = betterLine (endX, endY) plotter
    plotted

let path = Path.Combine(__SOURCE_DIRECTORY__, "spirograph.png")
let bitmapSize = 512
let plotter = {
    position = 212,212
    color = Color.OrangeRed
    direction = 90.0
    bitmap = new Bitmap(bitmapSize, bitmapSize)
}

(*bounds*)
let bitmap = plotter.bitmap
bitmap.SetPixel(0, 0, Color.AliceBlue)
bitmap.SetPixel(0, bitmapSize-1, Color.AliceBlue)
bitmap.SetPixel(bitmapSize-1, 0, Color.AliceBlue)
bitmap.SetPixel(bitmapSize-1, bitmapSize-1, Color.AliceBlue)

let polygon (sides:int) length plotter =
    let angle = Math.Round(360.0/float sides)
    Seq.fold(fun s i -> turn angle (move length s)) plotter [1.0..(float sides)]

let generate cmdsStripe times fromPlotter =
    let cmdsGen = 
        seq { while true do yield! cmdsStripe }
    let cmds = cmdsGen |> Seq.take times
    cmds |> Seq.fold (fun plot cmd -> cmd plot) fromPlotter

let cmdsStripe = 
    [ 
      move 15
      turn 15.0
      polygon 3 10
    ]

generate cmdsStripe 1 plotter
plotter.bitmap.Save(path)
