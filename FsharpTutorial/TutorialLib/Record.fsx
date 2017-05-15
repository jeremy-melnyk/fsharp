type Date = {
    day: int
    month: int
    year: int
}

type Person = {
    firstName: string
    lastName: string
    favClub: string
    myAge: int
    birthday: Date
}

let me = { firstName="John"
           lastName = "Doe" 
           favClub = "Manchester" 
           myAge = 30
           birthday = { day = 1; month = 1; year = 2000 } }

let { firstName = myFirstName } = me
let { lastName = fav } = me

let myFullName = me.firstName + " " + me.lastName

let temp =  { me with firstName = "Bob" }

let addBirthday person birthday =
    let updated = { person with birthday = birthday }
    updated

let birthday = { day = 5; month = 6; year = 2017; }
let updated = addBirthday me birthday




