module FMol.Tests.SmilesParserIsotopeTests

open System
open FsCheck
open FsCheck.NUnit

open FMol.Tests.ParserTestHelper
open FMol.Tests.SmilesGenerators
open FMol.SmilesParserPrimitives

[<PropertyAttribute>]
let validIsotopeSucceeds() =
    Prop.forAll (Arb.fromGen validIsotopeGenerator) (testParserSucceedsWith isotope)

[<PropertyAttribute>]
let isotopeOutOfRangeFails() =
    Prop.forAll (Arb.fromGen outOfRangeIsotopeGenerator) (testParserFails isotope)

[<PropertyAttribute>]
let invalidIsotopeFails() =
    Prop.forAll (Arb.fromGen invalidIsotopeGenerator) (testParserFails isotope)