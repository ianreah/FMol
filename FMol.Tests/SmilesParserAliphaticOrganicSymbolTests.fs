module FMol.Tests.SmilesParserAliphaticOrganicSymbolTests

open FsCheck
open FsCheck.NUnit

open FMol.Tests.ParserTestHelper
open FMol.Tests.GeneratorHelpers
open FMol.SmilesParserPrimitives

let aliphaticOrganicSymbols = ["Cl"; "Br"; "B"; "C"; "N"; "O"; "S"; "P"; "F"; "I"]

let aliphaticOrganicSymbolGenerator = choiceGenerator aliphaticOrganicSymbols
let notAliphaticOrganicSymbolGenerator = notAnOptionGenerator aliphaticOrganicSymbols

[<PropertyAttribute>]
let validSymbolSucceeds() =
    Prop.forAll (Arb.fromGen aliphaticOrganicSymbolGenerator) (testParserSucceedsWithInput aliphatic_organic)

[<PropertyAttribute>]
let invalidSymbolFails() =
    Prop.forAll (Arb.fromGen notAliphaticOrganicSymbolGenerator) (testParserFails aliphatic_organic)