let childAges = 2,4

let addChildAges ages = 
    let bob,sam = ages
    bob + sam

let addAges bob sam =
    bob + sam

let singleAge mark = 
    mark

let complexAges ageOne ageTwo ageThree ageFour = 
    ageOne * ageTwo + ageThree, ageOne + ageTwo + ageThree, ageFour / ageOne

let triangleArea (b:float) (h:float) = 
    (b*h)/2.0

let mutate a:float =
    a + 1.0