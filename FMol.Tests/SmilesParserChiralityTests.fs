module FMol.Tests.SmilesParserChiralityTests

open System
open FsCheck
open NUnit.Framework
open FsCheck.NUnit

open FMol.Tests.ParserTestHelper
open FMol.Tests.Generators

open FMol.SmilesParserPrimitives

let chiralityGenerator = gen {
    let! prefix, min, max = choiceGenerator [("@", 0, 0); ("@@", 0, 0); ("@TH", 1, 2); ("@AL", 1, 2); ("@SP", 1, 3); ("@TB", 1, 99); ("@OH", 1, 99)]
    let! n = Gen.choose (min, max)
    if n=0 then return prefix
    else return String.Format("{0}{1}", prefix, n)
}

[<Test>]
let parsesClockwiseChiralityShorthand() =
    Assert.That(testParserSucceedsWithInput chiral "@@", Is.True)

[<Test>]
let parsesAnticlockwiseChiralityShorthand() =
    Assert.That(testParserSucceedsWithInput chiral "@", Is.True)

[<Test>]
let parsesSquarePlanerChirality() =
    Assert.That(
        testParserSucceedsWithInput chiral "@SP1" &&
        testParserSucceedsWithInput chiral "@SP2" &&
        testParserSucceedsWithInput chiral "@SP3", Is.True)

[<PropertyAttribute>]
let failsWithInvalidSquarePlanerChirality n =
    (n < 1 || n > 3) ==> testParserFails chiral (String.Format("@SP{0}", n))

[<Test>]
let parsesTetrahedralChirality() =
    Assert.That(
        testParserSucceedsWithInput chiral "@TH1" &&
        testParserSucceedsWithInput chiral "@TH2", Is.True)

[<PropertyAttribute>]
let failsWithInvalidTetrahedralChirality n =
    (n < 1 || n > 2) ==> testParserFails chiral (String.Format("@TH{0}", n))

[<Test>]
let parsesAllenaleChirality() =
    Assert.That(
        testParserSucceedsWithInput chiral "@AL1" &&
        testParserSucceedsWithInput chiral "@AL2", Is.True)

[<PropertyAttribute>]
let failsWithInvalidAllenaleChirality n =
    (n < 1 || n > 2) ==> testParserFails chiral (String.Format("@AL{0}", n))

[<PropertyAttribute>]
let parsesTrigonalBipyramidalChirality n =
    (1 <= n && n <= 99) ==> testParserSucceedsWithInput chiral (String.Format("@TB{0}", n))

[<PropertyAttribute>]
let failsWithInvalidTrigonalBipyramidalChirality n =
    (n < 1 || n > 99) ==> testParserFails chiral (String.Format("@TB{0}", n))

[<PropertyAttribute>]
let parsesOctahedralChirality n =
    (1 <= n && n <= 99) ==> testParserSucceedsWithInput chiral (String.Format("@OH{0}", n))

[<PropertyAttribute>]
let failsWithInvalidOctahedralChirality n =
    (n < 1 || n > 99) ==> testParserFails chiral (String.Format("@OH{0}", n))
