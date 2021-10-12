module HW5.Tests.AssertHelper
open Xunit
let ass (expected: int) (actual: int) =
    Assert.Equal (expected, actual)