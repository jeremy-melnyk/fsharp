let location = (77.51, 166.40, 21)

let longAlt location =
    let (long,_,alt) = location
    (alt, long)

printfn "%A" (snd (longAlt location))
