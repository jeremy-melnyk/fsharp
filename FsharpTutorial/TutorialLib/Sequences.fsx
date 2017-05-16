let first = seq { for i in 0..10..100 -> seq {0..i}}
let second = seq { for i in 0..10..100 do yield! seq {0..i}}