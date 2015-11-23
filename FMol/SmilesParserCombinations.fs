module FMol.SmilesParserCombinations

open FParsec

open FMol.SmilesParserPrimitives

let symbol:Parser<string, unit> = choice [elementSymbol; aromaticSymbol; pstring "*"]

let bracketAtom:Parser<Atom, unit> =
    let patom = parse {
        let! isotope = opt isotope
        let! symbol = symbol
        let! chiral = chiral <|> preturn ""
        let! hCount = hcount <|> preturn 0
        let! charge = charge <|> preturn 0
        let! atomClass = opt atomClass

        return {Isotope = isotope; Symbol = symbol; Chiralty = chiral; hCount = hCount; Charge = charge; AtomClass = atomClass}
    }
    between (pstring "[") (pstring "]" ) patom