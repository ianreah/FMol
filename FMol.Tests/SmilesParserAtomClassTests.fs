module Fmol.Tests.SmilesParserAtomClassTests

open System
open FsCheck
open NUnit.Framework
open FsCheck.NUnit

open FMol.Tests.ParserTestHelper
open FMol.Tests.SmilesGenerators
open FMol.SmilesParserPrimitives


[<PropertyAttribute>]
let validAtomClassSucceeds() =
    Prop.forAll (Arb.fromGen validAtomClassGenerator) (testParserSucceedsWith atomClass)

[<PropertyAttribute>]
let invalidAtomClassFails() =
    Prop.forAll (Arb.fromGen invalidAtomClassGenerator) (testParserFails atomClass)