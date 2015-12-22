module FMol.SmilesParserCombinations

open FParsec

open FMol.SmilesParserPrimitives

let private unknown = pstring "*"

let symbol:Parser<string, unit> = choice [elementSymbol; aromaticSymbol; unknown]

let bracketAtom:Parser<Atom, unit> =
    let patom = parse {
        let! isotope = opt isotope
        let! symbol = symbol
        let! chiral = chiral <|> preturn ""
        let! hCount = hcount <|> preturn 0
        let! charge = charge <|> preturn 0
        let! atomClass = opt atomClass

        return {Isotope = isotope; Symbol = symbol; Chiralty = chiral; hCount = Count(hCount); Charge = charge; AtomClass = atomClass}
    }
    between (pstring "[") (pstring "]" ) patom

let atom:Parser<Atom, unit> =
    let makeAtom s =
        preturn {Isotope = None; Symbol = s; Chiralty = ""; hCount = Implicit; Charge = 0; AtomClass = None}
    bracketAtom <|> (choice [aliphatic_organic; aromatic_organic; unknown] >>= makeAtom)