module HW5.Tests.AssertHelper
open Xunit
let ass (expected: decimal) (actual: decimal) =
    Assert.Equal (expected, actual)