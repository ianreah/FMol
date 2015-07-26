module FMol.Tests.SmilesParserIsotopeTests

open System
open FsCheck
open FsCheck.NUnit

open FMol.Tests.ParserTestHelper
open FMol.Tests.Generators
open FMol.SmilesParserPrimitives

let validIsotopeGenerator = gen {
    // A general-purpose SMILES parser must accept at least three digits
    // for the isotope and values from 0 to 999
    let! validIsotope = Gen.choose (0, 999)

    // An isotope is interpreted as a number, so that [2H], [02H] and
    // [002H] all mean deuterium.
    let! padWithZeros = Arb.generate<bool>
    let! zeroCount = if padWithZeros then Arb.generate<uint32> else (Gen.constant 0u)
    let parserInput = String.Format("{0}{1}", (String.replicate (int zeroCount) "0"), validIsotope)

    return parserInput, validIsotope;
}

let outOfRangeIsotopeGenerator = gen {
    let! neg = Arb.generate<bool>
    let! invalidIsotope = Gen.suchThat (fun x -> if neg then x < 0 else x > 999) Arb.generate<int>
    return invalidIsotope.ToString()
} 

let invalidIsotopeGenerator = notStartWithGenerator [| '0'..'9' |]

[<PropertyAttribute>]
let validIsotopeSucceeds() =
    Prop.forAll (Arb.fromGen validIsotopeGenerator) (testParserSucceedsWith isotope)

[<PropertyAttribute>]
let isotopeOutOfRangeFails() =
    Prop.forAll (Arb.fromGen outOfRangeIsotopeGenerator) (testParserFails isotope)

[<PropertyAttribute>]
let invalidIsotopeFails() =
    Prop.forAll (Arb.fromGen invalidIsotopeGenerator) (testParserFails isotope)