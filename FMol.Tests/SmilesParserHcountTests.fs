module FMol.Tests.SmilesParserHcountTests

open System
open FsCheck
open FsCheck.NUnit

open FMol.Tests.ParserTestHelper
open FMol.Tests.Generators
open FMol.SmilesParserPrimitives

let hCountGenerator = gen {
    // If H is speciﬁed without a number, it is identical to H1. 
    let! excludeNumber = Arb.generate<bool>
    if excludeNumber then
        return ("H", 1)
    else
        // Hydrogens inside of brackets are speciﬁed as Hn where n is a number such as H3. 
        let! n = Gen.choose (0, 9)
        return (String.Format("H{0}", n), n)
}

let invalidhCountGenerator = notStartWithGenerator [|'H'|]

[<PropertyAttribute>]
let validHcountSucceeds() =
    Prop.forAll (Arb.fromGen hCountGenerator) (testParserSucceedsWith hcount)

[<PropertyAttribute>]
let invalidHcountFails() =
    Prop.forAll (Arb.fromGen invalidhCountGenerator) (testParserFails hcount)