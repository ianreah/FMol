module FsCheckTest

open FsCheck.NUnit

[<PropertyAttribute>]
let revRevIsOrig (xs:list<int>) = List.rev(List.rev xs) = xs