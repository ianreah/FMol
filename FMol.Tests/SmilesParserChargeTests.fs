module FMol.Tests.SmilesParserChargeTests

open System
open FsCheck
open FsCheck.NUnit

open FMol.Tests.GeneratorHelpers
open FMol.Tests.ParserTestHelper
open FMol.SmilesParserPrimitives

// Charge is speciﬁed by a +n or -n where n is a number; if the number is missing, it means either +1 or -1 as appropriate.
// An implementation is required to accept charges in the range -15 to +15.
let chargeGenerator = gen {
    let! polarity =  choiceGenerator [("+", id); ("-", (*)-1)]
    let! excludeNumber = Arb.generate<bool>
    if excludeNumber then
        return ((fst polarity), (snd polarity) 1)
    else
        let! n = Gen.choose (0, 15)
        return (String.Format("{0}{1}", fst polarity, n), (snd polarity) n)
}

let tooBigChargeGenerator = gen {
    let! n = Gen.suchThat (fun x -> x > 15 || x < -15) Arb.generate<int>
    return string n
}

let invalidChargeGenerator = notStartWithGenerator [|'+'; '-'|]

[<PropertyAttribute>]
let validChargeSucceeds() =
    Prop.forAll (Arb.fromGen chargeGenerator) (testParserSucceedsWith charge)

[<PropertyAttribute>]
let tooBigChargeFails() =
    Prop.forAll (Arb.fromGen tooBigChargeGenerator) (testParserFails charge)

[<PropertyAttribute>]
let invalidChargeFails() =
    Prop.forAll (Arb.fromGen invalidChargeGenerator) (testParserFails charge)
