

namespace Football.Domain.Enums;

public enum Operators
{
    contains,           // String contains
    equal,              // Numeric/date equality
    equalString,        // Case-insensitive string equality
    notEqual,           // Numeric/date inequality
    biggerThan,         // Numeric greater than
    lessThan,           // Numeric less than
    biggerThanOrEqual,  // Numeric >=
    lessThanOrEqual,    // Numeric <=
    startsWith,         // String starts with
    endsWith,           // String ends with
    inList,             // Value in comma-separated list
    dateAfter,          // Date comparisons
    dateBefore,
    dateEqual,
    booleanEqual        // Boolean checks
}
