module FMol.Tests.SmilesParserAromaticOrganicSymbolTests

open FsCheck
open FsCheck.NUnit

open FMol.Tests.ParserTestHelper
open FMol.Tests.GeneratorHelpers
open FMol.SmilesParserPrimitives

let aromaticOrganicSymbols = ["b"; "c"; "n"; "o"; "s"; "p"]

let aromaticOrganicSymbolGenerator = choiceGenerator aromaticOrganicSymbols
let notAnAromaticOrganicSymbolGenerator = notAnOptionGenerator aromaticOrganicSymbols

[<PropertyAttribute>]
let validSymbolSucceeds() =
    Prop.forAll (Arb.fromGen aromaticOrganicSymbolGenerator) (testParserSucceedsWithInput aromatic_organic)

[<PropertyAttribute>]
let invalidSymbolFails() =
    Prop.forAll (Arb.fromGen notAnAromaticOrganicSymbolGenerator) (testParserFails aromatic_organic)