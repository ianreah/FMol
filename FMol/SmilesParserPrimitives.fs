module FMol.SmilesParserPrimitives

open FParsec

let isotope:Parser<uint32, unit> = parse {
    let! n = puint32
    if n <= 999u then return n
}