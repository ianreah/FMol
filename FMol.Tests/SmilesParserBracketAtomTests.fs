module Fmol.Tests.SmilesParserBracketAtomTests

open FsCheck
open NUnit.Framework
open FsCheck.NUnit

open FMol.Tests.ParserTestHelper
open FMol.Tests.SmilesGenerators
open FMol.SmilesParserCombinations

let private failsBracketAtomParser generator = Prop.forAll (Arb.fromGen (generator |> Gen.map fst)) (testParserFails bracketAtom)

[<PropertyAttribute>]
let validBracketAtomPasses() =
    Prop.forAll (Arb.fromGen validBracketAtomGenerator) (testParserSucceedsWith bracketAtom)

[<PropertyAttribute>]
let bracketAtomWithInvalidIsotopeFails() =
    bracketAtomGeneratorWithInvalidIsotope |> failsBracketAtomParser

[<PropertyAttribute>]
let bracketAtomWithInvalidSymbolFails() =
    bracketAtomGeneratorWithInvalidSymbol |> failsBracketAtomParser

[<PropertyAttribute>]
let bracketAtomWithInvalidChiraltyFails() =
    bracketAtomGeneratorWithInvalidChiralty |> failsBracketAtomParser

[<PropertyAttribute>]
let bracketAtomWithInvalidHCountFails() =
    bracketAtomGeneratorWithInvalidHCount |> failsBracketAtomParser

[<PropertyAttribute>]
let bracketAtomWithInvalidChargeFails() =
    bracketAtomGeneratorWithInvalidCharge |> failsBracketAtomParser

[<PropertyAttribute>]
let bracketAtomWithInvalidAtomClassFails() =
    bracketAtomGeneratorWithInvalidAtomClass |> failsBracketAtomParser