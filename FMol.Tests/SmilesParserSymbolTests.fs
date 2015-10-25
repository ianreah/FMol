module FMol.Tests.SmilesParserSymbolTests

open FsCheck
open NUnit.Framework
open FsCheck.NUnit

open FMol.Tests.ParserTestHelper
open FMol.Tests.SmilesGenerators
open FMol.SmilesParserCombinations

[<PropertyAttribute>]
let validSymbolPasses() =
    Prop.forAll (Arb.fromGen symbolGenerator) (testParserSucceedsWithInput symbol)