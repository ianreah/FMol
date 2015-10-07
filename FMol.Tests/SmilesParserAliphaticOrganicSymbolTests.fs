module FMol.Tests.SmilesParserAliphaticOrganicSymbolTests

open FsCheck
open FsCheck.NUnit

open FMol.Tests.ParserTestHelper
open FMol.Tests.SmilesGenerators
open FMol.SmilesParserPrimitives


[<PropertyAttribute>]
let validSymbolSucceeds() =
    Prop.forAll (Arb.fromGen aliphaticOrganicSymbolGenerator) (testParserSucceedsWithInput aliphatic_organic)

[<PropertyAttribute>]
let invalidSymbolFails() =
    Prop.forAll (Arb.fromGen notAliphaticOrganicSymbolGenerator) (testParserFails aliphatic_organic)