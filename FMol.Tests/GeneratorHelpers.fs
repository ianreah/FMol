module FMol.Tests.GeneratorHelpers

open System
open FsCheck

let chooseFrom options = gen {
    let! i = Gen.choose (0, List.length options-1)
    return (List.nth options i)
}

let arbitraryStringExcluding options = 
    let startsWithAny (s:string) = 
        options |> List.exists (s.StartsWith)
    let stringSuchThat predicate = 
        Gen.suchThat (fun x -> x <> Unchecked.defaultof<string> && x |> predicate) Arb.generate<string>
    stringSuchThat (startsWithAny >> not)

let paddedNumberGenerator numberGenerator = gen {
    let! n = numberGenerator

    let! padWithZeros = Arb.generate<bool>
    let! zeroCount = if padWithZeros && n > 0 then Arb.generate<uint32> else (Gen.constant 0u)
    let padded = String.Format("{0}{1}", (String.replicate (int zeroCount) "0"), n)

    return padded, n;
}