﻿module FMol.Tests.SmilesParserAtomTests

open FsCheck
open NUnit.Framework
open FsCheck.NUnit

open FMol.Tests.ParserTestHelper
open FMol.Tests.SmilesGenerators
open FMol.SmilesParserCombinations

[<PropertyAttribute>]
let validAtomPasses() =
    Prop.forAll (Arb.fromGen atomGenerator) (testParserSucceedsWith atom)