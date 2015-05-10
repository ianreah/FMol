module FMol.Parsers

open System
open FParsec

let pint32Range min max = parse {
    let! n = pint32
    if min <= n && n <= max then return n
}

let prefixSuffix pPrefix pSuffix = parse {
    let! prefix = pPrefix
    let! suffix = pSuffix
    return String.Format("{0}{1}", prefix, suffix)
}
