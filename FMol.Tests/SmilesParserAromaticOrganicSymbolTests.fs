module FMol.Tests.SmilesParserAromaticOrganicSymbolTests

open FsCheck
open FsCheck.NUnit

open FMol.Tests.ParserTestHelper
open FMol.Tests.SmilesGenerators
open FMol.SmilesParserPrimitives


[<PropertyAttribute>]
let validSymbolSucceeds() =
    Prop.forAll (Arb.fromGen aromaticOrganicSymbolGenerator) (testParserSucceedsWithInput aromatic_organic)

[<PropertyAttribute>]
let invalidSymbolFails() =
    Prop.forAll (Arb.fromGen notAnAromaticOrganicSymbolGenerator) (testParserFails aromatic_organic)