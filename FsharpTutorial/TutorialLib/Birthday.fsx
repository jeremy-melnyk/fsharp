let birthday = (15,01,2018)

let birthdayFlip birthday =
    let (dd,mm,yy) = birthday
    (yy,mm,dd)

let s = birthdayFlip birthday
printfn "%A" (s)
s.ToString()
