module Fmol.Tests.SmilesParserBracketAtomTests

open FsCheck
open NUnit.Framework
open FsCheck.NUnit

open FMol.Tests.ParserTestHelper
open FMol.Tests.SmilesGenerators
open FMol.SmilesParserCombinations

[<PropertyAttribute>]
let validBracketAtomPasses() =
    Prop.forAll (Arb.fromGen validBracketAtomGenerator) (testParserSucceedsWith bracketAtom)