module FMol.SmilesParserCombinations

open FParsec

open FMol.SmilesParserPrimitives

let symbol:Parser<string, unit> = choice [elementSymbol; aromaticSymbol; pstring "*"]